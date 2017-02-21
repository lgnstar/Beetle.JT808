
using Beetle.JT808.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    public class MessageFactory
    {

        public static IProtocolBuffer MessateToBuffer<T>(ushort businessNO, string sim, Action<IMessage, T> handler) where T : new()
        {
            IProtocolBuffer buffer = ProtocolBufferPool.Default.Pop();
            Message msg = new Message();
            msg.BussinessNO = businessNO;
            msg.SIM = sim;
            T body = new T();
            msg.Body = body;
            if (handler != null)
                handler(msg, body);
            msg.Save(buffer);
            return buffer;
        }

        public static ushort GetMessageID<T>()
        {
            Type type = typeof(T);
            Serializes.Serializer serializer = Serializes.SerializerFactory.Defalut.Get(type);
            if (serializer == null)
                throw new ProtocolProcessError(string.Format("{0} serializer not found!", type));
            return serializer.MessageType.ID;
        }

        public static Message MessageFromBuffer(IProtocolBuffer buffer)
        {
            Message msg = new Message();
            msg.Load(buffer);
            return msg;
        }
    }
}
