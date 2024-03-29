﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2020
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using System.Reflection;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Blocks;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines.Definitions.FunctionExtensions;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen
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

            //services.Sitecore().Rules(config => config.Registry(registry => registry.RegisterAssembly(assembly)));

            //services.Sitecore().Pipelines(builder => builder

            //.ConfigurePipeline<IConfigureServiceApiPipeline>(pipeline => pipeline
            //    .Add<ConfigureServiceApiBlock>()
            //)

            //);

            services.Sitecore().Pipelines(config =>
            {
                config.AddPipeline<IGetShopperReferencePipeline, GetShopperReferencePipeline>(configure =>
                {
                    configure.Add<GenerateShopperReferenceBlock>();
                });

                config.AddPipeline<IWaitingForPaymentOrdersMinionPipeline, WaitingForPaymentOrdersMinionPipeline>(configure =>
                {
                    configure
                        .Add<CheckAdyenPaymentBlock>()
                        .Add<MoveWaitingForPaymentOrderBlock>();
                });
            });
        }
    }
}
