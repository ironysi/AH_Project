using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class ServerAuction
    {
        public string Name { get; set; }
        public double CurrentPrice { get; set; }
        public int TimeLeftInSeconds { get; set; }
        public string Description { get; set; }
        public int AuctionState { get; set; }
        private bool CountdownCompleted { get; set; }

        public ServerAuction(string name, double currentPrice, int timeleftInSeconds)
        {
            this.Name = name;
            this.CurrentPrice = currentPrice;
            this.TimeLeftInSeconds = timeleftInSeconds;
        }

        public ServerAuction(string name, double currentPrice, int timeleftInSeconds, string description, int auctionstate)
        {
            this.Name = name;
            this.CurrentPrice = currentPrice;
            this.TimeLeftInSeconds = timeleftInSeconds;
            this.Description = description;
            this.AuctionState = auctionstate;
        }

    }
}
