using AutoMapper;
using Integracao.CPTEC.Domain.Exceptions;
using System.Net;

namespace Integracao.CPTEC.Application
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
    }

    public class SuccessResponse : Response
    {
        public bool Success { get; set; } = true;
        public object Data { get; set; }
    }

    public class ErrorResponse : Response
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }

    public static class ResponseHelper 
    {
        public static Response AutoMapperValidationException(AutoMapperMappingException ex)
        => ex.InnerException is DomainException
                            ? new ErrorResponse
                            {
                                StatusCode = HttpStatusCode.UnprocessableEntity,
                                Message = "Validation failed.",
                            }
                            : (Response)new ErrorResponse
                            {
                                StatusCode = HttpStatusCode.InternalServerError,
                                Message = "Internal server error.",
                            };
    }
}

