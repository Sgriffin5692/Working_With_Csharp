using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCsharp_FinalProject
{
    interface ICharacter
    {
        string Name
        {
            get;
            set;
        }

        int Health
        {
            get;
            set;
        }

        int MaximumDamage
        {
            get;
            set;
        }

        bool AttackBonus
        {
            get;
            set;
        }

        int Attack(IDice dice);

        void Defend(int _damage);
    }
}
