using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static bool Validator(string userInput)
        {
            int intInput = 0;
            if (int.TryParse(userInput, out intInput))
            {

            }
            return true;
        }
    }

    class HeMan
    {
        // public variables
        public int health = 100;

        // private variables
        private string[] swordAttacks = { "Sword slash", "Uppercut sword slash", "Weak slash" };
        private string insultAttack = "You raise your sword and yell out: I HAVE THE POWER!!!!";
        private string healingPower = "You reach for a bag of chips and eat them.";
        private Random attackGenerator = new Random();
        public HeMan()
        {

        }

        public bool isDead()
        {
            if (health == 0)
            {
                return true;
            }
            return false;
        }

        public int Attack(int userInput)
        {
            int attackPower = 0;
            int chances = 0;

            if (userInput == 1)
            {
                attackPower = attackGenerator.Next(25, 45);
                chances = attackGenerator.Next(1, 100);
                if (chances < attackPower)
                {
                    if (attackPower >= 35)
                    {
                        Console.WriteLine("You deal {0} damage a great feat with your {1}. Way to go, He-man...", attackPower, swordAttacks[1]);
                    }
                    else
                    {
                        // you miss and skeletor insults you
                        Console.WriteLine("<Skeletor>You weakling. Go back to your football.\nYou miss with your {0}.", swordAttacks[2]);
                    }
                }
            }

            if (userInput == 2)
            {
                attackPower = attackGenerator.Next(1, 15);
                Console.WriteLine(insultAttack);
            }

            if (userInput == 3)
            {
                Console.WriteLine("{0} They restore your health {1} points.", healingPower,Healing(attackGenerator.Next(10, 20)));
            }

            return attackPower;
        }

        /// <summary>
        /// Heals He-man
        /// </summary>
        /// <param name="addHealth"></param>
        /// <returns></returns>
        public int Healing(int addHealth)
        {
            health += addHealth;
            return addHealth;
        }
    }

    class Skeletor
    {
        public int health = 200;

        public Skeletor()
        {

        }
    }
}
