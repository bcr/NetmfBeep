using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bcr.Protocols.Beep.Test
{
    [TestClass]
    public class FrameHeaderTest
    {
        [TestMethod]
        public void TestMsgFrame()
        {
            var fixture = new FrameHeader();
            const string headerString = "MSG 0 1 . 52 120\r\n";
            var headerStringBytes = Encoding.ASCII.GetBytes(headerString);

            Assert.IsFalse(fixture.IsDataCompleted);
            fixture.AddBytes(headerStringBytes, 0, headerStringBytes.Length);
            Assert.IsTrue(fixture.IsDataCompleted);

            Assert.AreEqual(FrameType.Message, fixture.ThisFrameType);
            Assert.AreEqual(0, fixture.Channel);
            Assert.AreEqual(1, fixture.MessageNo);
            Assert.IsFalse(fixture.More);
            Assert.AreEqual((uint) 52, fixture.SequenceNo);
            Assert.AreEqual(120, fixture.Size);
        }

        [TestMethod]
        public void TestAnsFrame()
        {
            var fixture = new FrameHeader();
            const string headerString = "ANS 1 0 * 0 20 0\r\n";
            var headerStringBytes = Encoding.ASCII.GetBytes(headerString);

            Assert.IsFalse(fixture.IsDataCompleted);
            fixture.AddBytes(headerStringBytes, 0, headerStringBytes.Length);
            Assert.IsTrue(fixture.IsDataCompleted);

            Assert.AreEqual(FrameType.Answer, fixture.ThisFrameType);
            Assert.AreEqual(1, fixture.Channel);
            Assert.AreEqual(0, fixture.MessageNo);
            Assert.IsTrue(fixture.More);
            Assert.AreEqual((uint)0, fixture.SequenceNo);
            Assert.AreEqual(20, fixture.Size);
            Assert.AreEqual(0, fixture.AnswerNo);
        }
    }
}
