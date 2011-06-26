using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bcr.Protocols.Beep.Test
{
    [TestClass]
    public class FrameTest
    {
        [TestMethod]
        public void TestItAll()
        {
            const string frameString = "RPY 0 0 . 0 52\r\n" +
                                       "Content-Type: application/beep+xml\r\n" +
                                       "\r\n" +
                                       "<greeting />\r\n" +
                                       "END\r\n";
            var frameStringBytes = Encoding.ASCII.GetBytes(frameString);
            var fixture = new Frame();

            CrLfDataSinkTest.WritePedanticData(fixture, frameStringBytes);
        }
    }
}
