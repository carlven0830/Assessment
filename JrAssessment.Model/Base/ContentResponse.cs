using System.Net;

namespace JrAssessment.Model.Base
{
    public class ContentResponse<T> : BaseResponse
    {
        public T? Content { get; set; }
        public static ContentResponse<T> Add(HttpStatusCode status, string message, T? content, string token = "")
        {
            return new ContentResponse<T>
            {
                Status  = status,
                Message = message,
                Content = content,
                Token   = token
            };
        }
    }
}
