using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Policies
{
    public class OrderNumberPolicy : Policy
    {
        public string Prefix { get; set; }

        public uint StartNumber { get; set; }

        public uint OrderNumberLength { get; set; }

        public char PaddingCharacter { get; set; }
    }
}
