// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2020
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using Achievecreative.Commerce.Plugin.Foundation.Pipelines.Blocks;
using Achievecreative.Commerce.Plugin.Orders.Pipelines.Blocks;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.SQL;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Achievecreative.Commerce.Plugin.Orders
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

            services.Sitecore().Pipelines(builder =>
            {
                builder.ConfigurePipeline<IFindEntitiesInListPipeline>(configure =>
                {
                    
                });

                builder.ConfigurePipeline<ICreateOrderPipeline>(configure =>
                {
                    configure.Add<MoveToWaitingForPaymentOrderBlock>().After<CreateOrderBlock>();
                });
            });
        }
    }
}
