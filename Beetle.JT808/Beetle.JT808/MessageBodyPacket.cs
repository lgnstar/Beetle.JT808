using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public class PacketInfo
    {
        public ushort Count { get; set; }

        public ushort Index { get; set; }

        public void Save(IProtocolBuffer buffer)
        {
            Count = Core.SwapUInt16(Count);
            Index = Core.SwapUInt16(Index);
        }

        public void Load(IProtocolBuffer buffer)
        {
            byte[] data = buffer.Read(2);
            Count = BitConverter.ToUInt16(data, 0);
            Count = Core.SwapUInt16(Count);

            data = buffer.Read(2);
            Index = BitConverter.ToUInt16(data, 0);
            Index = Core.SwapUInt16(Count);
        }
    }
}
