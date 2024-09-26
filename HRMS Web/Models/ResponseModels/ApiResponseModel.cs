using System.Net;
using System.Text.Json.Serialization;

namespace FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Models.ResponseModels
{
    public class ApiResponseModel
    {
        [JsonPropertyName("data")]
        public dynamic Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
