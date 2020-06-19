using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Entities;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Achievecreative.Commerce.Plugin.OrderNumber.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks
{
    [PipelineDisplayName("BKLCommerce.GenerateNewOrderNumberBlock")]
    public class GenerateNewOrderNumberBlock : PipelineBlock<GenerateOrderNumberArgument, OrderNumberEntity, CommercePipelineExecutionContext>
    {
        private static readonly ConcurrentDictionary<string, object> LockTable = new ConcurrentDictionary<string, object>();
        
        private const string DefaultOrderNumberLockKey = "Global";


        private readonly IFindEntityPipeline _findEntityPipeline;
        private readonly IPersistEntityPipeline _persistEntityPipeline;
        public GenerateNewOrderNumberBlock(IFindEntityPipeline findEntityPipeline, IPersistEntityPipeline persistEntityPipeline)
        {
            this._findEntityPipeline = findEntityPipeline;
            this._persistEntityPipeline = persistEntityPipeline;
        }

        public override Task<OrderNumberEntity> Run(GenerateOrderNumberArgument arg, CommercePipelineExecutionContext context)
        {
            var orderNumberPolicy = context.GetPolicy<OrderNumberPolicy>();

            var shopName = context.CommerceContext.CurrentShopName();
            if (string.IsNullOrEmpty(shopName))
            {
                shopName = DefaultOrderNumberLockKey;
            }

            var lockObject = LockTable.GetOrAdd(shopName, x => new object());

            lock (lockObject)
            {
                Condition.Requires(_findEntityPipeline, "findEntitiesPipeline");

                var findEntityArgument = new FindEntityArgument(typeof(OrderNumberEntity), OrderNumberEntity.OrderNumberEntityId);

                var foundEntity = _findEntityPipeline.Run(findEntityArgument, context).Result;

                var result = foundEntity as OrderNumberEntity;

                if (result == null)
                {
                    result = new OrderNumberEntity()
                    {
                        UniqueId = Guid.NewGuid(),
                        Id = OrderNumberEntity.OrderNumberEntityId,
                        Version = 1,
                        LastOrderNumber = orderNumberPolicy?.StartNumber ?? 1,
                        DateCreated = DateTimeOffset.Now
                    };
                }
                else
                {
                    result.LastOrderNumber += 1;
                    result.DateUpdated = DateTimeOffset.Now;
                }

                var persistEntity = _persistEntityPipeline.Run(new PersistEntityArgument(result), context).Result;

                return Task.FromResult(result);
            }
        }
    }
}
