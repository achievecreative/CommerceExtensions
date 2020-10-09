using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Commerce.XA.Foundation.Common.Controllers;

namespace Achievecreative.Commerce.Plugin.Cart.Web.Controllers
{
    public class CheckoutController : BaseCommerceStandardController
    {
        [HttpGet]
        public ActionResult Checkout()
        {
            return View(GetRenderingView("Checkout"));
        }
    }
}