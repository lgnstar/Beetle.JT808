using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beetle.JT808.UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //Beetle.Express.Clients.SyncTcpClient client = new Express.Clients.SyncTcpClient("192.168.1.184", 5555);
            //DateTime dt = new DateTime(2016, 12, 23, 13, 50, 50);
            //IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientPostion>(BUSINESS_NO, SIM, (m, b) =>
            //{
            //    b.Direction = 4;
            //    b.Height = 5;
            //    b.Latitude = 56;
            //    b.Longitude = 100;
            //    b.Speed = 100;
            //    b.Status.ACC = true;
            //    b.Time = dt;
            //    b.WarningMark.DisplayTheFault = true;
            //});
            //client.Send(buffer.Array, 0, buffer.Length);
            Console.WriteLine("OK");
        }

        private const UInt16 BUSINESS_NO = 1;

        private const string SIM = "13660170908";

        [TestMethod]
        public void TestClientResponse()
        {
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientResponse>(BUSINESS_NO, SIM, (m, b) =>
            {
                b.BussinessNO = 12;
                b.ResultID = 5;
                b.Result = Messages.ResultType.Success;
            });
            buffer.Postion = 0;
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.ClientResponse response = msg.GetBody<Messages.ClientResponse>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.ClientResponse>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
            Assert.AreEqual(response.BussinessNO, 12);
            Assert.AreEqual(response.Result, Messages.ResultType.Success);
            Assert.AreEqual(response.ResultID, 5);
        }

        [TestMethod]
        public void TestClientPing()
        {
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientPing>(BUSINESS_NO, SIM, (m, b) =>
            {

            });
            buffer.Postion = 0;
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.ClientPing response = msg.GetBody<Messages.ClientPing>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.ClientPing>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
        }

        [TestMethod]
        public void TestCenterResponse()
        {
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.CenterResponse>(BUSINESS_NO, SIM, (m, b) =>
            {
                b.BussinessNO = 12;
                b.ResultID = 5;
                b.Result = Messages.ResultType.Success;
            });
            buffer.Postion = 0;
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.CenterResponse response = msg.GetBody<Messages.CenterResponse>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.CenterResponse>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
            Assert.AreEqual(response.BussinessNO, 12);
            Assert.AreEqual(response.Result, Messages.ResultType.Success);
            Assert.AreEqual(response.ResultID, 5);
        }

        [TestMethod]
        public void TestClientRegister()
        {
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientRegister>(BUSINESS_NO, SIM, (m, b) =>
            {
                b.City = 5;
                b.Color = 4;
                b.DeviceID = "abc";
                b.DeviceNumber = "1002";
                b.PlateNumber = "粤A4XB38";
                b.Provider = "gdgz";
                b.Province = 10;
            });
            buffer.Postion = 0;
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.ClientRegister register = msg.GetBody<Messages.ClientRegister>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.ClientRegister>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
            Assert.AreEqual(register.City, 5);
            Assert.AreEqual(register.Color, 4);
            Assert.AreEqual(register.DeviceID, "abc");
            Assert.AreEqual(register.DeviceNumber, "1002");
            Assert.AreEqual(register.PlateNumber, "粤A4XB38");
            Assert.AreEqual(register.Provider, "gdgz");
            Assert.AreEqual(register.Province, 10);
        }

        [TestMethod]
        public void TestClientRegisterResponse()
        {
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientRegisterResponse>(BUSINESS_NO, SIM, (m, b) =>
            {
                b.BusinessNO = 6;
                b.Result = 10;
                b.Signature = "bbqabc";
            });
            buffer.Postion = 0;
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.ClientRegisterResponse response = msg.GetBody<Messages.ClientRegisterResponse>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.ClientRegisterResponse>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
            Assert.AreEqual(response.BusinessNO, 6);
            Assert.AreEqual(response.Result, 10);
            Assert.AreEqual(response.Signature, "bbqabc");
        }

        [TestMethod]
        public void TestClientRegisterCancel()
        {
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientRegisterCancel>(BUSINESS_NO, SIM, (m, b) =>
            {

            });
            buffer.Postion = 0;
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.ClientRegisterCancel response = msg.GetBody<Messages.ClientRegisterCancel>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.ClientRegisterCancel>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
        }

        [TestMethod]
        public void TestClientSignature()
        {
        }

        [TestMethod]
        public void TestClientPostion()
        {

            DateTime dt = new DateTime(2016, 12, 23, 13, 50, 50);
            IProtocolBuffer buffer = MessageFactory.MessateToBuffer<Messages.ClientPostion>(BUSINESS_NO, SIM, (m, b) =>
            {
                b.Direction = 4;
                b.Height = 5;
                b.Latitude = 56;
                b.Longitude = 100;
                b.Speed = 100;
                b.Status.ACC = true;
                b.Time = dt;
                b.WarningMark.DisplayTheFault = true;
            });
            buffer.Postion = 0;
            Console.WriteLine(buffer.ToString());
            IMessage msg = MessageFactory.MessageFromBuffer(buffer);
            Messages.ClientPostion postion = msg.GetBody<Messages.ClientPostion>();
            Assert.AreEqual(msg.ID, MessageFactory.GetMessageID<Messages.ClientPostion>());
            Assert.AreEqual(msg.SIM, SIM);
            Assert.AreEqual(msg.BussinessNO, BUSINESS_NO);
            Assert.AreEqual(postion.Direction, 4);
            Assert.AreEqual(postion.Height, 5);
            Assert.AreEqual(postion.Latitude, (uint)56);
            Assert.AreEqual(postion.Longitude, (uint)100);
            Assert.AreEqual(postion.Speed, 100);
            Assert.AreEqual(postion.Status.ACC, true);
            Assert.AreEqual(postion.Time, dt);
            Assert.AreEqual(postion.WarningMark.DisplayTheFault, true);
        }
    }
}
