using System;
using System.Collections.Generic;
using System.Text;
using Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines.Arguments;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Payments.Adyen.Pipelines
{
    public class WaitingForPaymentOrdersMinionPipeline : CommercePipeline<WaitingForPaymentOrdersMinionArgument, bool>, IWaitingForPaymentOrdersMinionPipeline
    {
        public WaitingForPaymentOrdersMinionPipeline(IPipelineConfiguration<IWaitingForPaymentOrdersMinionPipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {
        }
    }
}
