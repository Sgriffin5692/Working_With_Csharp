using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCsharp_FinalProject
{
    interface IDice
    {
        int Sides
        {
            get;
            set;
        }

        int Roll();
    }
}
