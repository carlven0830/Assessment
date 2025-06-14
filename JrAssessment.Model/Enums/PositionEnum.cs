using System.ComponentModel.DataAnnotations;

namespace JrAssessment.Model.Enums
{
    public enum PositionEnum
    {
        [Display(Name = "Web Developer")]
        WebDeveloper,
        [Display(Name = "Network Engineer")]
        NetworkEngineer,
        [Display(Name = "Computer Technician")]
        ComputerTechnician,
        [Display(Name = "Project Manager")]
        ProjectManager
    }
}
