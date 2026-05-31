using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class BankTransactionsVM : BaseModelVM
    {
        public required string Type { get; set; }
        public long Amout { get; set; }
        public required string Description { get; set; }
        public Guid BankAccountID { get; set; }
        public required BankAccountVM BankAccount { get; set; }
    }
}
