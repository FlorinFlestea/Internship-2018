using System;
using System.Runtime.Serialization;

namespace BusinessTripModels.Exception
{ 
    [Serializable]
    public class DatabaseException : System.Exception
    {
        public DatabaseException()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}