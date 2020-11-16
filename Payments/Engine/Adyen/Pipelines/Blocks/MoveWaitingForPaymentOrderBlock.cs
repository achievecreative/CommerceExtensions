using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Orders.Policies;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Blocks
{
    public class MoveWaitingForPaymentOrderBlock : AsyncPipelineBlock<Order, Order, CommercePipelineExecutionContext>
    {
        private readonly IPersistOrderPipeline persistOrderPipeline;
        private readonly IRemoveListEntitiesPipeline removeListEntitiesPipeline;

        public MoveWaitingForPaymentOrderBlock(IRemoveListEntitiesPipeline removeListEntitiesPipeline, IPersistOrderPipeline persistOrderPipeline)
        {
            this.removeListEntitiesPipeline = removeListEntitiesPipeline;
            this.persistOrderPipeline = persistOrderPipeline;
        }

        public override async Task<Order> RunAsync(Order arg, CommercePipelineExecutionContext context)
        {
            var resultModel = context.GetModel<PaymentProcessResult>();
            if (!(resultModel?.Processed ?? false))
            {
                return arg;
            }

            var component = arg.GetComponent<TransientListMembershipsComponent>();
            if (component == null)
            {
                component = new TransientListMembershipsComponent()
                {
                    Memberships = resultModel.NewLists
                };
            }
            else
            {
                foreach (var resultModelNewList in resultModel.NewLists)
                {
                    component.Memberships.Add(resultModelNewList);
                }
            }

            arg.SetComponent(component);
            arg.Status = resultModel.NewStatus;

            var entityIds = new[] { arg.Id };

            //Remove from previous list
            await this.removeListEntitiesPipeline.RunAsync(new ListEntitiesArgument(entityIds, nameof(NewOrderListPolicy.WaitingForPaymentOrders)), context);
            await this.persistOrderPipeline.RunAsync(arg, context);

            return arg;
        }
    }
}
