using System;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Commands
{
    public class NewOrderNumberCommand : CommerceCommand
    {
        public NewOrderNumberCommand(IServiceProvider provider) : base(provider)
        {

        }

        public virtual async Task<string> Process(CommerceContext commerceContext)
        {
            var pipeline = this.GetService<INewOrderNumberPipeline>();
            var r = await pipeline.Run(new NewOrderNumberArgument(), commerceContext.PipelineContext.ContextOptions);

            return r;
        }
    }
}
