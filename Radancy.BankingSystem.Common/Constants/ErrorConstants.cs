namespace Radancy.BankingSystem.Common.Constants
{
    public static class ErrorConstants
    {
        public const int InvalidRequestInputCode = 1001;
        public const string InvalidRequestInputMessage = "Request inputs are invalid";

        public const int NotFoundInputCode = 1002;
        public const string NotFoundInputMessage = "Requested resource not found";

        public const int UnhandledErrorCode = 1099;
        public const string UnhandledErrorCodeDescription = "Unhandled error occured while processing request.";
        public const string UnhandledErrorCodeMessage = "Unhandled error occured while processing request.";

    }
}
