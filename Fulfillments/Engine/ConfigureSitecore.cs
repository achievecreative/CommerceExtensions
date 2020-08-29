// © 2019 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Reflection;
using Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Achievecreative.Commerce.Plugin.Fulfillments
{
    /// <summary>
    /// The configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            services.Sitecore().Pipelines(config =>
                    config.ConfigurePipeline<IValidatePartyPipeline>(configure =>
                    {
                        configure.Replace<ValidatePartyBlock, ValidationPartyBlock>();
                    })
                    .ConfigurePipeline<IGetCartFulfillmentMethodsPipeline>(configure =>
                    {
                        configure.Add<FilterCartFulfillmentMethodsByCountryBlock>().After<FilterCartFulfillmentMethodsBlock>();
                    })
                    .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<ConfigureServiceApiBlock>()));
        }
    }
}
