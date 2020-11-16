using System;
using System.Threading.Tasks;
using Achievecreative.Commerce.Plugin.OrderNumber.Commands;
using Achievecreative.Commerce.Plugin.OrderNumber.Entities;
using Achievecreative.Commerce.Plugin.OrderNumber.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Sitecore.Commerce.Core;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Controllers
{
    [EnableQuery]
    public class NewOrderNumberController : CommerceODataController
    {
        public NewOrderNumberController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpGet]
        [EnableQuery]
        public async Task<OrderNumberModel> Get()
        {
            var r = await this.Command<NewOrderNumberCommand>().Process(this.CurrentContext);

            var entity = new NewOrderNumberEntity() { OrderNumber = r };

            return new OrderNumberModel() { OrderNumber = r, DateTime = DateTime.Now };
        }
    }
}
