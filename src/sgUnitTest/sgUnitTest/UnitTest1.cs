using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sgUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /* 
         * IPv4 を渡されたときに、分解するして、以下のテストを通るように、
         * CheckIP#Check のメソッドの中身を書き替えよ
         * 
         * - 文字列が正しくないときは、例外ではなくて false を返すこと
         */

        [TestMethod]
        public void TestMethod1()
        {
            string ipaddr = "127.0.0.1";

            var ip = new CheckIP();
            Assert.AreEqual(true, ip.Check(ipaddr));
            Assert.AreEqual(127, ip.IP1);
            Assert.AreEqual(0, ip.IP2);
            Assert.AreEqual(0, ip.IP3);
            Assert.AreEqual(1, ip.IP4);
            Assert.AreEqual("127.0.0.1", ip.ToString());
        }


        [TestMethod]
        public void 空欄の場合()
        {
            string ipaddr = "";
            var ip = new CheckIP();
            Assert.AreEqual(false, ip.Check(ipaddr));
        }

        [TestMethod]
        public void ピリオドが足りない場合()
        {
            string ipaddr = "255.255.255";
            var ip = new CheckIP();
            Assert.AreEqual(false, ip.Check(ipaddr));
        }

        [TestMethod]
        public void ピリオドが多すぎる場合()
        {
            string ipaddr = "255.255.255.255.255.255";
            var ip = new CheckIP();
            Assert.AreEqual(false, ip.Check(ipaddr));
        }

        [TestMethod]
        public void 数値が255を超える場合()
        {
            string ipaddr = "255.255.256.255";
            var ip = new CheckIP();
            Assert.AreEqual(false, ip.Check(ipaddr));
        }

        [TestMethod]
        public void 文字列を含む場合()
        {
            string ipaddr = "255.255.xxx.255";
            var ip = new CheckIP();
            Assert.AreEqual(false, ip.Check(ipaddr));
        }

        [TestMethod]
        public void 途中で空欄があっても大丈夫()
        {
            string ipaddr = "127. 0     .0.1   ";

            var ip = new CheckIP();
            Assert.AreEqual(true, ip.Check(ipaddr));
            Assert.AreEqual(127, ip.IP1);
            Assert.AreEqual(0, ip.IP2);
            Assert.AreEqual(0, ip.IP3);
            Assert.AreEqual(1, ip.IP4);
            Assert.AreEqual("127.0.0.1", ip.ToString());
        }

        [TestMethod]
        public void localhostを有効にする()
        {
            string ipaddr = "localhost";

            var ip = new CheckIP();
            Assert.AreEqual(true, ip.Check(ipaddr));
            Assert.AreEqual(127, ip.IP1);
            Assert.AreEqual(0, ip.IP2);
            Assert.AreEqual(0, ip.IP3);
            Assert.AreEqual(1, ip.IP4);
            Assert.AreEqual("127.0.0.1", ip.ToString());
        }
    }
}
