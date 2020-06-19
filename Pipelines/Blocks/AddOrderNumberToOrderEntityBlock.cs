using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Components;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Achievecreative.Commerce.Plugin.OrderNumber.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks
{
    public class AddOrderNumberToOrderEntityBlock : PipelineBlock<Order, Order, CommercePipelineExecutionContext>
    {
        private readonly INewOrderNumberPipeline _newOrderNumberPipeline;
        public AddOrderNumberToOrderEntityBlock(INewOrderNumberPipeline newOrderNumberPipeline)
        {
            this._newOrderNumberPipeline = newOrderNumberPipeline;
        }

        public override async Task<Order> Run(Order arg, CommercePipelineExecutionContext context)
        {
            var orderNumberPolicy = context.GetPolicy<OrderNumberPolicy>();

            var pipelineArgs = new NewOrderNumberArgument()
            {
                Prefix = orderNumberPolicy?.Prefix,
                Separator = "-"
            };

            var fulfillmentComponent = arg.GetComponent<FulfillmentComponent>();
            if (fulfillmentComponent is PhysicalFulfillmentComponent)
            {
                pipelineArgs.CountryCode = ((PhysicalFulfillmentComponent)fulfillmentComponent).ShippingParty.CountryCode;
            }

            var orderNumber = await _newOrderNumberPipeline.Run(pipelineArgs, context);

            var orderNumberComponent = new OrderNumberComponent()
            {
                OrderNumber = orderNumber
            };

            arg.AddComponents(orderNumberComponent);

            return arg;
        }
    }
}
