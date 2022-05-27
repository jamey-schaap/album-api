using System;
using Album.Api.Models;
using Album.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Album.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HelloController : Controller
  {
    private readonly ILogger _logger;
    public HelloController(ILogger<HelloController> logger)
    {
      _logger = logger;
    }

    // /api/hello?name=Jack
    [HttpGet]
    public IActionResult Get([FromQuery] string name)
    {
      _logger.LogInformation($"{DateTime.Now} [GET] name='{name}'");
      GreetDto greet = new GreetDto()
      {
        greet = GreetingService.Greet(name)
      };
      return Ok(greet);
    }

    // api/hello/Jack
    // [HttpGet("{name?}")]
    // public IActionResult Get(string name)
    // {
    //   _logger.LogInformation($"{DateTime.Now} [GET] name='{name}'");
    //   GreetDto greet = new GreetDto()
    //   {
    //     greet = GreetingService.Greet(name)
    //   };
    //   return Ok(greet);
    // }
  }
}
