using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Entities
{
    public class OrderNumberEntity : CommerceEntity
    {
        public const string OrderNumberEntityId = "BKL.Commerce.Plugin.OrderNumberEntity";

        public uint LastOrderNumber { get; set; }
    }
}
