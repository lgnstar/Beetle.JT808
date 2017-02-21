using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public interface IProtocolBuffer
    {
        bool Import(byte value);
        int Import(byte[] data, int offset, int count);

        void Reset();
        int Length { get; }
        void SetLength(int length);
        int Postion { get; set; }
        byte[] Array { get; }

        void Write(byte[] data);
        void Write(byte data);
        void Write(ushort value);
        void Write(uint value);
        void WriteBCD(string value);
        void WriteASCII(string value, int length);
        int WriteGBK(string value);
        void WriteSubBuffer(IProtocolBuffer buffer);
        void WriteTag();

        byte Read();
        byte[] Read(int length);
        ushort ReadUInt16();
        uint ReadUInt();
        string ReadBCD(int length);
        string ReadASCII(int length);
        string ReadGBK(int length = -1);
        void ReadSubBuffer(IProtocolBuffer buffer, int count);
    }
}
