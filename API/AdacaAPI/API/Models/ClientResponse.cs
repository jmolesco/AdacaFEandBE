using System.Collections.Generic;

namespace Adaca.Api.Models
{
    public class ClientResponse
    {

        public ClientResponse()
        {
            ValidationResult = new List<ValidationResult>();
        }
        public string Decision { get; set; }
        public List<ValidationResult> ValidationResult { get; set; }
    }

    public class ValidationResult
    {
        public string Rule { get; set; }
        public string Message { get; set; }
        public string Decision { get; set; }
    }
}
