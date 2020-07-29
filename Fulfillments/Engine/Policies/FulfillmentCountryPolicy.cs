using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Policies
{
    /// <summary>
    /// Use to specific the available countries
    /// GetAvailableFulfilmentMethodBlock will use this 
    /// </summary>
    public class FulfillmentCountryPolicy : Policy
    {
        public FulfillmentCountryPolicyItem[] Methods { get; set; }
    }
}
