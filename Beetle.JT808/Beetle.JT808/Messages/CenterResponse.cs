using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808.Messages
{

    [MessageType(ID = 0x0008)]
    public class CenterResponse
    {
        [UInt16Handler]
        public ushort BussinessNO { get; set; }
        [UInt16Handler]
        public ushort ResultID { get; set; }
        [ByteHandler]
        public ResultType Result { get; set; }
    }
}
