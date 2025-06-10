using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Database
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string CreateBy { get; set; }
        DateTime CreateDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        bool IsEnabled { get; set; }
    }

    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
