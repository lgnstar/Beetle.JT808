using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808.Messages
{
    [MessageType(ID = 0x8100)]
    public class ClientRegisterResponse
    {
        [UInt16Handler]
        public ushort BusinessNO { get; set; }
        [ByteHandler]
        public byte Result { get; set; }
        [GBKHandler]
        public string Signature { get; set; }
    }
}
