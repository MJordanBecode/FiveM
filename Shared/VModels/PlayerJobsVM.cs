using ClassLibrary1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FivemCsharpCore.Models
{
    public class PlayerJobsVM : BaseModelVM
    {
        public Guid PlayerID { get; set; }
        public Guid JobID { get; set; }
        public Guid JobGradeID { get; set; }

        public  PlayersVM Player { get; set; }
        public  JobsVM Job { get; set; }
        public  JobGradesVM JobGrade { get; set; }
    }
}
