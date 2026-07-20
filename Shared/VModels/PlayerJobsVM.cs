using System;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
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
