using System.Collections.Generic;
using escape_corona.Interfaces;

namespace escape_corona.Models
{
    class Player : IPlayer
    {
        private string playerName;

        public Player(string playerName)
        {
            Name = playerName;
            Inventory = new List<IItem>();
        }

        public string Name { get; set; }
        public List<IItem> Inventory { get; set; }
    }
}