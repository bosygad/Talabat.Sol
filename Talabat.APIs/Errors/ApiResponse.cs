namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode {  get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMassegeForStatusCode(StatusCode);
        }
        private string? GetDefaultMassegeForStatusCode(int? statusCode)
        {
            //500 => internal Server Error
            //400 => Bad Request
            //401 =>  Unauthorized
            //404 => Not Found

            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You Are Not Authorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null,
            };
            
        }
    }
}
