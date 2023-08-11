using Integracao.CPTEC.Domain.Exceptions;

namespace Integracao.CPTEC.Domain.Entities
{
    public abstract class Entity
    {
        public List<string> Errors { get; protected set; } = new();

        protected void ValidadeErrors()
        {
            if (Errors.Any())
                throw new DomainException(string.Join(" ", Errors));
        }
    }
}
