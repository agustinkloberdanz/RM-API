namespace RM_API.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Boolean IsSuccess { get; set; } = true;

        public Response() { }
        public Response(int code, string msg)
        {
            StatusCode = code;
            Message = msg;
        }
        public Response(int code, string msg, Boolean changeBool)
        {
            StatusCode = code;
            Message = msg;
            IsSuccess = changeBool;
        }
    }
}
