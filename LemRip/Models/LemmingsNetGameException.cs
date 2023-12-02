using System;
using System.Runtime.Serialization;

namespace Lemmings.NET.Models;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
[Serializable()]
public class LemmingsNetGameException : Exception
{
    public LemmingsNetGameException(string message) : base(message) { }

    public LemmingsNetGameException(string message, Exception ex) : base(message, ex) { }
}
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
