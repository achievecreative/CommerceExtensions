// © 2019 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Reflection;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines;
using Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Achievecreative.Commerce.Plugin.OrderNumber
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

            services.Sitecore().Pipelines(config =>
            {
                config.AddPipeline<IGenerateNewOrderNumberPipeline, GenerateOrderNumberPipeline>(configure =>
                    {
                        configure.Add<GenerateNewOrderNumberBlock>();
                    })
                    .AddPipeline<INewOrderNumberPipeline, NewOrderNumberPipeline>(configure =>
                    {
                        configure.Add<NewOrderNumberBlock>();
                    })
                    .ConfigurePipeline<ICreateOrderPipeline>(configure =>
                    {
                        configure.Add<AddOrderNumberToOrderEntityBlock>().Before<IPersistEntityPipeline>();
                    })
                    .ConfigurePipeline<IConfigureServiceApiPipeline>(configure =>
                    {
                        configure.Add<ConfigureServiceApiBlock>();
                    });
            });

            services.RegisterAllCommands(assembly);
        }
    }
}
