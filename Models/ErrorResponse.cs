namespace net_core_based.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Errors { get; set; }
    }
}
