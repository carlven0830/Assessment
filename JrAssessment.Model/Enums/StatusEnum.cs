using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Enums
{
    public enum StatusEnum
    {
        Pending,
        [Display(Name = "In Progress")]
        InProgress,
        Completed
    }
}
