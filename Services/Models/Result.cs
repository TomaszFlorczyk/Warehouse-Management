using System.Net;

namespace WarehouseMenagementAPI.Services.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string? Message { get; set; }
    }
    public class Result<T> : Result
    {
        public T? Data { get; set; }
    }
}
