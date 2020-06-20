using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines
{
    [PipelineDisplayName("Achievecreative.Commerce.NewOrderNumber")]
    public interface INewOrderNumberPipeline : IPipeline<NewOrderNumberArgument, string, CommercePipelineExecutionContext>
    {

    }
}
