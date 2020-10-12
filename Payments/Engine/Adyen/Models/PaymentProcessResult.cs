using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Models
{
    public class PaymentProcessResult : Model
    {
        public bool Processed { get; set; }

        public string NewStatus { get; set; }

        public IList<string> NewLists { get; set; }
    }
}
