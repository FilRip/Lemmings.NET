using System;

namespace Lemmings.NET.Exceptions;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
[Serializable()]
public class LemmingsNetException : Exception
{
    public LemmingsNetException(string message) : base(message) { }

    public LemmingsNetException(string message, Exception ex) : base(message, ex) { }
}
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
