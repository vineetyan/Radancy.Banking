using Radancy.BankingSystem.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Models.ViewModels
{
    public class ApiResponse
    {
        public IList<Error> Errors { get; set; }
    }
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public IList<Error> Errors { get; set; }
    }
    public class Error
    {
        public Error()
        {
        }


        public Error(int code, string message, string description)
        {
            Code = code;
            Message = message;
            Description = description;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Error))
            {
                return false;
            }
            return (this.Code == ((Error)obj).Code
                && this.Description == ((Error)obj).Description
                && this.Message == ((Error)obj).Message);
        }

        public static Error InvalidRequestError(string description)
        {
            return new Error()
            {
                Code = ErrorConstants.InvalidRequestInputCode,
                Message = ErrorConstants.InvalidRequestInputMessage,
                Description = description
            };
        }

        public static Error NotFoundError(string description)
        {
            return new Error()
            {
                Code = ErrorConstants.NotFoundInputCode,
                Message = ErrorConstants.NotFoundInputMessage,
                Description = description
            };
        }

        public static Error UnhandledError()
        {
            return new Error()
            {
                Code = ErrorConstants.UnhandledErrorCode,
                Message = ErrorConstants.UnhandledErrorCodeMessage,
                Description = ErrorConstants.UnhandledErrorCodeDescription
            };
        }
    }
    }
