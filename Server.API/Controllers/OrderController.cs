using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Models;
using Microsoft.Extensions.Configuration;
using Server.API.Models;
namespace Server.API.Operations
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class OrderController : ControllerBase
    {
        private IConfiguration Configuration;
        public OrderOperation orderOperation;
        public OrderController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            orderOperation = new OrderOperation(Configuration);
        }
        [HttpPost]
        [Route("addorder")]
        public IActionResult addOrder(OrderTable order)
        {
            return Ok(orderOperation.AddOrder(order));
        }
        [HttpGet("{orderId}")]
        public IActionResult getOrderDetails(int orderId)
        {
            return Ok(orderOperation.getOrderDetails(orderId));
        }
        [HttpGet("ExtraDays")]
        public IActionResult completeOrder(int orderId,int extraDays)
        {
            return Ok(orderOperation.completeOrder(orderId, extraDays));
        }
        [HttpGet("userId")]
        [ActionName("orderByUserId")]
        public IActionResult getOrderDetailsByUserId(int userId)
        {
            return Ok(orderOperation.getOrderDetailsByUserId(userId));
        }
    }
}
