// © 2019 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Models;
using Microsoft.AspNet.OData.Builder;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber
{
    /// <summary>
    /// Defines a block which configures the OData model
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         Sitecore.Framework.Pipelines.PipelineBlock{Microsoft.AspNetCore.OData.Builder.ODataConventionModelBuilder,
    ///         Microsoft.AspNetCore.OData.Builder.ODataConventionModelBuilder,
    ///         Sitecore.Commerce.Core.CommercePipelineExecutionContext}
    ///     </cref>
    /// </seealso>
    [PipelineDisplayName("OrderNumberPluginConfigureServiceApiBlock")]
    public class ConfigureServiceApiBlock : AsyncPipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ODataConventionModelBuilder"/>.
        /// </returns>
        public override Task<ODataConventionModelBuilder> RunAsync(ODataConventionModelBuilder arg, CommercePipelineExecutionContext context)
        {
            //Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");

            //// Add the entities
            //arg.AddEntityType(typeof(SampleEntity));



            //// Add the entity sets
            //arg.EntitySet<SampleEntity>("Sample");

            //// Add complex types

            //// Add unbound functions

            //// Add unbound actions
            //var configuration = arg.Action("SampleCommand");
            //configuration.Parameter<string>("Id");
            //configuration.ReturnsFromEntitySet<CommerceCommand>("Commands");

            arg.AddEntityType(typeof(OrderNumberModel));
            arg.EntitySet<OrderNumberModel>("NewOrderNumber");

            return Task.FromResult(arg);
        }
    }
}
