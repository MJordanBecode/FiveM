using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class BankTransactions : BaseModel
    {
        public  string Type { get; set; }
        public long Amount { get; set; }
        public  string Description { get; set; }
        public Guid BankAccountID { get; set; }
        public  BankAccounts BankAccount { get; set; }
    }
}
