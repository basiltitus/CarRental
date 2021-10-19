using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Operations;
using Microsoft.Extensions.Configuration;
using Server.API.Models;
namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IConfiguration Configuration;
        PaymentOperation paymentOperation;
        public PaymentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            paymentOperation = new PaymentOperation(Configuration);
        }
        [HttpPost]
        [Route("addPayment")]
        public IActionResult AddPayment(PaymentTable item)
        {
            return Ok(paymentOperation.AddPayment(item));
        }
        [HttpGet("{userId}")]
        public IActionResult GetPaymentList(int userId)
        {
            return Ok(paymentOperation.GetPaymentList(userId));
        }
    }
}
