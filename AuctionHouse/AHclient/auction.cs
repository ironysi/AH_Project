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
        public double Price { get; set; }
        public int TimeLeft { get; set; }
        public string Description { get; set; }
        public int ID { get; private set; }
        private bool active;
        
        public Auction(string name, double currentPrice, int timeleft, string description, bool isActive)
        {
            Name = name;
            Price = currentPrice;
            TimeLeft = timeleft;
            Description = description;
            active = isActive;
        }

        public Auction() { }

        public void UpdateTimeLeft(int timeleft)
        {
            this.TimeLeft = timeleft;
        }
    }
}
