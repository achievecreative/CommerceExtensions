using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks
{
    /*
     * Master View properties -> Summary section in order detail
     *
     * Summary View -> Details section in order detail
     *
     *
     */

    [PipelineDisplayName("Order.block.GetOrderNumberEntityViewBlock")]
    public class GetOrderNumberEntityViewBlock : AsyncPipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> RunAsync(EntityView arg, CommercePipelineExecutionContext context)
        {
            var entityViewArgument = context.CommerceContext.GetObject<EntityViewArgument>();
            if (entityViewArgument == null)
            {
                return Task.FromResult(arg);
            }

            var knownOrderViewPolicy = context.GetPolicy<KnownOrderViewsPolicy>();
            if (knownOrderViewPolicy == null)
            {
                return Task.FromResult(arg);
            }

            if (entityViewArgument.ViewName != knownOrderViewPolicy.Master)
            {
                return Task.FromResult(arg);
            }

            var order = entityViewArgument.Entity as Order;
            if (order == null)
            {
                return Task.FromResult(arg);
            }

            var orderNumberComponent = order.GetComponent<OrderNumberComponent>();
            if (orderNumberComponent == null)
            {
                return Task.FromResult(arg);
            }

            var orderNumberProp = new ViewProperty()
            {
                Name = "OrderNumber",
                RawValue = orderNumberComponent.OrderNumber,
                IsReadOnly = true
            };

            arg.Properties.Add(orderNumberProp);
            return Task.FromResult(arg);
        }
    }
}
