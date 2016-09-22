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
    public class ServerAuction
    {
        private int startTime;
        private int endTime;
        private bool Active;
        private int auctionTime;
        public int ID { get; private set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int TimeLeft { get; set; }
        public string HighestBidder { get; set; }
        private List<ClientHandler> Subscribers;

        public ServerAuction(string name, double price, int ahTime, string description)
        {
            Subscribers = ServerUtilities.ClientList;
            Name = name;
            Price = price;
            auctionTime = ahTime;
            Description = description;
            TimeLeft = auctionTime;
            Active = true;
            UpdateTime();
            //ServerUtilities.AuctionList.Add(new ServerAuction(name, price, ahTime, description));
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
                if(TimeLeft <= 3)
                {
                    for (int i = 0; i < Subscribers.Count; i++)
                    {
                        Subscribers[i].Gavel("Time left: " + TimeLeft.ToString());
                    }
                }
            }

        }

        public void NewBidAccepted(double newPrice, string highestBidder)
        {
            if (Active == true)
            {
                if (TimeLeft <= 3)
                {
                    UpdateTime();
                }
                TimeLeft = auctionTime;
                Price = newPrice;
                HighestBidder = highestBidder;

                for (int i = 0; i < Subscribers.Count; i++)
                {
                    Subscribers[i].BidNotification(Price, HighestBidder);
                }

            }
            else
            {
                // Return that the auction is already closed
            }

        }

        public void CheckNewBid(double newPrice, string highestBidder)
        {
            if (newPrice > Price)
            {
                NewBidAccepted(newPrice, highestBidder);
                UpdateTime();
            }
            else
            {
                // return that the bid was not accepted
            }
        }

        public void Notify()
        {
            for (int i = 0; i < Subscribers.Count; i++)
            {
                Subscribers[i].Update();
            }
        }

        public void RunActiveAuction()
        {
            int currentTime = ServerUtilities.Time;
            startTime = ServerUtilities.Time;

            while (true)
            {
                if (ServerUtilities.Time != startTime + auctionTime)
                {
                    if (currentTime != ServerUtilities.Time)
                    {
                        currentTime++;
                        SetTimeLeft();
                    }
                }
                else
                {
                    Console.WriteLine("Bid over");

                    for (int i = 0; i < Subscribers.Count; i++)
                    {
                        Subscribers[i].Gavel("Bid over, sold to " + HighestBidder);
                    }

                    break;
                }
            }
        }
    }
}
