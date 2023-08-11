using Integracao.CPTEC.Domain.Exceptions;

namespace Integracao.CPTEC.Application.Excpetions
{
    public class UserMessageException : Exception
    {
        public UserMessageException(string error) :base(error) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new UserMessageException(error);
        }
    }
}
