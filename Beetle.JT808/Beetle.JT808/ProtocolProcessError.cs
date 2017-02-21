using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public class ProtocolProcessError : Exception
    {
        public ProtocolProcessError()
        {
        }

        public ProtocolProcessError(string error) : base(error) { }

        public ProtocolProcessError(string error, Exception e) : base(error, e) { }
    }
}
