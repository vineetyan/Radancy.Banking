using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Models.Exceptions
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException() : base("Insufficient Funds")
        {

        }
    }
}
