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
 try
                {
                    try
                {
                    return Ok(carOperation.AddCar(car));
                }
                catch (Exception) { return StatusCode(500); }
                }
                catch (Exception) { return StatusCode(500); }
           
        }
        [HttpGet]
        [Route("getlist")]
        public IActionResult Get()
        {
           
                try
                {
                    return Ok(carOperation.GetList());
                }
                catch (Exception) { return StatusCode(500); }
            
        }
     
        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            
                try
                {
                    return Ok(carOperation.GetCar(id));
                }
                catch (Exception) { return StatusCode(500); }
            
        }
        [HttpGet("carvarient")]
        public IActionResult GetCarVarient()
        {
            try
            {
                return Ok(carOperation.GetCarVarients());
            }
            catch (Exception) { return StatusCode(500); }
        }
        [HttpPost]
        [Route("updatecar")]
        public IActionResult UpdateCar(CarTable car)
        {
            try
                {
                    return Ok(carOperation.UpdateCar(car));
                }
                catch (Exception) { return StatusCode(500); }
           

        }
        [HttpPost]
        [Route("deletecar")]
        public IActionResult Deletecar([FromBody] int id)
        {
            
                try
                {
                    return Ok(carOperation.DeleteCar(id));
                }
                catch (Exception) { return StatusCode(500); }
            
        }


    }
}