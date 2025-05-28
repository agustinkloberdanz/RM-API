namespace RM_API.Models
{
    public class ResponseCollection<T> : Response
    {
        public List<T> Collection { get; set; }
        public ResponseCollection()
        {
            Collection = new List<T>();
        }
        public ResponseCollection(int code, string msg, List<T> collection)
        {
            StatusCode = code;
            Message = msg;
            this.Collection = collection;
        }
        public ResponseCollection(int code, string msg, Boolean changeBool)
        {
            StatusCode = code;
            Message = msg;
            IsSuccess = changeBool;
        }

    }
}
