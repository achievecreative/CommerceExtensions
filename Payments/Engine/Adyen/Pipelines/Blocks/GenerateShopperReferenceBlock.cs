using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Blocks
{
    public class GenerateShopperReferenceBlock : AsyncPipelineBlock<GetShopperReferenceArgument, string, CommercePipelineExecutionContext>
    {
        public override Task<string> RunAsync(GetShopperReferenceArgument arg, CommercePipelineExecutionContext context)
        {
            if ((arg?.UserId ?? Guid.Empty) == Guid.Empty)
            {
                return Task.FromResult(string.Empty);
            }

            var adyenPolicy = context.CommerceContext.GetPolicy<AdyenPolicy>();
            Condition.Requires(adyenPolicy).IsNotNull($"Adyen policy not found.");

            var reference = $"{adyenPolicy.ReferencePrefix}-{arg.UserId.ToString("N").ToUpper()}";

            return Task.FromResult(reference);
        }
    }
}
