using JrAssessment.Model.Enums;
namespace JrAssessment.Model.Base
{
    public class ListRequest
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public OrderByEnum Orderby { get; set; } = OrderByEnum.CreateDate;
        public bool Asc { get; set; } = true;
        public bool IsPageList { get; set; } = false;
    }
}
