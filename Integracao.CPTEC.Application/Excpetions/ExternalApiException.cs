using System.Net;

namespace Integracao.CPTEC.Application.Excpetions
{
    public class ExternalApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ExternalApiException(string message): base(message) { }

        public ExternalApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
