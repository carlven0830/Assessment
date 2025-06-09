using System.Net;

namespace JrAssessment.Model.Base
{
    public class ListResponse<T> : BaseResponse
    {
        public IEnumerable<T>? Content { get; set; }
        public long TotalPages { get; set; }
        public long TotalCount { get; set; }
        public static ListResponse<T> Add(HttpStatusCode status, string message, IEnumerable<T>? content, long totalpages, long totalCount)
        {
            return new ListResponse<T>
            {
                Status = status,
                Message = message,
                Content = content,
                TotalPages = totalpages,
                TotalCount = totalCount
            };
        }
    }
}
