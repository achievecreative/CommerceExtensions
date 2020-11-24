// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2020
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using Sitecore.Framework.Rules;
using System.Reflection;
using Achievecreative.Commerce.Plugin.BizFx.Pipelines.Blocks;
using Sitecore.Commerce.Plugin.BusinessUsers;
using ConfigureServiceApiBlock = Sitecore.Commerce.Core.ConfigureServiceApiBlock;

namespace Achievecreative.Commerce.Plugin.BizFx
{
    /// <summary>The configure sitecore class.</summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>The configure services.</summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            services.Sitecore().Rules(config => config.Registry(registry => registry.RegisterAssembly(assembly)));

            services.Sitecore().Pipelines(builder => builder
                .ConfigurePipeline<IBizFxEnvironmentsPipeline>(pipeline => pipeline.Add<FilterEnvironmentsByRoleViewBlock>().After<FilterEnvironmentsViewBlock>())
            );
        }
    }
}
