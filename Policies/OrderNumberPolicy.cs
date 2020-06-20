using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Policies
{
    public class OrderNumberPolicy : Policy
    {
        public string Prefix { get; set; }

        public int StartNumber { get; set; }

        public int OrderNumberLength { get; set; }

        public char PaddingCharacter { get; set; }
    }
}
