using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Policies
{
    public class FulfillmentCountryPolicyItem : Policy
    {
        public string FulfillmentMethodName { get; set; }

        public string[] AvailableCountries { get; set; }

        public string[] NotAvailableCountries { get; set; }
    }
}
