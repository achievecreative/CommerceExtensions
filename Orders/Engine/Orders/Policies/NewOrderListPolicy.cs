using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Orders.Policies
{
    public class NewOrderListPolicy : Policy
    {
        public string WaitingForPaymentOrders { get; set; }

        public string ManuallyReviewOrders { get; set; }
    }
}
