namespace Ecommerce_Angular_DotNetAPI.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string detail = null) : 
            base(statusCode, message)
        {
            this.detail = detail;
        }
        public string detail { get; set; }
    }
}
