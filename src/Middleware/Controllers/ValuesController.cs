using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.Controllers {
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller {
        [HttpGet]
        public string Hi() {
            return "Hi";
        }
    }
}
