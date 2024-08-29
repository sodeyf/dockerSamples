using Microsoft.AspNetCore.Mvc;

namespace DockerTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static int _result { get; set; } = 100;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            return _result++;
        }

        //[HttpGet]
        //public TestDTO Get()
        //{
        //    return new TestDTO { Version = "V1" };
        //}
    }
}
