using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Player
    {
        public string Name { get; protected set; }
        public List<Critter> collection = new List<Critter>();
        public List<Critter> battleCritter;

        public Player(List<Critter> critters, string _name)
        {
            Name = _name;
            battleCritter = critters;
            collection = critters;
        }
    }
}
