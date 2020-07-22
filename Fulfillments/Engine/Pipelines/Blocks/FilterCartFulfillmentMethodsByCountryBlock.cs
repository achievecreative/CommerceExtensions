using System.Collections.Generic;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Fulfillments.Pipelines.Blocks
{
    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class FilterCartFulfillmentMethodsByCountryBlock : PipelineBlock<IEnumerable<Sitecore.Commerce.Plugin.Fulfillment.FulfillmentMethod>, IEnumerable<Sitecore.Commerce.Plugin.Fulfillment.FulfillmentMethod>, CommercePipelineExecutionContext>
    {
        private readonly IGetCartPipeline _getCartPipeline;
        public FilterCartFulfillmentMethodsByCountryBlock(IGetCartPipeline pipeline)
        {
            _getCartPipeline = pipeline;
        }

        public override Task<IEnumerable<Sitecore.Commerce.Plugin.Fulfillment.FulfillmentMethod>> Run(IEnumerable<Sitecore.Commerce.Plugin.Fulfillment.FulfillmentMethod> arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: The argument can not be null");

            var commerceContext = context.CommerceContext;

            var shopName = commerceContext.CurrentShopName();
            var shopperId = commerceContext.CurrentShopperId();

            var cartModel = context.GetModel<CartInfoModel>();

            //var cart = await _getCartPipeline.Run(new ResolveCartArgument(shopName, cartModel.CartId, shopperId), context);




            return Task.FromResult(arg);
        }
    }
}