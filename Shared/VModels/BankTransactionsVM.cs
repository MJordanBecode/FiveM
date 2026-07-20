using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
{
    public class BankTransactionsVM : BaseModelVM
    {
        public  string Type { get; set; }
        public long Amount { get; set; }
        public  string Description { get; set; }
        public Guid BankAccountID { get; set; }
        public  BankAccountsVM BankAccount { get; set; }
    }
}
