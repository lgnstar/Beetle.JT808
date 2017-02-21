using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public interface IMessage
    {
        ushort ID { get; set; }
        MessageBodyAttributes Property { get; set; }
        string SIM { get; set; }
        ushort BussinessNO { get; set; }
        PacketInfo Packet { get; set; }
        void Save(IProtocolBuffer buffer);
        void Load(IProtocolBuffer buffer);
        object Body { get; set; }
        byte CRC { get; set; }
        T GetBody<T>();

    }
}
