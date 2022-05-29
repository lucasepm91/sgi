using System.Runtime.Serialization;

namespace Sgi.CrossCutting.Exceptions
{
    [Serializable]
    public class RegraDeNegocioException : Exception
    {
        public object Erro { get; }

        public RegraDeNegocioException(object erro)
        {
            Erro = erro;
        }

        public RegraDeNegocioException() { }

        public RegraDeNegocioException(string message) : base(message) { Erro = message; }

        protected RegraDeNegocioException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public RegraDeNegocioException(string message, Exception innerException) : base(message, innerException) { }
    }
}
