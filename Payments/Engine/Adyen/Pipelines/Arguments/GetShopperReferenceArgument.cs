using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments
{
    public class GetShopperReferenceArgument : PipelineArgument
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }
    }
}
