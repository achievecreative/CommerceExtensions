using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments
{
    public class WaitingForPaymentOrdersMinionArgument:PipelineArgument
    {
        public Guid OrderUniqueId { get; set; }
    }
}
