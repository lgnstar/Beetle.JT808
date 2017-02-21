using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808.Messages
{

    public enum ResultType : byte
    {
        Success = 0,
        Failure = 1,
        Error = 2,
        NotSupport = 3

    }
    [MessageType(ID = 0x0001)]
    public class ClientResponse
    {
        [UInt16Handler]
        public ushort BussinessNO { get; set; }
        [UInt16Handler]
        public ushort ResultID { get; set; }
        [ByteHandler]
        public ResultType Result { get; set; }
    }


}
