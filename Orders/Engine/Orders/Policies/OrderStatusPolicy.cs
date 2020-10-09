using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.Orders.Policies
{
    public class OrderStatusPolicy : Policy
    {
        /// <summary>
        /// User has enter the payment detail, waiting for the Payment result message from Payment gateway
        /// </summary>
        public string WaitingForPayment { get; set; }


        /// <summary>
        /// Payment has made, but transaction required a manually review
        /// </summary>
        public string ManuallyReview { get; set; }
    }
}
