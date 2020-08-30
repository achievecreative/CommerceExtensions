using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Framework.Rules;
using SolrNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Rules
{
    [EntityIdentifier("CartFulfillmentCountryCondition")]
    public class CartFulfillmentCountryCondition : IFulfillmentCondition, ICondition, IMappableRuleEntity
    {
        public bool Evaluate(IRuleExecutionContext context)
        {
            var countryCode = CountryCode?.Yield(context);

            if (string.IsNullOrEmpty(countryCode))
            {
                return false;
            }

            var cart = context.Fact<CommerceContext>(null)?.GetObject<Cart>();
            if(cart == null)
            {
                return false;
            }

            if(!cart.Lines.Any())
            {
                return false;
            }

            var fulfillmentComponent = cart.GetComponent<FulfillmentComponent>() as PhysicalFulfillmentComponent;
            if(fulfillmentComponent == null)
            {
                return false;
            }

            var country = fulfillmentComponent.ShippingParty?.CountryCode;

            return countryCode.Equals(country, StringComparison.OrdinalIgnoreCase);
        }

        public IRuleValue<string> CountryCode { get; set; }
    }
}
