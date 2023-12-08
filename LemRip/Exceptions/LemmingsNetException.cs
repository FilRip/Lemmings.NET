#pragma warning disable S3925, S125 // "ISerializable" should be implemented correctly
using System;
//using System.Runtime.Serialization;

namespace Lemmings.NET.Exceptions;

[Serializable()]
public class LemmingsNetException : Exception
{
    public LemmingsNetException(string message) : base(message) { }

    public LemmingsNetException(string message, Exception ex) : base(message, ex) { }

    //protected LemmingsNetException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
#pragma warning restore S3925, S125 // "ISerializable" should be implemented correctly
