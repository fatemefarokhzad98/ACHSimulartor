using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.HttpManager
{ 
    public class JsonResponseStatus
    {
        public static JsonResult Success()
        {
            return new JsonResult(new { status = "Success" ,code="200"});
        }

        public static JsonResult Success(string message)
        {
            return new JsonResult(new { status = "Success", message , code = "200" });
        }

        public static JsonResult Success(object returnData, string? message )
        {
            return new JsonResult(new { status = "Success", data = returnData, message, code = "200" });
        }

        public static JsonResult NotFound()
        {
            return new JsonResult(new { status = "NotFound", code = "404" });
        }

        public static JsonResult NotFound(string message)
        {
            return new JsonResult(new { status = "NotFound", message , code = "404" });
        }

        public static JsonResult NotFound(object? returnData, string? message )
        {
            return new JsonResult(new { status = "NotFound", data = returnData, message, code = "404" });
        }

        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error",code = "500" });
        }

        public static JsonResult Error(string message)
        {
            return new JsonResult(new { status = "Error", message, code = "500" });
        }

        public static JsonResult Error(object? returnData, string? message)
        {
            return new JsonResult(new { status = "Error", data = returnData, message , code = "500" });
        }


        public static JsonResult BadRequest()
        {
            return new JsonResult(new { status = "Error", code = "400" });
        }

        public static JsonResult BadRequest(string message)
        {
            return new JsonResult(new { status = "Error", message, code = "400" });
        }

        public static JsonResult BadRequest(object? returnData, string? message)
        {
            return new JsonResult(new { status = "Error", data = returnData, message, code = "400" });
        }


    }
}
