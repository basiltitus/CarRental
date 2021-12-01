using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Operations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration Configuration;
        Authentication auth;
        public AuthController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            auth = new Authentication(Configuration);
        }
        [HttpPost()]
        [Route("signin")]
        public IActionResult Login(UserAuth item)
        {

            try
            {
                return Ok(auth.SignIn(item.EmailId, item.Password));
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
        [Route("signup")]
        public IActionResult Register(User u)
        {
            try
            {
                return Ok(auth.SignUp(u));
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
        [Route("adminsecuritycheck")]
        public IActionResult SecurityCheck(SecurityTable securityIdentity)
        {
            try
            {
                return Ok(auth.CheckSecurityIdentity(securityIdentity));
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
        [HttpGet("getuser/{userid}")]
        public IActionResult GetUser(int userid)
        {
            try
            {
                return Ok(auth.GetUser(userid));
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
        [HttpGet("{emailId}")]
        public IActionResult UserExists(string emailId)
        {
            try
            {
                return Ok(auth.CheckUserExists(emailId));
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
        public IActionResult Put(User user)
        {
            try
            {
                auth.updateUser(user);
                return Ok();
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
        [HttpGet("getuserid/{emailid}")]
        public IActionResult GetUserId(string emailid)
        {
            try
            {
                return Ok(auth.GetUserId(emailid));
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
