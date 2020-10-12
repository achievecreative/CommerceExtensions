using System;
using System.Collections.Generic;
using System.Text;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines
{
    public interface IGetShopperReferencePipeline : IPipeline<GetShopperReferenceArgument, string, CommercePipelineExecutionContext>
    {
    }
}
