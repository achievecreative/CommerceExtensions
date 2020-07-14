using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Policies
{
    /// <summary>
    /// Use to specific the available countries
    /// GetAvailableFulfilmentMethodBlock will use this 
    /// </summary>
    public class FulfilmentFeeAvailableCountryPolicy : Policy
    {
        public string[] Countries { get; set; }
    }
}
