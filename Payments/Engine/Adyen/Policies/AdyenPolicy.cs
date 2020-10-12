using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Policies
{
    public class AdyenPolicy : Policy
    {
        public string ReferencePrefix { get; set; }

        public string MerchantAccount { get; set; }

        public string Channel { get; set; }

        public string ApiKey { get; set; }

        public string Environment { get; set; }
    }
}
