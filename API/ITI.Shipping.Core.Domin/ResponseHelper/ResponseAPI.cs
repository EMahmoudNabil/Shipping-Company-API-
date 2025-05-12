using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.ResponseHelper;
public class ResponseAPI
{
    public ResponseAPI(int statusCode,string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetMessageFromStatusCode(StatusCode);
    }
    private string GetMessageFromStatusCode(int statusCode)
    {
        return StatusCode switch
        {
            200 => "Success",
            201 => "Created",
            204 => "No Content",
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Unknown"
        };
    }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}
