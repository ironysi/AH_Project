using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Client
    {
        private int Id;
        private string Name;
        private string PasswordHash;

        public Client(string name, string passwordHash) : this (0, name, passwordHash)
        {
            
        }

        public Client(int id, string name, string passwordHash)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
        }


    }
}
