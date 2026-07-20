using Shared.VModels;
using System.ComponentModel.DataAnnotations;

namespace Shared.VModels
{
    public class BankAccountsVM : BaseModelVM
    {
        public  string Pin { get; set; }
        public bool IsActived { get; set; } = true;
        public long Balance { get; set; }

        public  int PlayerID { get; set; }

        public  PlayersVM Player { get; set; }

    }
}
