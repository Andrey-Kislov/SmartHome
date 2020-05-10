using System;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Presentation.API.Models;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    public class SmartHomeBaseController : ControllerBase
    {
        protected new OkObjectResult Ok(object value)
        {
            return base.Ok(new CustomApiResponse(value));
        }

        protected BadRequestObjectResult BadRequest(Exception exception)
        {
            return base.BadRequest(new CustomApiResponse(exception));
        }

        protected BadRequestObjectResult BadRequest(string errorMessage)
        {
            return base.BadRequest(new CustomApiResponse(errorMessage));
        }

        protected NotFoundObjectResult NotFound(string errorMessage = "Not found")
        {
            return base.NotFound(new CustomApiResponse(errorMessage));
        }
    }
}
