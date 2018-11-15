using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCsharp_FinalProject
{
    class Program
    {
        // static field to keep track of battle number
        static int battleNum = 0;

        static void Main(string[] args)
        {
            Character hero = new Character("Hero", 40, 20);
            Character monster = new Character("Monster", 40, 20);
            Dice theDice = new Dice();

            DoBattle(hero, monster, theDice);
            Console.ReadLine();
        }

        private static void DetermineBonus(ICharacter char1, ICharacter char2)
        {
            Random rand = new Random();
            int bonusValue = rand.Next(2);
            if (bonusValue == 0)
                char1.AttackBonus = true;
            else
                char2.AttackBonus = true;
        }

        private static void AdjustStats(string _roll, int _damage, ICharacter opponentChar)
        {
            Console.WriteLine($"{_roll}\nAttack = {_damage}\n");
            opponentChar.Defend(_damage);
        }

        public static void BonusRoll(ICharacter char1, ICharacter char2, IDice _dice)
        {
            DetermineBonus(char1, char2);
            Console.WriteLine("Bonus Roll:");

            if (char1.AttackBonus == true)
                AdjustStats(char1.Name + " Bonus Roll", char1.Attack(_dice), char2);
            else
                AdjustStats(char2.Name + " Bonus Roll", char2.Attack(_dice), char1);
        }

        public static void DoBattle(ICharacter char1, ICharacter char2, IDice _dice)
        {
            BonusRoll(char1, char2, _dice);
            Console.WriteLine("Battle Roll:");
            while (char1.Health > 0 && char2.Health > 0)
            {
                AdjustStats(char1.Name + " Battle Roll", char1.Attack(_dice), char2);
                AdjustStats(char2.Name + " Battle Roll", char2.Attack(_dice), char1);
            }
            DisplayResults(char1, char2);
        }

        private static void DisplayEndStats(ICharacter opponent1, ICharacter opponent2)
        {
            // Pre-increments battleNum to display new battle number
            Console.WriteLine($"Battle {++battleNum}:");
            Console.WriteLine($"Name: {opponent1.Name} - Health: {opponent1.Health} - Damage Maximum: {opponent1.MaximumDamage} - Attack Bonus: {opponent1.AttackBonus}");
            Console.WriteLine($"Name: {opponent2.Name} - Health: {opponent2.Health} - Damage Maximum: {opponent2.MaximumDamage} - Attack Bonus: {opponent2.AttackBonus}");
        }

        private static string DetermineWinner(ICharacter opponent1, ICharacter opponent2)
        {
            if (opponent1.Health < 0 && opponent2.Health < 0)
                return $"Both {opponent1.Name} and {opponent2.Name} died.";
            else if (opponent1.Health > 0)
                return $"{opponent1.Name} won the battle, {opponent2.Name} died.";
            else
                return $"{opponent2.Name} won the battle, {opponent1.Name} died.";
        }

        private static void DisplayResults(ICharacter opponent1, ICharacter opponent2)
        {
            DisplayEndStats(opponent1, opponent2);
            Console.WriteLine("\n***********************************");
            Console.WriteLine(DetermineWinner(opponent1, opponent2));
            Console.WriteLine("***********************************");
        }
    }
}
