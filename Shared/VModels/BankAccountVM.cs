using FivemCsharpCore.Models;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class BankAccountVM : BaseModelVM
    {

        public required string Pin { get; set; }
        public bool IsActived { get; set; } = true;
        public long Balance { get; set; }

        public required int PlayerID { get; set; }

        public required PlayersVM Player { get; set; }

    }
}
