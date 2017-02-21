using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public class MessageBodyAttributes
    {

        #region consts
        public const ushort CUSTOM_HEIGHT = 0x8000;

        public const ushort CUSTOM_LOW = 0x4000;

        public const ushort IS_PACKET = 0x2000;

        public const ushort ENCRYPT_HEIGHT = 0x1000;

        public const ushort ENCRYPT_MIDDLE = 0x400;

        public const ushort ENCRYPT_LOW = 0x200;

        public const ushort BODY_LENGTH = 0x1FF;
        #endregion

        //保留位15
        public bool CustomHigh { get; set; }

        //保留位14
        public bool CustomLow { get; set; }

        //分包位13
        public bool IsPacket { get; set; }

        //加密位12
        public bool EncryptHigh { get; set; }

        //加密位11
        public bool EncryptMiddle { get; set; }

        //加密位10
        public bool EncryptLow { get; set; }

        //消息长度9-0
        public ushort BodyLength { get; set; }

        public void Save(IProtocolBuffer buffer)
        {
            ushort value = (ushort)(BodyLength & BODY_LENGTH);
            if (CustomHigh)
                value |= CUSTOM_HEIGHT;
            if (CustomLow)
                value |= CUSTOM_LOW;
            if (IsPacket)
                value |= IS_PACKET;
            if (EncryptHigh)
                value |= ENCRYPT_HEIGHT;
            if (EncryptMiddle)
                value |= ENCRYPT_MIDDLE;
            if (EncryptLow)
                value |= ENCRYPT_LOW;
            buffer.Write(value);
        }

        public void Load(IProtocolBuffer buffer)
        {

            ushort value = buffer.ReadUInt16();
            CustomHigh = (CUSTOM_HEIGHT & value) > 0;
            CustomLow = (CUSTOM_LOW & value) > 0;
            IsPacket = (IS_PACKET & value) > 0;
            EncryptHigh = (ENCRYPT_HEIGHT & value) > 0;
            EncryptMiddle = (ENCRYPT_MIDDLE & value) > 0;
            EncryptLow = (ENCRYPT_LOW & value) > 0;
            BodyLength = (ushort)(BODY_LENGTH & value);
        }
    }
}
