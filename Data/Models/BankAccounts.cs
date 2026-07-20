using System;
using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data.Models
{
    public class BankAccounts : BaseModel
    {
        public  string Pin { get; set; }
        public bool IsActived { get; set; } = true;
        public long Balance { get; set; }

        public  Guid PlayerID { get; set; }

        public  Players Player { get; set; }

    }
}
