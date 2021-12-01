using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

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
        public IActionResult AddOrder(Order order)
        {

            try
            {
                return Ok(orderOperation.AddOrder(order));
            }
            catch (Exception e)
            {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpGet("{orderId}/{userId}")]
        [AllowAnonymous]
        public IActionResult GetOrderDetails(int orderId, int userId)
        {
            try
            {
                return Ok(orderOperation.GetOrderDetails(orderId, userId));
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpGet("getreceipt/{orderId}/{userId}")]
        public IActionResult GetReciept(int orderId, int userId)
        {
            try
            {
                return Ok(orderOperation.GetReciept(orderId, userId));
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("makepayment/{orderId}")]
        public IActionResult MakePayment(int orderId)
        {
            try
            {
                return Ok(orderOperation.MakePayment(orderId));
            }
            catch (Exception e)
            {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("ExtraDays")]
        public IActionResult CompleteOrder(int orderId)
        {
            try
            {
                return Ok(orderOperation.CompleteOrder(orderId));
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("userId")]
        [ActionName("orderByUserId")]
        public IActionResult GetOrderDetailsByUserId(int userId)
        {
            try
            {
                return Ok(orderOperation.GetOrderDetailsByUserId(userId));
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("requestreturn/{id}")]
        public IActionResult RequestReturn(int id)
        {
            try
            {
                return Ok(orderOperation.RequestReturn(id));
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpGet("getadminrequests")]
        public IActionResult GetAdminRequests()
        {
            try
            {
                return Ok(orderOperation.GetAdminRequests());
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpGet("caravailability/fromdate={FromDate}&todate={ToDate}/{CarId}")]
        public IActionResult GetCarAvailability(DateTime FromDate, DateTime ToDate, int CarId)
        {
            try
            {
                return Ok(orderOperation.GetCarAvailability(FromDate, ToDate, CarId));
            }
            catch (Exception e)
            {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("updateorder")]
        public IActionResult UpdateOrder(Order order)
        {
            try
            {
                return Ok(orderOperation.UpdateOrder(order));
            }
            catch (Exception e)
            {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
