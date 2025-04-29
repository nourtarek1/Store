using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErroresModels
{
    public class ValidtionErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMesage { get; set; } = "Validation Error";
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
