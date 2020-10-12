using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Blocks
{
    public class ChangeOrderStatusBlock : AsyncPipelineBlock<Order, Order, CommercePipelineExecutionContext>
    {
        public override Task<Order> RunAsync(Order arg, CommercePipelineExecutionContext context)
        {
            var resultModel = context.GetModel<PaymentProcessResult>();
            if (!(resultModel?.Processed ?? false))
            {
                return Task.FromResult(arg);
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

            return Task.FromResult(arg);
        }
    }
}
