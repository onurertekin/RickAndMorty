namespace DomainService.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public BusinessException(int statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.ErrorMessage = message;
        }
    }
}
