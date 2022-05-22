using System.Runtime.Serialization;

namespace Sgi.CrossCutting.Exceptions
{
    [Serializable]
    public class ErroInternoException : Exception
    {
        public object Erro { get; }

        public ErroInternoException(object erro)
        {
            Erro = erro;
        }

        public ErroInternoException() { }

        public ErroInternoException(string message) : base(message) { }

        protected ErroInternoException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ErroInternoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
