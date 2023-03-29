using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Models.Exceptions
{
    public class DepositLimitExceededException : ApplicationException
    {
        public DepositLimitExceededException() : base("Can not deposit more than $1000 in single transaction.")
        {

        }
    }
}
