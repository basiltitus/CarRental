using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Operations;
using Server.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*[Authorize(Roles = "Admin")]*/
    public class CarController : ControllerBase
    {
        private IConfiguration Configuration;
        public CarOperation carOperation;
        public CarController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            carOperation = new CarOperation(Configuration);
        }
        [HttpPost]
        [Route("adddetails")]
        public IActionResult Post(CarTable car)
        {

            return Ok(carOperation.AddCar(car));
        }
        [HttpGet]
        [Route("getlist")]
        public IActionResult Get()
        {
            return Ok(carOperation.GetList());
        }
        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            return Ok(carOperation.GetCar(id));
        }
        [HttpGet("carvarient")]
        public IActionResult GetCarVarient()
        {
            return Ok(carOperation.GetCarVarients());
        }
        [HttpPost]
        [Route("updatecar")]
        public IActionResult UpdateCar(CarTable car)
        {
            return Ok(carOperation.UpdateCar(car));

        }
        [HttpPost]
        [Route("deletecar")]
        public IActionResult Deletecar([FromBody]  int id)
        {
            return Ok(carOperation.DeleteCar(id));
        }
        
            
    }
}