using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Policies;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Minions
{
    public class WaitingForPaymentOrdersMinion : Minion
    {
        private IWaitingForPaymentOrdersMinionPipeline waitingForPaymentOrdersMinionPipeline = null;

        protected override async Task<MinionRunResultsModel> Execute()
        {
            Condition.Requires(waitingForPaymentOrdersMinionPipeline).IsNotNull($"Not able to resolve the pipeline: IPaymentResultsMinionPipeline");

            int totalProcessedItem = 0;

            foreach (var listName in Policy.ListsToWatch)
            {
                var argument = await this.GetListIds<Order>(listName, Policy.ItemsPerBatch);

                var list = argument.List;
                do
                {
                    foreach (var guid in list.Items.Select(x => x.UniqueId))
                    {
                        var options = new CommercePipelineExecutionContextOptions(MinionContext);
                        var result = await waitingForPaymentOrdersMinionPipeline.RunAsync(new WaitingForPaymentOrdersMinionArgument() { OrderUniqueId = guid }, options);
                        if (result)
                        {
                            totalProcessedItem++;
                        }
                        else
                        {
                            //TODO: Log error
                        }
                    }

                    argument = await this.GetListIds<Order>(listName, Policy.ItemsPerBatch, list.CurrentPage * Policy.ItemsPerBatch);
                    list = argument.List;

                } while (list.CurrentPage < list.PageCount);
            }

            return new MinionRunResultsModel()
            {
                DidRun = true,
                ItemsProcessed = totalProcessedItem
            };
        }

        public override void Initialize(IServiceProvider serviceProvider, MinionPolicy policy, CommerceContext commerceContext)
        {
            base.Initialize(serviceProvider, policy, commerceContext);

            waitingForPaymentOrdersMinionPipeline = serviceProvider.GetService<IWaitingForPaymentOrdersMinionPipeline>();
        }
    }
}
