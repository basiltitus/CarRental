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
        [Route("addcardetails")]
        public IActionResult Post(Car car)
        {
            try
            {
                    return Ok(carOperation.AddCar(car));
             }
            catch (Exception e) { return StatusCode(500); }

        }
        [HttpGet("checkregnoexists/{regno}")]
        public IActionResult CheckRegNoExists(string regno) {
            try
            {
                return Ok(carOperation.CheckRegNoExists(regno));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("addmodeldetails")]
        public IActionResult Post(CarModel car)
        {
                try
                {
                    return Ok(carOperation.AddCarModel(car));
                }
                catch (Exception) { return StatusCode(500); 
                }
           
        }
        [HttpGet("getcarlist")]
        public IActionResult GetCarList()
        {
            try
            {
                return Ok(carOperation.GetCarList());

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("getmodellist")]
        public IActionResult Get()
        {

            try
            {
                return Ok(carOperation.GetList());
            }
            catch (Exception e) {
                return StatusCode(500); }

            
        }
     
        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            
                try
                {
                    return Ok(carOperation.GetCar(id));
                }
                catch (Exception e) { return StatusCode(500); }
            
        }
        [HttpGet("getlist/{transmission}/{varient}")]
        public IActionResult GetCar(int transmission,int varient)
        {
            return Ok(carOperation.GetCar(transmission, varient));
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
        [Route("updatecarmodel")]
        public IActionResult UpdateCar(CarModel car)
        {
            try
                {
                    return Ok(carOperation.UpdateCar(car));
                }
                catch (Exception e) { return StatusCode(500); }
           

        }
        [HttpPost]
        [Route("deletecarmodel")]
        public IActionResult Deletecar([FromBody] int id)
        {
            
                try
                {
                    return Ok(carOperation.DeleteCar(id));
                }
                catch (Exception e) { return StatusCode(500); }
            
        }


    }
}