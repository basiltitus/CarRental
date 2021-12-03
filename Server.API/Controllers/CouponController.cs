using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Server.API.Models;
using Server.API.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private IConfiguration Configuration;
        CouponOperation couponOperation;
        public CouponController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            couponOperation = new CouponOperation(Configuration);
        }

        [HttpGet]
        public IActionResult GetCoupons()
        {
            try
            {
                return Ok(couponOperation.GetCoupons());
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
        [HttpGet("{id}")]
        public IActionResult GetCoupon(int id)
        {
            try
            {
                return Ok(couponOperation.GetCoupon(id));
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
        [HttpPost]
        public IActionResult AddCoupon(Coupon coupon)
        {
            try
            {
                return Ok(couponOperation.AddCoupon(coupon));
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
        [HttpPut]
        public IActionResult EditCoupon(Coupon coupon)
        {
            try
            {
                return Ok(couponOperation.EditCoupon(coupon));
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
        [HttpGet("ChangeCouponStatus/{id}/{status}")]
        public IActionResult ChangeCouponStatus(int id,bool status)
        {
            try
            {
                return Ok(couponOperation.ChangeCouponStatus(id,status));
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
