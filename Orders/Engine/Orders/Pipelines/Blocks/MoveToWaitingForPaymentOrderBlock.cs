using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Orders.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Sitecore.Framework.Pipelines.Abstractions;

namespace Achievecreative.Commerce.Plugin.Orders.Pipelines.Blocks
{
    public class MoveToWaitingForPaymentOrderBlock : AsyncPipelineBlock<Order, Order, CommercePipelineExecutionContext>
    {
        public override Task<Order> RunAsync(Order arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: Order can't be null");

            if (!arg.HasComponent<TransientListMembershipsComponent>())
            {
                var component = new TransientListMembershipsComponent()
                {
                    Memberships = new List<string>() { nameof(NewOrderListPolicy.WaitingForPaymentOrders) }
                };
                arg.AddComponents(component);
            }
            else
            {
                var knowOrderListPolicy = context.CommerceContext.GetPolicy<KnownOrderListsPolicy>();
                arg.GetComponent<TransientListMembershipsComponent>().Memberships.Remove(knowOrderListPolicy.PendingOrders);
                arg.GetComponent<TransientListMembershipsComponent>().Memberships.Add(nameof(NewOrderListPolicy.WaitingForPaymentOrders));
            }

            return Task.FromResult(arg);
        }
    }
}
