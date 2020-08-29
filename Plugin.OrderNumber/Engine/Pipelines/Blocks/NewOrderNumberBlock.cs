using System.Linq;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments;
using Achievecreative.Commerce.Plugin.OrderNumber.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks
{
    [PipelineDisplayName("Achievecreative.CommerceGetNewOrderBlock")]
    public class NewOrderNumberBlock : AsyncPipelineBlock<NewOrderNumberArgument, string, CommercePipelineExecutionContext>
    {
        private readonly IGenerateNewOrderNumberPipeline _generateNewOrderNumberPipeline;
        public NewOrderNumberBlock(IGenerateNewOrderNumberPipeline generateNewOrderNumberPipeline)
        {
            this._generateNewOrderNumberPipeline = generateNewOrderNumberPipeline;
        }

        public override async Task<string> RunAsync(NewOrderNumberArgument arg, CommercePipelineExecutionContext context)
        {
            var orderNumberEntity = await _generateNewOrderNumberPipeline.RunAsync(new GenerateOrderNumberArgument(), context);

            var orderNumber = orderNumberEntity.LastOrderNumber.ToString().PadLeft(arg.OrderNumberLength, arg.PaddingCharacter);

            var buffer = new string[7];

            if (!string.IsNullOrEmpty(arg.Prefix))
            {
                buffer[0] = arg.Prefix;
                buffer[1] = arg.Separator;
            }

            buffer[3] = string.IsNullOrEmpty(arg.CountryCode) ? "NA" : arg.CountryCode;
            buffer[4] = arg.Separator;

            buffer[5] = orderNumber;

            if (!string.IsNullOrEmpty(arg.Suffix))
            {
                buffer[6] = arg.Separator;
                buffer[7] = arg.Suffix;
            }

            return string.Join("", buffer.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
