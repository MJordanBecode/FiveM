using System;
using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data.Models
{
    public class PlayerJobs : BaseModel
    {
        public Guid PlayerID { get; set; }
        public Guid JobID { get; set; }
        public Guid JobGradeID { get; set; }

        public  Players Player { get; set; }
        public  Jobs Job { get; set; }
        public  JobGrades JobGrade { get; set; }
    }
}
