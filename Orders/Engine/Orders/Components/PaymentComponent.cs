using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Orders.Components
{
    public abstract class PaymentComponent : Component
    {
        public DateTime? CreateTime { get; set; }


    }
}
