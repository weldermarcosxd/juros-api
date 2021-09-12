using System;
using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    [Serializable]
    internal class InvalidTaxaJurosServiceUrlException : Exception
    {
        public InvalidTaxaJurosServiceUrlException() : base("A url do serviço de taxa de juros não foi informada ou não é válida")
        {
        }

        public InvalidTaxaJurosServiceUrlException(string message) : base(message)
        {
        }

        public InvalidTaxaJurosServiceUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTaxaJurosServiceUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}