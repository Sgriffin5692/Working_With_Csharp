using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCsharp_FinalProject
{
    class Character : ICharacter
    {
        private string name;
        private int health;
        private int maximumDamage;
        private bool attackBonus;

        public Character(string _name, int _health, int _maximumDamage)
        {
            name = _name;
            health = _health;
            maximumDamage = _maximumDamage;
            attackBonus = false;
        }

        public Character() { }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int MaximumDamage
        {
            get
            {
                return maximumDamage;
            }
            set
            {
                maximumDamage = value;
            }
        }

        public bool AttackBonus
        {
            get
            {
                return attackBonus;
            }
            set
            {
                attackBonus = value;
            }
        }

        public int Attack(IDice _dice)
        {
            _dice.Sides = maximumDamage;
            return _dice.Roll();
        }

        public void Defend(int _damage)
        {
            health -= _damage;
        }
    }
}
