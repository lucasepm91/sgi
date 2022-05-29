using System.Runtime.Serialization;

namespace Sgi.CrossCutting.Exceptions
{
    [Serializable]
    public class NaoAutorizadoException : Exception
    {        
        public object Erro { get; }

        public NaoAutorizadoException(object erro)
        {
            Erro = erro;
        }

        public NaoAutorizadoException() { }

        public NaoAutorizadoException(string message) : base(message) { Erro = message; }

        protected NaoAutorizadoException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public NaoAutorizadoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
