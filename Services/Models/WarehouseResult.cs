﻿using System.Net;

namespace WarehouseMenagementAPI.Services.Models
{
    public class WarehouseResult<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}