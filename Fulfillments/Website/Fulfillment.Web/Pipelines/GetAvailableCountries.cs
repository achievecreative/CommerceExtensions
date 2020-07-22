using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Pipelines;
using Sitecore.Commerce.Services.Orders;
using Sitecore.Commerce.XA.Foundation.Common.Context;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.DependencyInjection;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Web.Pipelines
{
    public class GetAvailableCountries : PipelineProcessor<ServicePipelineArgs>
    {
        public override void Process(ServicePipelineArgs args)
        {
            var result = (GetAvailableCountriesResult)args.Result;

            var commerceStorefront = ServiceLocator.ServiceProvider.GetService<IStorefrontContext>()?.CurrentStorefront;
            if (commerceStorefront == null)
            {
                return;
            }

            var countryRegionConfig = commerceStorefront.ControlPanel.Children.FirstOrDefault(x => x.Name == "Country-Region Configuration");
            if (countryRegionConfig == null)
            {
                return;
            }

            var selectedCountryItems = ((MultilistField) countryRegionConfig.Fields["Countries-Regions"]).Value.Split('|')
                .Where(x => ID.IsID(x)).Select(x => countryRegionConfig.Database.GetItem(ID.Parse(x)));

            var selectedCountries = selectedCountryItems.Select(x => x["Name"]);

            var countries = result.AvailableCountries.Where(x => selectedCountries.Contains(x.Value));

            result.AvailableCountries = countries.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}