using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHclient
{
    class Auction
    {
        public string Name { get; set; }
        public double CurrentPrice { get; set; }
        public int TimeLeftInSeconds { get; set; }
        public string Description { get; set; }
        public int AuctionState { get; set; }

        public Auction(string name, double currentPrice, int timeleftInSeconds)
        {
            this.Name = name;
            this.CurrentPrice = currentPrice;
            this.TimeLeftInSeconds = timeleftInSeconds;
        }

        public Auction(string name, double currentPrice, int timeleftInSeconds, string description, int auctionstate)
        {
            this.Name = name;
            this.CurrentPrice = currentPrice;
            this.TimeLeftInSeconds = timeleftInSeconds;
            this.Description = description;
            this.AuctionState = auctionstate;
        }

        public void UpdateTimeLeft(int timeleftInSeconds)
        {
            this.TimeLeftInSeconds = timeleftInSeconds;
        }

        public void SetAuctionState(int auctionstate)
        {
            this.AuctionState = auctionstate;
        }

        public void ChangePrice(double currentPrice)
        {
            this.CurrentPrice = currentPrice;
        }
    }
}
