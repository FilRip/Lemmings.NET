using System;
using System.Runtime.Serialization;

namespace Lemmings.NET.Models
{
    [Serializable()]
    public class LemmingsNetGameException : Exception
    {
        public LemmingsNetGameException(string message) : base(message) { }

        public LemmingsNetGameException(string message, Exception ex) : base(message, ex) { }

        protected LemmingsNetGameException(SerializationInfo serializationEntries, StreamingContext context) : base(serializationEntries, context) { }
    }
}
