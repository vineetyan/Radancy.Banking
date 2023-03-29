using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Models.Exceptions
{
    public class WithdrawLimitExceededException : ApplicationException
    {
        public WithdrawLimitExceededException() : base("Withdraw limit exceeded. Can not withdraw more than 90 percent of balance.")
        {

        }
    }
}
