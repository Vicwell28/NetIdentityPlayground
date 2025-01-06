using System.Collections.Generic;
using System.Linq;

namespace NetIdentityPlayground.Application.Common.DTOs
{
    public class BaseResponseDto
    {
        public bool IsSuccess => Errors == null || !Errors.Any();
        public string? Message { get; set; }
        public List<ErrorDto>? Errors { get; set; }
        public string? Code { get; set; }
        public int StatusCode { get; set; } = 200;

        public virtual void SetSuccess(string? message = null, int? statusCode = null, string? code = null)
        {
            Message = message;
            Errors = null;
            Code = code;
            StatusCode = statusCode ?? 200;
        }

        public virtual void SetError(string? message, List<ErrorDto>? errors = null, int? statusCode = null, string? code = null)
        {
            Message = message;
            Errors = errors ?? new List<ErrorDto>();
            Code = code;
            StatusCode = statusCode ?? 200;
        }
    }
}
