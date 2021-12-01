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
using Server.API.Models.ViewModels;
using System.IO;

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
        [HttpGet("checkregnoexists/{regno}")]
        public IActionResult CheckRegNoExists(string regno)
        {
            try
            {
                return Ok(carOperation.CheckRegNoExists(regno));
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
        [Route("addmodeldetails")]
        public IActionResult Post(CarModel car)
        {
            try
            {
                return Ok(carOperation.AddCarModel(car));
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
        [HttpGet("getcarlist")]
        public IActionResult GetCarList()
        {
            try
            {
                return Ok(carOperation.GetCarList());

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
        [HttpPost("getavailablecarlist")]
        public IActionResult GetAvailableCarList(DatesVM dates)
        {
            try
            {
                return Ok(carOperation.GetAvailableCarList(dates.FromDate, dates.ToDate));

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
        [HttpGet]
        [Route("getmodellist")]
        public IActionResult Get()
        {

            try
            {
                return Ok(carOperation.GetList());
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
        [HttpGet("carvarient")]
        public IActionResult GetCarVarient()
        {
            try
            {
                return Ok(carOperation.GetCarVarients());
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
        [HttpPost]
        [Route("updatecarmodel")]
        public IActionResult UpdateCarModel(CarModel car)
        {
            try
            {
                return Ok(carOperation.UpdateCarModel(car));
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
        [HttpPost]
        [Route("updatecar")]
        public IActionResult UpdateCar(Car car)
        {
            try
            {
                return Ok(carOperation.UpdateCar(car));
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
        /*[HttpPost]
        [Route("deletecarmodel")]
        public IActionResult DeleteCarModel([FromBody] int id)
        {

            try
            {
                return Ok(carOperation.DeleteCarModel(id));
            }
            catch (Exception e) {
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }*/
        [HttpGet]
        [Route("changecaractive/{id}/{active}")]
        public IActionResult ChangeCarActive(int id, bool active)
        {

            try
            {
                return Ok(carOperation.ChangeCarActive(id, active));
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

        [HttpGet("getcarmodel/{id}")]
        public IActionResult GetCarModel(int id)
        {
            try
            {
                return Ok(carOperation.GetCarModel(id));
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
        [HttpGet("getcarjoined/{id}")]
        public IActionResult GetCarJoined(int id)
        {
            try
            {
                return Ok(carOperation.GetCarJoined(id));
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
        [HttpGet("getcar/{id}")]
        public IActionResult GetCar(int id)
        {
            try
            {
                return Ok(carOperation.GetCar(id));
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