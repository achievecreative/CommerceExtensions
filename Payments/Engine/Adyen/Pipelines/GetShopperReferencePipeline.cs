using System;
using System.Collections.Generic;
using System.Text;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines
{
    public class GetShopperReferencePipeline : CommercePipeline<GetShopperReferenceArgument, string>, IGetShopperReferencePipeline
    {
        public GetShopperReferencePipeline(IPipelineConfiguration<IGetShopperReferencePipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {
        }
    }
}
