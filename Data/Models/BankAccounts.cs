using FivemCsharpCore.Models;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class BankAccounts : BaseModel
    {

        public required string Pin { get; set; }
        public bool IsActived { get; set; } = true;
        public long Balance { get; set; }

        public required int PlayerID { get; set; }

        public required Players Player { get; set; }

    }
}
