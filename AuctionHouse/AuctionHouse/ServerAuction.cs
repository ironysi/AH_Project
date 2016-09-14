using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouse
{
    /// <summary>
    /// BID
    /// </summary>
    class ServerAuction
    {
        private int startTime;
        private int endTime;
        private bool Active;
        private int auctionTime;
        public string Description { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int TimeLeft { get; set; }
        public string HighestBidder { get; set; }

        public ServerAuction(string name, double price, int timeleftInSeconds)
        {
            Name = name;
            Price = price;
            TimeLeft = timeleftInSeconds;
        }

        public ServerAuction(string name, double price, int ahTime, string description)
        {
            Name = name;
            Price = price;
            auctionTime = ahTime;
            Description = description;
            TimeLeft = auctionTime;
            UpdateTime();
        }

        public void UpdateTime()
        {
            startTime = ServerUtilities.Time;
            endTime = startTime + auctionTime;
        }

        public void SetTimeLeft()
        {
            if (TimeLeft == 0)
            {
                Active = false;
            }
            else
            {
                TimeLeft = TimeLeft - 1;
            }

        }

        public void NewBidAccepted(double newPrice, string highestBidder)
        {
            UpdateTime();
            TimeLeft = auctionTime;
            Price = newPrice;
            HighestBidder = highestBidder;
        }

        public void CheckNewBid(double newPrice, string highestBidder)
        {
            if (newPrice > Price)
            {
                NewBidAccepted(newPrice, highestBidder);
            }
            else
            {
                // return that the bid was not accepted
            }
        }

        public void RunActiveAuction()
        {
            int currentTime = ServerUtilities.Time;
            int startTime = ServerUtilities.Time;

            while (true)
            {
                if (ServerUtilities.Time != startTime + 18)
                {
                    if (currentTime != ServerUtilities.Time)
                    {
                        Console.WriteLine(TimeLeft);
                        Console.WriteLine("--------Server Time ------"+ServerUtilities.Time);
                        currentTime++;
                        SetTimeLeft();
                    }
                }
                else
                {
                    Console.WriteLine("Bid over");
                    break;

                }
            }
        }

    }
}
