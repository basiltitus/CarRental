using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Models;
using Microsoft.Extensions.Configuration;

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
        public IActionResult AddOrder(OrderTable order)
        {
            return Ok(orderOperation.AddOrder(order));
        }
        [HttpGet("{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            return Ok(orderOperation.GetOrderDetails(orderId));
        }
        [HttpGet("ExtraDays")]
        public IActionResult CompleteOrder(int orderId,int extraDays)
        {
            return Ok(orderOperation.CompleteOrder(orderId, extraDays));
        }
        [HttpGet("userId")]
        [ActionName("orderByUserId")]
        public IActionResult GetOrderDetailsByUserId(int userId)
        {
            return Ok(orderOperation.GetOrderDetailsByUserId(userId));
        }
    }
}
