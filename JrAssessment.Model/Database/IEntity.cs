using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Database
{
    public interface IEntity
    {
        Guid ID { get; set; }
        string CreateBy { get; set; }
        DateTime CreateDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }

    public class Entity : IEntity
    {
        public Guid ID { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
