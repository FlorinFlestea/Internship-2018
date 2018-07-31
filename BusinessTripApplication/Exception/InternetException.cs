using System;
using System.Runtime.Serialization;

[Serializable]
internal class InternetException : Exception
{
    public InternetException()
    {
    }

    public InternetException(string message) : base(message)
    {
    }

    public InternetException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InternetException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}