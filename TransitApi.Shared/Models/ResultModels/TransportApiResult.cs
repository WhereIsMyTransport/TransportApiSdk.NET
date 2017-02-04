using System.Net;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class TransportApiResult
        <T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }

        public TransportApiResult()
        {
            IsSuccess = false;
            Error = string.Empty;
        }
    }
}
