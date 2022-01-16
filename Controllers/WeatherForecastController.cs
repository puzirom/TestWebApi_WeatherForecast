using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Contracts;
using WeatherForecast.Processors;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<ForecastResult>> Get()
        {
            try
            {
                var result = WeatherProcessor.GetForecast();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                // better to use exception filter class instead of try-catch - but it is out of scope
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }
    }
}
