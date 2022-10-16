using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adaca.Api.Infrastructure.Exceptions
{
    public class ApiProcessingException : Exception
    {
        public int StatusCode { get; }
        public string MessageDetail { get; set; }

        public ApiProcessingException(HttpStatusCode statusCode, string message = null, string messageDetail = null) : base(message)
        {
            StatusCode = (int)statusCode;
            MessageDetail = messageDetail;
        }
    }

    public class ExceptionMessage
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string MessageDetail { get; set; }

        public ExceptionMessage() { }

        public ExceptionMessage(string message, string messageDetail)
        {
            Message = message;
            MessageDetail = messageDetail;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

