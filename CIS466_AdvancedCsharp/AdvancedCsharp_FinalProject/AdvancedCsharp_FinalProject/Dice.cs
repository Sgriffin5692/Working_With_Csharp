using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCsharp_FinalProject
{
    class Dice : IDice
    {
        private Random rand = new Random();
        private int sides;

        public int Sides
        {
            get
            {
                return sides;
            }
            set
            {
                sides = value;
            }
        }

        public int Roll()
        {
            // sides + 1 allows the maximum value to be used
            return rand.Next(sides + 1);
        }
    }
}
