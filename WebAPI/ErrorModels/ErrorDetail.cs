using System.Text.Json;

namespace WebAPI.ErrorModels
{
    public class ErrorDetail
    {
        public ErrorDetail(string message,int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
        public int StatusCode { get;}
        public string Message { get;}
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
