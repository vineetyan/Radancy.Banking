using Radancy.BankingSystem.Models.Exceptions;
using Radancy.BankingSystem.Models.ViewModels;
using Radancy.BankingSystem.Models;

namespace Radancy.BankingSystem
{
    public class ExceptionHandler
    {
        public static ApiResponse<T> HandleException<T>(Exception exception, HttpResponse response)
        {
            if (exception is InvalidRequestException)
            {
                return new ApiResponseBuilder<T>()
                        .WithError(
                   Error.InvalidRequestError(exception.Message))
                        .WithHttpStatus(response, System.Net.HttpStatusCode.BadRequest)
                        .Build();
            }
            else if (exception is NotFoundException)
            {
                return new ApiResponseBuilder<T>()
                        .WithError(
                   Error.NotFoundError(exception.Message))
                        .WithHttpStatus(response, System.Net.HttpStatusCode.NotFound)
                        .Build();
            }
            else
            {
                return new ApiResponseBuilder<T>()
                            .WithError(
                       Error.UnhandledError())
                            .WithHttpStatus(response, System.Net.HttpStatusCode.InternalServerError)
                            .Build();
            }
        }

        public static ApiResponse HandleException(Exception exception, HttpResponse response)
        {
            if (exception is InvalidRequestException)
            {
                return new ApiResponseBuilder()
                        .WithError(
                   Error.InvalidRequestError(exception.Message))
                        .WithHttpStatus(response, System.Net.HttpStatusCode.BadRequest)
                        .Build();
            }
            else if (exception is NotFoundException)
            {
                return new ApiResponseBuilder()
                        .WithError(
                   Error.NotFoundError(exception.Message))
                        .WithHttpStatus(response, System.Net.HttpStatusCode.NotFound)
                        .Build();
            }
            else
            {
                return new ApiResponseBuilder()
                            .WithError(
                       Error.UnhandledError())
                            .WithHttpStatus(response, System.Net.HttpStatusCode.InternalServerError)
                            .Build();
            }
        }
    }
}
