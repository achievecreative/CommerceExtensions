using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks
{
    public class GetFulfillmentMethodsByCountryBlock : PipelineBlock<IEnumerable<FulfillmentMethod>, IEnumerable<FulfillmentMethod>, CommercePipelineExecutionContext>
    {
        public override Task<IEnumerable<FulfillmentMethod>> Run(IEnumerable<FulfillmentMethod> arg, CommercePipelineExecutionContext context)
        {
            return Task.FromResult(arg);
        }
    }
}
