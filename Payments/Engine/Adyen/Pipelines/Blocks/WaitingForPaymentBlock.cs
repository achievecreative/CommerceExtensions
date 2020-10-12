using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Components;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Payments;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Blocks
{
    public class WaitingForPaymentBlock : AsyncPipelineBlock<WaitingForPaymentOrdersMinionArgument, Order,
        CommercePipelineExecutionContext>
    {
        private readonly IGetOrderPipeline getOrderPipeline;

        public WaitingForPaymentBlock(IGetOrderPipeline getOrderPipeline)
        {
            this.getOrderPipeline = getOrderPipeline;
        }

        public override async Task<Order> RunAsync(WaitingForPaymentOrdersMinionArgument arg,
            CommercePipelineExecutionContext context)
        {
            var order = await getOrderPipeline.RunAsync(arg.OrderUniqueId.ToString(), context);

            var paymentComponment = order.GetComponent<FederatedPaymentComponent>();
            if (paymentComponment == null)
            {
                return order;
            }

            var paymentResults = paymentComponment.ChildComponents.OfType<PaymentResultComponent>()
                .OrderByDescending(x => x.CreatedTime).FirstOrDefault();
            if (paymentResults == null)
            {
                return order;
            }

            //TODO: Check payment result

            var knowOrderListPolicy = context.GetPolicy<KnownOrderListsPolicy>();
            var knowOrderStatusPolicy = context.GetPolicy<KnownOrderStatusPolicy>();

            var component = order.GetComponent<TransientListMembershipsComponent>();
            if (component == null)
            {
                component = new TransientListMembershipsComponent()
                {
                    Memberships = new List<string>() {knowOrderListPolicy.PendingOrders}
                };
            }
            else
            {
                component.Memberships.Add(knowOrderListPolicy.PendingOrders);
            }

            order.SetComponent(component);

            order.Status = knowOrderStatusPolicy.Pending;

            return order;
        }
    }
}
