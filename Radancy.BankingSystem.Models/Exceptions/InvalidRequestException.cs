using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Models.Exceptions
{
    public class InvalidRequestException : ApplicationException
    {
        public InvalidRequestException(string message) : base(message)
        {

        }
    }
}
