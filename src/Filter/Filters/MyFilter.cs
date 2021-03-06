using Filter.Controllers.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Filter.Filters {


    public class MyFilter : IActionFilter {
        public void OnActionExecuted(ActionExecutedContext context) {

        }

        public void OnActionExecuting(ActionExecutingContext context) {
        }
    }

    public class MyResultFilter : ResultFilterAttribute {
        private readonly ILogger<MyResultFilter> _logger;

        public MyResultFilter(ILogger<MyResultFilter> logger) {
            _logger = logger;
        }

        public override void OnResultExecuting(ResultExecutingContext context) {
            var body = context.Result;
            if (body is ObjectResult o) {
                if (o.Value is Model model) {
                    _logger.LogInformation("name - {0}", model.Name);
                }
            }
            base.OnResultExecuting(context);
        }
    }
}