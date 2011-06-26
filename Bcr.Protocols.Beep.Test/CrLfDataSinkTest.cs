using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bcr.Protocols.Beep.Test
{
    [TestClass]
    public class CrLfDataSinkTest
    {
        [TestMethod]
        public void TestWorking()
        {
            var fixture = new CrLfDataSink();
            const string testString = "hi\r\n";
            var testStringBytes = Encoding.ASCII.GetBytes(testString);

            Assert.IsFalse(fixture.IsDataCompleted);
            Assert.AreEqual(testStringBytes.Length, fixture.AddBytes(testStringBytes, 0, testStringBytes.Length));
            Assert.IsTrue(fixture.IsDataCompleted);
            Assert.AreEqual(testString, fixture.FinalString);
        }

        [TestMethod]
        public void TestNoDataOverread()
        {
            var fixture = new CrLfDataSink();
            const string testString = "hi\r\n ";
            var testStringBytes = Encoding.ASCII.GetBytes(testString);

            Assert.IsFalse(fixture.IsDataCompleted);
            Assert.AreEqual(testStringBytes.Length - 1, fixture.AddBytes(testStringBytes, 0, testStringBytes.Length));
            Assert.IsTrue(fixture.IsDataCompleted);
            Assert.AreEqual("hi\r\n", fixture.FinalString);
        }

        [TestMethod]
        public void TestNoDataOverreadSecondBlock()
        {
            var fixture = new CrLfDataSink();
            const string testString = "hi\r\n";
            var testStringBytes = Encoding.ASCII.GetBytes(testString);

            Assert.IsFalse(fixture.IsDataCompleted);
            Assert.AreEqual(testStringBytes.Length, fixture.AddBytes(testStringBytes, 0, testStringBytes.Length));
            Assert.IsTrue(fixture.IsDataCompleted);
            Assert.AreEqual(0, fixture.AddBytes(testStringBytes, 0, testStringBytes.Length));
            Assert.AreEqual(testString, fixture.FinalString);
        }

        [TestMethod]
        public void TestPedanticData()
        {
            var fixture = new CrLfDataSink();
            const string testString = "hi\r\n";
            var testStringBytes = Encoding.ASCII.GetBytes(testString);

            WritePedanticData(fixture, testStringBytes);
            Assert.AreEqual("hi\r\n", fixture.FinalString);
        }

        [TestMethod]
        public void TestReset()
        {
            var fixture = new CrLfDataSink();
            const string testString = "hi\r\n";
            var testStringBytes = Encoding.ASCII.GetBytes(testString);

            Assert.IsFalse(fixture.IsDataCompleted);
            Assert.AreEqual(testStringBytes.Length, fixture.AddBytes(testStringBytes, 0, testStringBytes.Length));
            Assert.IsTrue(fixture.IsDataCompleted);
            Assert.AreEqual(testString, fixture.FinalString);

            fixture.Reset();

            const string testString2 = "there\r\n";
            var testStringBytes2 = Encoding.ASCII.GetBytes(testString2);

            Assert.IsFalse(fixture.IsDataCompleted);
            Assert.AreEqual(testStringBytes2.Length, fixture.AddBytes(testStringBytes2, 0, testStringBytes2.Length));
            Assert.IsTrue(fixture.IsDataCompleted);
            Assert.AreEqual(testString2, fixture.FinalString);
        }

        public static void WritePedanticData(IDataSink fixture, byte[] testStringBytes)
        {
            Assert.IsFalse(fixture.IsDataCompleted);
            for (int counter = 0;counter < testStringBytes.Length;++counter)
            {
                Assert.AreEqual(1, fixture.AddBytes(testStringBytes, counter, 1));
                if (counter == (testStringBytes.Length - 1))
                {
                    Assert.IsTrue(fixture.IsDataCompleted);
                }
                else
                {
                    Assert.IsFalse(fixture.IsDataCompleted);
                }
            }
        }
    }
}
