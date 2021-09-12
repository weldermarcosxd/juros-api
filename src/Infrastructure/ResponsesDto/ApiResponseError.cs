namespace Infrastructure.ResponsesDto
{
    public class ApiResponseError : ApiResponse
    {
        public ApiResponseError(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
