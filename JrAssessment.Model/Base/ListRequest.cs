using JrAssessment.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Base
{
    public class ListRequest
    {
        public long Pagenum { get; set; }
        public long PageSize { get; set; }
        public OrderByEnum Orderby { get; set; }
        public bool Asc { get; set; }
    }
}
