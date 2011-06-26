using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bcr.Protocols.Beep.Test
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void TestIsMoreIndicator()
        {
            Assert.IsFalse(Util.IsMoreIndicator("."));
            Assert.IsTrue(Util.IsMoreIndicator("*"));
        }

        [TestMethod]
        public void TestFrameTypeFromString()
        {
            Assert.AreEqual(FrameType.Message, Util.FrameTypeFromString("MSG"));
            Assert.AreEqual(FrameType.Reply, Util.FrameTypeFromString("RPY"));
            Assert.AreEqual(FrameType.Answer, Util.FrameTypeFromString("ANS"));
            Assert.AreEqual(FrameType.Error, Util.FrameTypeFromString("ERR"));
            Assert.AreEqual(FrameType.Null, Util.FrameTypeFromString("NUL"));
        }
    }
}
