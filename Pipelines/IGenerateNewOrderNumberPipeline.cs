using Achievecreative.Commerce.Plugin.OrderNumber.Entities;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines
{
    [PipelineDisplayName("BKL.Commerce.GenerateOrderNumberPipeline")]
    public interface IGenerateNewOrderNumberPipeline : IPipeline<GenerateOrderNumberArgument, OrderNumberEntity, CommercePipelineExecutionContext>
    {

    }
}
