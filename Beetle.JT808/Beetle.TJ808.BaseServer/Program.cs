using Beetle.JT808;
using Beetle.JT808.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.TJ808.BaseServer
{
    class Program : Beetle.Express.IServerHandler
    {
        private static Beetle.Express.IServer mServer;

        static void Main(string[] args)
        {
            Beetle.JT808.Serializes.SerializerFactory.Init();
            mServer = Beetle.Express.ServerFactory.CreateTCP();
            mServer.Port = 9091;
            mServer.Handler = new Program();
            mServer.Open(false, false, false, 1, 1, "JT808");
            Console.WriteLine("server start!");
            Console.Read();
        }

        public void Connect(Express.IServer server, Express.ChannelConnectEventArgs e)
        {

        }

        public void Disposed(Express.IServer server, Express.ChannelEventArgs e)
        {

        }

        public void Error(Express.IServer server, Express.ErrorEventArgs e)
        {
            Console.WriteLine(e.Error.Message);
        }

        public void Opened(Express.IServer server)
        {

        }

        public void Receive(Express.IServer server, Express.ChannelReceiveEventArgs e)
        {
            IProtocolBuffer protocolbuffer;
            if (e.Channel.Tag == null)
            {
                protocolbuffer = ProtocolBufferPool.Default.Pop();
                e.Channel.Tag = protocolbuffer;
            }
            else
            {
                protocolbuffer = (IProtocolBuffer)e.Channel.Tag;

            }
            int offset = 0; int count = e.Data.Count;
            while (count > 0)
            {
                int readcout = protocolbuffer.Import(e.Data.Array, 0, count);
                if (readcout > 0)
                {
                    Message message = MessageFactory.MessageFromBuffer(protocolbuffer);
                    ProtocolBufferPool.Default.Push(protocolbuffer);
                    protocolbuffer = ProtocolBufferPool.Default.Pop();
                    e.Channel.Tag = protocolbuffer;
                    IProtocolBuffer result =  MessageFactory.MessateToBuffer<CenterResponse>(
                        message.BussinessNO, message.SIM, (m, b) =>
                        {
                            b.BussinessNO = message.BussinessNO;
                            b.ResultID = message.ID;
                            b.Result = ResultType.Success;
                        });
                    Beetle.Express.Data data = new Express.Data(result.Array, result.Length);
                    data.Tag = result;
                    server.Send(data, e.Channel);
                    offset += readcout;
                    count -= readcout;
                }
            }
        }

        public void SendCompleted(Express.IServer server, Express.ChannelSendEventArgs e)
        {
            IProtocolBuffer buffer = (IProtocolBuffer)e.Data.Tag;
            ProtocolBufferPool.Default.Push(buffer);
        }
    }
}
