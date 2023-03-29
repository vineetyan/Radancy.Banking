using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Models.ViewModels
{
    public class CreateAccountViewModel
    {
         public decimal InitialBalance { get; set; }
        public string AccountNumber { get; set; }
        public string ClientId
        {
            get; set;
        }
    }
}
