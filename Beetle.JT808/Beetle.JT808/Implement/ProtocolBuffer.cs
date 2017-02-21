using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public class ProtocolBuffer : IProtocolBuffer
    {
        private byte[] mArray = new byte[1024];

        private int mPostion;

        private int mLength;

        private bool mProtocolStart = false;

        public const byte PROTOBUF_TAG = 0x7e;

        public const byte REPLACE_TAG = 0x7d;

        public bool Import(byte value)
        {
            if (value == PROTOBUF_TAG)
            {
                OnWrite(value);
                if (!mProtocolStart)
                {
                    mProtocolStart = true;
                }
                else
                {
                    mPostion = 0;
                    return true;
                }
            }
            else
            {
                if (mProtocolStart)
                {
                    OnWrite(value);
                }
            }
            return false;
        }

        public int Import(byte[] data, int offset, int count)
        {
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                result++;
                byte value = data[offset + i];
                if (Import(value))
                    return result;
            }
            return -1;
        }

        private byte OnRead()
        {
            byte value = mArray[mPostion];
            mPostion++;
            return value;
        }

        public byte Read()
        {
            byte value = OnRead();
            if (value == REPLACE_TAG)
            {
                value = Read();
                if (value == 0x01)
                {
                    return REPLACE_TAG;
                }
                else if (value == 0x02)
                {
                    return PROTOBUF_TAG;
                }
            }
            return value;
        }

        public byte[] Read(int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = Read();
            }
            return result;
        }

        private ProtocolBuffer OnWrite(byte value)
        {
            mArray[mPostion] = value;
            mPostion++;
            mLength++;
            return this;
        }

        public void WriteTag()
        {
            OnWrite(PROTOBUF_TAG);
        }

        public void Write(byte data)
        {
            if (data == PROTOBUF_TAG)
            {
                OnWrite(REPLACE_TAG).OnWrite(0x02);
            }
            else if (data == REPLACE_TAG)
            {
                OnWrite(REPLACE_TAG).OnWrite(0x01);
            }
            else
            {
                OnWrite(data);
            }
        }

        public void Write(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Write(data[i]);
            }
        }

        public int Length { get { return mLength; } }

        public int Postion { get { return mPostion; } set { mPostion = value; } }

        public byte[] Array { get { return mArray; } }

        public void ReadSubBuffer(IProtocolBuffer buffer, int count)
        {
            Buffer.BlockCopy(mArray, mPostion, buffer.Array, 0, count);
            mPostion += count;
            buffer.SetLength(count);
            buffer.Postion = 0;
        }

        public void WriteSubBuffer(IProtocolBuffer buffer)
        {
            Buffer.BlockCopy(buffer.Array, 0, mArray, mPostion, buffer.Length);
            mPostion += buffer.Length;
            mLength += buffer.Length;
        }

        public void SetLength(int length)
        {
            mLength = length;
        }

        public void Reset()
        {
            mPostion = 0;
            mLength = 0;
            mProtocolStart = false;
        }

        public void Write(ushort value)
        {
            value = Core.SwapUInt16(value);
            byte[] data = BitConverter.GetBytes(value);
            Write(data);
        }

        public void Write(uint value)
        {
            value = Core.SwapUInt32(value);
            byte[] data = BitConverter.GetBytes(value);
            Write(data);
        }

        public void WriteBCD(string value)
        {
            byte[] data = str2Bcd(value);
            Write(data);
        }

        public ushort ReadUInt16()
        {
            byte[] data = Read(2);
            ushort result = BitConverter.ToUInt16(data, 0);
            return Core.SwapUInt16(result);
        }

        public uint ReadUInt()
        {
            byte[] data = Read(4);
            uint result = BitConverter.ToUInt32(data, 0);
            return Core.SwapUInt32(result);
        }

        public string ReadBCD(int length)
        {
            byte[] data = Read(length);
            return bcd2Str(data);
        }

        public byte[] str2Bcd(String asc)
        {
            int len = asc.Length;
            int mod = len % 2;
            if (mod != 0)
            {
                asc = "0" + asc;
                len = asc.Length;
            }
            byte[] abt = new byte[len];
            if (len >= 2)
            {
                len = len / 2;
            }
            byte[] bbt = new byte[len];
            abt = System.Text.Encoding.ASCII.GetBytes(asc);
            int j, k;
            for (int p = 0; p < asc.Length / 2; p++)
            {
                if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9'))
                {
                    j = abt[2 * p] - '0';
                }
                else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z'))
                {
                    j = abt[2 * p] - 'a' + 0x0a;
                }
                else
                {
                    j = abt[2 * p] - 'A' + 0x0a;
                }
                if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9'))
                {
                    k = abt[2 * p + 1] - '0';
                }
                else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z'))
                {
                    k = abt[2 * p + 1] - 'a' + 0x0a;
                }
                else
                {
                    k = abt[2 * p + 1] - 'A' + 0x0a;
                }
                int a = (j << 4) + k;
                byte b = (byte)a;
                bbt[p] = b;
            }
            return bbt;

        }

        public string bcd2Str(byte[] bytes)
        {
            StringBuilder temp = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                temp.Append((byte)((bytes[i] & 0xf0) >> 4));
                temp.Append((byte)(bytes[i] & 0x0f));
            }
            return temp.ToString().Substring(0, 1).Equals("0") ? temp.ToString().Substring(1) : temp.ToString();

        }

        public void WriteASCII(string value, int length)
        {
            if (value.Length > length)
            {
                value = value.Substring(0, length);
            }
            else
            {
                for (int i = value.Length; i < length; i++)
                {
                    value = " " + value;
                }
            }
            byte[] data = Encoding.ASCII.GetBytes(value);
            Write(data);
        }

        public string ReadASCII(int length)
        {
            byte[] data = Read(length);
            return Encoding.ASCII.GetString(data);
        }

        public int WriteGBK(string value)
        {
            int postion = mPostion;
            byte[] data = Encoding.GetEncoding("GBK").GetBytes(value);
            Write(data);
            return mPostion - postion;
        }

        public string ReadGBK(int length = -1)
        {
            if (length == -1)
                return Encoding.GetEncoding("GBK").GetString(Array, mPostion, mLength - mPostion);
            byte[] data = Read(length);
            return Encoding.GetEncoding("GBK").GetString(data);
        }

        public override string ToString()
        {
            string hex = BitConverter.ToString(Array, 0, Length).Replace("-", string.Empty);
            return hex;
        }
    }


    public class ProtocolBufferPool
    {
        private System.Collections.Concurrent.ConcurrentStack<IProtocolBuffer>
             mPool = new System.Collections.Concurrent.ConcurrentStack<IProtocolBuffer>();

        public ProtocolBufferPool()
        {
            for (int i = 0; i < 1000; i++)
            {
                mPool.Push(CreateBuffer());
            }
        }

        public IProtocolBuffer Pop()
        {
            IProtocolBuffer result = null;
            if (!mPool.TryPop(out result))
            {
                result = CreateBuffer();
            }

            result.Reset();
            return result;
        }

        private IProtocolBuffer CreateBuffer()
        {
            ProtocolBuffer buffer = new ProtocolBuffer();
            return buffer;
        }

        public void Push(IProtocolBuffer buffer)
        {
            mPool.Push(buffer);
        }

        private static ProtocolBufferPool mDefault;

        public static ProtocolBufferPool Default
        {
            get
            {
                if (mDefault == null)
                    mDefault = new ProtocolBufferPool();
                return mDefault;
            }
        }
    }
}
