using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines
{
    public class NewOrderNumberPipeline : CommercePipeline<NewOrderNumberArgument, string>, INewOrderNumberPipeline
    {
        public NewOrderNumberPipeline(IPipelineConfiguration<INewOrderNumberPipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {

        }
    }
}
