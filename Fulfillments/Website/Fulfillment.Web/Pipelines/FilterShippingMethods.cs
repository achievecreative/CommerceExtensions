using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Commerce.Engine;
using Sitecore.Commerce.Engine.Connect.Pipelines;
using Sitecore.Commerce.Engine.Connect.Pipelines.Shipping;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Entities.Shipping;
using Sitecore.Commerce.Pipelines;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Commerce.Services.Shipping;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Web.Pipelines
{
    public class FilterShippingMethods : GetShippingMethods
    {
        private IDictionary<string, (string[] availables, string[] notAvailables)> fulfillmentCountryPolics = new ConcurrentDictionary<string, (string[] availables, string[] notAvailables)>();

        public FilterShippingMethods(IEntityFactory entityFactory) : base(entityFactory)
        {

        }

        public override void Process(ServicePipelineArgs args)
        {
            fulfillmentCountryPolics.Clear();

            base.Process(args);
            var request = args.Request as GetShippingMethodsRequest;
            var result = args.Result as GetShippingMethodsResult;
            if (request == null || result == null)
            {
                return;
            }

            var countryCode = request.Party?.Country;
            if (string.IsNullOrEmpty(countryCode))
            {
                return;
            }


            var updatedShippingMethods = result.ShippingMethods.ToList();
            updatedShippingMethods.RemoveAll(x =>
            {
                var policyFound = fulfillmentCountryPolics.TryGetValue(x.Name, out (string[] availablies, string[] notAvailables) countries);
                if (!policyFound)
                {
                    return false;
                }

                var inAvailableList = countries.availablies?.Contains(countryCode) ?? false;
                if (inAvailableList)
                {
                    return false;
                }

                var inNotAvailableList = countries.notAvailables?.Contains(countryCode) ?? false;
                if (inNotAvailableList)
                {
                    return true;
                }

                return false;
            });

            result.ShippingMethods = updatedShippingMethods.AsReadOnly();
        }

        protected override ShippingMethod TranslateShippingMethod(FulfillmentMethod fulfillment)
        {
            var policy = fulfillment.Policies.OfType<FulfillmentCountryPolicyItem>().FirstOrDefault();
            if (policy != null)
            {
                fulfillmentCountryPolics.Add(policy.FulfillmentMethodName, (policy.AvailableCountries?.ToArray(), policy.NotAvailableCountries?.ToArray()));
            }

            return base.TranslateShippingMethod(fulfillment);
        }
    }
}