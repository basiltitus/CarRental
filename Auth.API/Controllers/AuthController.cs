using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Server.Library;
namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        Authentication auth;
        public AuthController()
        {
            auth = new Authentication();
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //Login
        [HttpPost("{userName}")]
        public IActionResult Login(string userName, UserTable u)
        {
            
            return Ok(auth.SignIn(userName,u.Password));
        }
        [HttpPost]
        public IActionResult Register(UserTable u)
        {
            return Ok(auth.SignUp(u));
        }
    }
}
