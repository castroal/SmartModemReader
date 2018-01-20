using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartModemReader.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly DslDataReader reader;

        public ValuesController(DslDataReader reader)
        {
            this.reader = reader;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // return Ok( await reader.ReadSampleDataAsync());
            return Ok( await reader.ReadDataAsync());
        }
    }
}
