
/*
 *  use this viewblock to add the order number to the document, need to add the viewblock to 2 pipelines:
 * IFullIndexMinionPipeline and IIncrementalIndexMinionPipeline
 *
 * Also need to update the Policy to include the ordernumber field
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Search;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Pipelines.Blocks
{
    public class AddOrderNumberToIndexDocumentViewBlock : AsyncPipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> RunAsync(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires<EntityView>(arg).IsNotNull(base.Name + ":argument cannot be null");
            var argument = context.CommerceContext.GetObjects<SearchIndexMinionArgument>().FirstOrDefault<SearchIndexMinionArgument>();
            if (!string.IsNullOrEmpty(argument?.Policy.Name))
            {
                List<Order> source = argument.Entities?.OfType<Order>().ToList<Order>();
                if (source == null || !source.Any<Order>())
                {
                    return Task.FromResult<EntityView>(arg);
                }

                var searchViewName = context.GetPolicy<KnownSearchViewsPolicy>();
                foreach (var order in source)
                {
                    EntityView item = arg.ChildViews.Cast<EntityView>().FirstOrDefault<EntityView>(v => v.EntityId.Equals(order.Id, StringComparison.OrdinalIgnoreCase) && v.Name.Equals(searchViewName.Document, StringComparison.OrdinalIgnoreCase));
                    if (item == null)
                    {
                        continue;
                    }

                    var orderNumberComponent = order.GetComponent<OrderNumberComponent>();
                    if (orderNumberComponent == null)
                    {
                        continue;
                    }

                    var orderNumberView = new ViewProperty()
                    {
                        Name = "OrderNumber",
                        RawValue = orderNumberComponent.OrderNumber
                    };

                    item.Properties.Add(orderNumberView);
                }
            }

            return Task.FromResult(arg);
        }
    }
}
