using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuctionHouse;

namespace UnitTestServer
{
    [TestClass]
    public class UnitTest1
    {
        // Unit Test checking if it works
        [TestMethod]
        public void CreateEditClient()
        {
            Client testClient = new Client("name");

            testClient.ChangeInformation("newName");
        }
    }
}
