using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Logger _logger;

        public TestController(Logger logger)
        {
            _logger = logger;
        }

        [HttpGet("get")]
        public String Get()
        {
            _logger.Information("App is working");
            return "App is working";
        }
    }
}
