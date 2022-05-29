using System.Runtime.Serialization;

namespace Sgi.CrossCutting.Exceptions
{
    [Serializable]
    public class NaoEncontradoException : Exception
    {
        public object Erro { get; }

        public NaoEncontradoException(object erro)
        {
            Erro = erro;
        }

        public NaoEncontradoException() { }

        public NaoEncontradoException(string message) : base(message) { Erro = message; }

        protected NaoEncontradoException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public NaoEncontradoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
