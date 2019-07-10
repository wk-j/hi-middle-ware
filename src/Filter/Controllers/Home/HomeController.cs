using Filter.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filter.Controllers.Home {
    public class Model {
        public string Name { set; get; }
    }

    [Route("api/[controller]/[action]")]
    [MyResultFilter]
    public class HomeController : ControllerBase {
        [HttpGet]
        public ActionResult<string> Go() {
            return "Hello, world!";
        }

        [HttpGet]
        public ActionResult<Model> GoModel() {
            return new Model { Name = "wk" };
        }
    }
}