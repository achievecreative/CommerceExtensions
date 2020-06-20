using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Arguments
{
    public class NewOrderNumberArgument : PipelineArgument
    {
        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string CountryCode { get; set; }

        public string Separator { get; set; }

        public char PaddingCharacter { get; set; }

        public int OrderNumberLength { get; set; }
    }
}
