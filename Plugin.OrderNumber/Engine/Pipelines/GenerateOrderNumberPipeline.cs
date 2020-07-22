using Achievecreative.Commerce.Plugin.OrderNumber.Entities;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines
{
    public class GenerateOrderNumberPipeline : CommercePipeline<GenerateOrderNumberArgument, OrderNumberEntity>, IGenerateNewOrderNumberPipeline
    {
        public GenerateOrderNumberPipeline(IPipelineConfiguration<IGenerateNewOrderNumberPipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {

        }
    }
}
