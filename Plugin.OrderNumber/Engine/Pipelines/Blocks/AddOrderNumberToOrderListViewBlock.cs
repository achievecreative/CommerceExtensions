using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Orders;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks
{
    public class AddOrderNumberToOrderListViewBlock : GetListViewBlock
    {
        private readonly IGetOrderPipeline getOrderPipeline;
        public AddOrderNumberToOrderListViewBlock(IGetOrderPipeline getOrderPipeline, CommerceCommander commerceCommander) : base(commerceCommander)
        {
            this.getOrderPipeline = getOrderPipeline;
        }

        public override async Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            var entityViewArgument = context.CommerceContext.GetObject<EntityViewArgument>();
            if (entityViewArgument == null)
            {
                return arg;
            }

            var knownOrderViewPolicy = context.GetPolicy<KnownOrderViewsPolicy>();
            if (knownOrderViewPolicy == null)
            {
                return arg;
            }

            if (!entityViewArgument.ViewName.Contains("OrdersList"))
            {
                return arg;
            }

            var orderListEntityView = arg.ChildViews.OfType<EntityView>();
            foreach (var entityView in orderListEntityView)
            {
                var orderId = entityView.ItemId;
                var order = await this.getOrderPipeline.Run(orderId, context);
                if (order != null)
                {
                    entityView.Properties.Add(new ViewProperty()
                    {
                        Name = "OrderNumber",
                        RawValue = order.GetComponent<OrderNumberComponent>()?.OrderNumber
                    });
                }
            }

            return arg;
        }
    }
}
