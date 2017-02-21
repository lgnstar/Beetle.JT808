using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beetle.JT808.Serializes;
namespace Beetle.JT808
{
    public class Message : IMessage
    {
        public Message()
        {
            Property = new JT808.MessageBodyAttributes();
        }

        public object Body
        {
            get;
            set;
        }

        public ushort ID
        {
            get;
            set;
        }

        public MessageBodyAttributes Property
        {
            get;
            set;
        }

        public string SIM
        {
            get;
            set;
        }

        public ushort BussinessNO
        {
            get;
            set;
        }

        public PacketInfo Packet
        {
            get;
            set;
        }

        public byte CRC
        {
            get;
            set;
        }

        public void Load(IProtocolBuffer buffer)
        {
            byte crc = Core.GetCRC(buffer.Array, 1, buffer.Length - 3);
            this.CRC = buffer.Array[buffer.Length - 2];
            if (this.CRC != crc)
                throw new ProtocolProcessError("message check CRC error!");
            buffer.Read();              //read start
            ID = buffer.ReadUInt16();   //read id
            Property.Load(buffer);      //read property
            SIM = buffer.ReadBCD(6);    //read sim
            BussinessNO = buffer.ReadUInt16();  //read no
            if (Property.IsPacket)      //read packet
            {
                Packet = new PacketInfo();
                Packet.Load(buffer);
            }
            if (Property.BodyLength > 0) //read body
            {
                IProtocolBuffer bodybuffer = ProtocolBufferPool.Default.Pop();
                try
                {

                    buffer.ReadSubBuffer(bodybuffer, Property.BodyLength);
                    Serializer serializer = SerializerFactory.Defalut.Get(ID);
                    Body = serializer.CreateObject();
                    serializer.Deserialize(Body, bodybuffer);
                }
                finally
                {
                    ProtocolBufferPool.Default.Push(bodybuffer);
                }
            }
            this.CRC = buffer.Read(); //read crc
            buffer.Read();            //read end
        }

        public void Save(IProtocolBuffer buffer)
        {
            IProtocolBuffer bodybuffer = null;
            try
            {
                if (Packet != null)
                    Property.IsPacket = true;
                if (Body != null)
                {
                    Serializer serializer = SerializerFactory.Defalut.Get(Body.GetType());
                    if (serializer == null)
                        throw new ProtocolProcessError(string.Format("{0} serializer not found!", Body));
                    ID = serializer.MessageType.ID;
                    if (!serializer.MessageType.NoBody)
                    {

                        bodybuffer = ProtocolBufferPool.Default.Pop();
                        serializer.Serialize(Body, bodybuffer);
                        if (bodybuffer.Length > MessageBodyAttributes.BODY_LENGTH)
                            throw new ProtocolProcessError("message body to long!");
                        Property.BodyLength = (ushort)bodybuffer.Length;

                    }
                }
                buffer.WriteTag();           //write start
                buffer.Write(ID);            //write id
                Property.Save(buffer);       //write body property
                buffer.WriteBCD(SIM);        //write sim
                buffer.Write(BussinessNO);   //write no
                if (Packet != null)          //write packet
                    Packet.Save(buffer);
                if (bodybuffer != null)      //write body
                    buffer.WriteSubBuffer(bodybuffer);
                byte crc = Core.GetCRC(buffer.Array, 1, buffer.Length - 1);
                buffer.Write(crc);          //write crc         
                buffer.WriteTag();          //write end
            }
            finally
            {
                if (bodybuffer != null)
                    ProtocolBufferPool.Default.Push(bodybuffer);
            }
        }

        public T GetBody<T>()
        {
            return (T)Body;
        }
    }
}
