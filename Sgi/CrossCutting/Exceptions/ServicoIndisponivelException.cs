using System.Runtime.Serialization;

namespace Sgi.CrossCutting.Exceptions
{
    [Serializable]
    public class ServicoIndisponivelException : Exception
    {
        public object Erro { get; }

        public ServicoIndisponivelException(object erro)
        {
            Erro = erro;
        }

        public ServicoIndisponivelException() { }

        public ServicoIndisponivelException(string message) : base(message) { }

        protected ServicoIndisponivelException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ServicoIndisponivelException(string message, Exception innerException) : base(message, innerException) { }
    }
}
