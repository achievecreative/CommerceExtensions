using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Components;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Models;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Payments;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Blocks
{
    public class CheckAdyenPaymentBlock : AsyncPipelineBlock<WaitingForPaymentOrdersMinionArgument, Order,
        CommercePipelineExecutionContext>
    {
        private readonly IGetOrderPipeline getOrderPipeline;

        public CheckAdyenPaymentBlock(IGetOrderPipeline getOrderPipeline)
        {
            this.getOrderPipeline = getOrderPipeline;
        }

        public override async Task<Order> RunAsync(WaitingForPaymentOrdersMinionArgument arg, CommercePipelineExecutionContext context)
        {
            var order = await getOrderPipeline.RunAsync(arg.EntityId, context);

            var paymentComponent = order.GetComponent<FederatedPaymentComponent>();
            if (paymentComponent == null)
            {
                return order;
            }

            //TODO: Check payment result
            var paymentResults = paymentComponent.ChildComponents.OfType<PaymentResultComponent>().OrderByDescending(x => x.CreatedTime).FirstOrDefault();
            if (paymentResults == null)
            {
                //TODO: Ignore the payment result for now
                //return order;
            }

            var knowOrderListPolicy = context.GetPolicy<KnownOrderListsPolicy>();
            var knowOrderStatusPolicy = context.GetPolicy<KnownOrderStatusPolicy>();

            context.AddModel(new PaymentProcessResult() { Processed = true, NewStatus = knowOrderStatusPolicy.Pending, NewLists = new List<string>() { knowOrderListPolicy.PendingOrders } });

            return order;
        }
    }
}
