using System.ComponentModel;

namespace Integracao.CPTEC.Domain.Entities
{
    public class LogErro
    {
        public string Description { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateHour { get; set; }
    }
}
