using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks
{
    public class SaveCartInfoBlock: PipelineBlock<CartPartyArgument, CartPartyArgument, CommercePipelineExecutionContext>
    {
        public override Task<CartPartyArgument> Run(CartPartyArgument arg, CommercePipelineExecutionContext context)
        {
 
            return Task.FromResult(arg);
        }
    }
}
