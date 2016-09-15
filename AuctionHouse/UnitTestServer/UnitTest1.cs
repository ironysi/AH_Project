using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuctionHouse;
using System.Threading;

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

        [TestMethod]
        public void CreateAuctionCountdownTest()
        {
            ServerAuction bid = new ServerAuction("Teddy", 10.0, 18, "Fluffy");

            Thread auctionThread = new Thread(bid.RunActiveAuction);
            auctionThread.Start();

        }
    }
}
