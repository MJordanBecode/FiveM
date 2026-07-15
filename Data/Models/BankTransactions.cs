using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class BankTransactions : BaseModel
    {
        public  string Type { get; set; }
        public long Amout { get; set; }
        public  string Description { get; set; }
        public Guid BankAccountID { get; set; }
        public  BankAccounts BankAccount { get; set; }
    }
}
