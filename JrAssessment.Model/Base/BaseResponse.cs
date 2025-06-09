using System.Net;

namespace JrAssessment.Model.Base
{
    public class BaseResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; }   = string.Empty;
    }
}
