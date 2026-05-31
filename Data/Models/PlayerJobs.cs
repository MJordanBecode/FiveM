using ClassLibrary1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FivemCsharpCore.Models
{
    public class PlayerJobs : BaseModel
    {
        public Guid PlayerID { get; set; }
        public Guid JobID { get; set; }
        public Guid JobGradeID { get; set; }

        public required Players Player { get; set; }
        public required Jobs Job { get; set; }
        public required JobGrades JobGrade { get; set; }
    }
}
