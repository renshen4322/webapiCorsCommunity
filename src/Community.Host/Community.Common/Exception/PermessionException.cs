using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common.Exception
{
    public class PermessionException : System.Exception
    {
        public PermessionException() : base() { }
        public PermessionException(string message) : base(message) { }
        public PermessionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public PermessionException(string message, System.Exception inner) : base(message, inner) { }
    }
}
