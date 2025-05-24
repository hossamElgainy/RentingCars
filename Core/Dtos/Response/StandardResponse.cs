using System.Net;

namespace Core.Dtos.Response
{
    public class StandardResponse<T>
    {
        public StandardResponse()
        {

        }
        public StandardResponse(string message)
        {
            Status = true;
            Message = message;
        }
        
        public StandardResponse(T? data)
        {
            Status = true;
            Data = data;
        }
        public StandardResponse(string error, bool status)
        {
            Error = error;
            Status = status;
        }
        public StandardResponse(T? data, string message)
        {
            Data = data;
            Message = message;
            Status = true;
        }
        public StandardResponse(T? data, int totalCount)
        {
            Data = data;
            CountIfPaginated = totalCount;
            Status = true;
        }
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public T? Data { get; set; }
        public int CountIfPaginated { get; set; }
    }
}
