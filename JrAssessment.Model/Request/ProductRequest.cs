using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Request
{
    public class ProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
