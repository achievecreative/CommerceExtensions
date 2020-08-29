using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Fulfillments.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Framework.Pipelines;
using Sitecore.Framework.Pipelines.Abstractions;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks
{
    public class FilterCartFulfillmentMethodsByCountryBlock : AsyncPipelineBlock<IEnumerable<FulfillmentMethod>, IEnumerable<FulfillmentMethod>, CommercePipelineExecutionContext>
    {
        public override Task<IEnumerable<FulfillmentMethod>> RunAsync(IEnumerable<FulfillmentMethod> arg, CommercePipelineExecutionContext context)
        {
            var methods = arg;

            var fulfillmentCountryPolicy = context.CommerceContext.GetPolicy<FulfillmentCountryPolicy>();

            foreach (var fulfillmentMethod in methods)
            {
                var policy = fulfillmentCountryPolicy.Methods.FirstOrDefault(x => x.FulfillmentMethodName == fulfillmentMethod.Name);
                if (policy != null)
                {
                    if (policy.AvailableCountries == null)
                    {
                        policy.AvailableCountries = new string[0];
                    }

                    if (policy.NotAvailableCountries == null)
                    {
                        policy.NotAvailableCountries = new string[0];
                    }

                    fulfillmentMethod.SetPolicy(policy);
                }
            }

            return Task.FromResult(methods);
        }
    }
}
