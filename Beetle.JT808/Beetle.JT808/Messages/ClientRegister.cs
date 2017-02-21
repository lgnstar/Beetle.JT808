using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Beetle.JT808.Messages
{
    [MessageType(ID = 0x0100)]
    public class ClientRegister 
    {
        [UInt16Handler]
        public ushort Province { get; set; }
        [UInt16Handler]
        public ushort City { get; set; }
        [ASCIIHandler(5)]
        public string Provider { get; set; }
        [ASCIIHandler(8)]
        public string DeviceNumber { get; set; }
        [ASCIIHandler(7)]
        public string DeviceID { get; set; }
        [ByteHandler]
        public byte Color { get; set; }
        [GBKHandler]
        public string PlateNumber { get; set; }
    }
}
