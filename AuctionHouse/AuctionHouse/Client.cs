using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    public class Client
    {
        private int Id;
        private string Name;

        public Client(string name) : this (0, name)
        {
            
        }

        public Client(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void ChangeInformation(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
