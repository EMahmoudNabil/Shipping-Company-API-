using ITI.Shipping.Core.Domin.ResponseHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("errors/{statusCode}")]
[ApiController]
public class ErrorController:ControllerBase
{
    [HttpGet]
    public IActionResult Error(int statusCode)
    {
        return new ObjectResult(new ResponseAPI(statusCode));
    }
}
