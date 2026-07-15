using FivemCsharpCore.Models;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class BankAccountVM : BaseModelVM
    {

        public  string Pin { get; set; }
        public bool IsActived { get; set; } = true;
        public long Balance { get; set; }

        public  int PlayerID { get; set; }

        public  PlayersVM Player { get; set; }

    }
}
