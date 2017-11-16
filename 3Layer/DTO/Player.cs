using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Layer.DTO
{
    public class Player
    {
        public String Name { get; set; }
        public int Score { get; set; }

        public Player(String name) {
            this.Name = name;
            this.Score = 0;
        }
    }
}
