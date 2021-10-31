using Microsoft.AspNetCore.Mvc;
using OnlineShopApi.Models;
using OnlineShopApi.Services;
using System.Collections.Generic;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _OrderService.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var Order = _OrderService.Get(id);

            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order Order)
        {
            _OrderService.Create(Order);

            return CreatedAtRoute("GetOrder", new { id = Order.Id.ToString() }, Order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order OrderIn)
        {
            var Order = _OrderService.Get(id);

            if (Order == null)
            {
                return NotFound();
            }

            _OrderService.Update(id, OrderIn);

            return NoContent();
        }
    }
}
