using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Operations;
using Microsoft.Extensions.Configuration;

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

            return Ok(auth.SignIn(item.UserName, item.Password));
        }
        [HttpPost]
        [Route("signup")]
        public IActionResult Register(User u)
        {
            return Ok(auth.SignUp(u));
        }
        [HttpPost]
        [Route("adminsecuritycheck")]
        public IActionResult SecurityCheck(SecurityTable securityIdentity)
        {
            return Ok(auth.CheckSecurityIdentity(securityIdentity));
        }
    }
}
