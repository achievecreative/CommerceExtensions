using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;
using Sitecore.Framework.Pipelines.Abstractions;

namespace Achievecreative.Commerce.Plugin.BizFx.Pipelines.Blocks
{
    public class FilterEnvironmentsByRoleViewBlock : AsyncPipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private const string RolePrefix = "sitecore\\Environment";

        public override Task<EntityView> RunAsync(EntityView arg, CommercePipelineExecutionContext context)
        {
            //Environment Name role start with "Environment"

            var roles = context.CommerceContext.Headers["Roles"].ToArray().SelectMany(x => x.Split('|'))
                .Where(x => x.StartsWith(RolePrefix, StringComparison.OrdinalIgnoreCase)).Select(x =>
                    x.Replace(RolePrefix, string.Empty, StringComparison.OrdinalIgnoreCase));

            if (roles?.Any() ?? false)
            {
                arg.ChildViews.RemoveAll(x => x is EntityView view && !roles.Contains(view.ItemId));
            }

            return Task.FromResult(arg);
        }
    }
}
