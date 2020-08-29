using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks
{
    public class ValidationPartyBlock : AsyncPipelineBlock<Party, Party, CommercePipelineExecutionContext>
    {
        private readonly IGetCountryPipeline _getCountryPipeline;
        public ValidationPartyBlock(IGetCountryPipeline getCountryPipeline)
        {
            _getCountryPipeline = getCountryPipeline;
        }

        public override async Task<Party> RunAsync(Party arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires<Party>(arg).IsNotNull(base.Name + ": The argument cannot be null.");
            var validationPolicy = ValidationPolicy.GetValidationPolicy(context.CommerceContext, typeof(Party));

            //Update CountryCode by Country
            await UpdateCountryAndCountryCode(arg, context);

            var result = await validationPolicy.ValidateModels(arg, context);
            if (result)
            {
                return arg;
            }

            return null;
        }

        private async Task UpdateCountryAndCountryCode(Party arg, CommercePipelineExecutionContext context)
        {
            if (!string.IsNullOrEmpty(arg.CountryCode) && !string.IsNullOrEmpty(arg.Country))
            {
                return;
            }

            if (!string.IsNullOrEmpty(arg.Country))
            {
                var country = await _getCountryPipeline.RunAsync(new GetCountryArgument(arg.Country), context);
                if (country != null && (country.Name == arg.Country || country.IsoCode2 == arg.Country))
                {
                    arg.Country = country.Name;
                    arg.CountryCode = country.IsoCode2;
                }

                return;
            }

            var countryByCountryCode = await _getCountryPipeline.RunAsync(new GetCountryArgument(arg.CountryCode), context);
            if (countryByCountryCode != null && (countryByCountryCode.IsoCode2 == arg.CountryCode || countryByCountryCode.Name == arg.CountryCode))
            {
                arg.Country = countryByCountryCode.Name;
                arg.CountryCode = countryByCountryCode.IsoCode2;
            }
        }
    }
}
