using FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected ApiResponseModel Response;
        public BaseApiController()
        {
            Response = InitializeResponse(false);
        }

        protected ApiResponseModel InitializeResponse(bool handShake)
        {
            var response = new ApiResponseModel();
            response.Data = new
            {
                result = false,
                handShake
            };
            response.Status = HttpStatusCode.BadRequest;
            return response;
        }
        protected ApiResponseModel AddResponseMessage(ApiResponseModel model, string message, dynamic result, bool handShake,
            HttpStatusCode statusCode)
        {

            model.Data = new
            {
                result,
                handShake
            };
            model.Status = statusCode;
            model.Message = message;

            return model;
        }
    }
}
