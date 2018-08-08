using System;
using System.Runtime.Serialization;

namespace BusinessTripModels.Exception
{
    [Serializable]
    public class InternetException : System.Exception
    {
        public InternetException()
        {
        }

        public InternetException(string message) : base(message)
        {
        }

        public InternetException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected InternetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}