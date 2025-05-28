namespace RM_API.Models
{
    public class ResponseModel<T> : Response
    {
        public T Model { get; set; }
        public ResponseModel() { }
        public ResponseModel(int code, string msg, T model)
        {
            StatusCode = code;
            Message = msg;
            Model = model;
        }

        public ResponseModel(int code, string msg, Boolean changeBool)
        {
            StatusCode = code;
            Message = msg;
            IsSuccess = changeBool;
        }

        public ResponseModel(int code, string msg, Boolean changeBool, T model)
        {
            StatusCode = code;
            Message = msg;
            IsSuccess = changeBool;
            Model = model;
        }
    }
}
