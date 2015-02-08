using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
	class Program
	{
		// point start
		static void Main(string[] args)
		{
			Game RunGame = new Game ();
			RunGame.Run ();
		}
	}

	/// <summary>
	/// All the game elements
	/// </summary>
	class Game{
		// global variables
		static HeMan player;
		static Skeletor boss;
		static int gameState;

		// Creates the game
		public Game(){
			player = new HeMan ();
			boss = new Skeletor ();
			// set window size
			Console.SetWindowSize (800, 600);
		}

		// the game loop
		public void Run()
		{
			bool quit = true;

			PrintTitle ();
			while (quit) {
				quit = player.Quitter ();
			}
		}

		public void PrintTitle(){
			string title = "     _______ _______ _______ _______ _______  ______\n     |  |  | |     | |          |    |       |     |     \n     |  |  | |_____| |______    |    |______ |_____/\n     |  |  | |     |       |    |    |       |   \\\n _   | _|__| | _   | ______|    |    |______ | ___\\_ _ _______\n |     | |\\    | _____ _     of_ the    ______ |       |\n |     | | \\   |   |    \\    / _______ |     | |______ |______\n |     | |  \\  |   |     \\  /  |______ |_____/       | |\n |_____| |   \\_| __|__    \\/   |______ |    \\_ ______| |______\n";

			for(int i = 0; i < title.Length; i++){
				Console.Write(i);
			}
		}

		/// <summary>
		/// Validator the specified userInput.
		/// </summary>
		/// <param name="userInput">User input.</param>
		/// <returns>Invalid input</returns>
		static bool Validator(string userInput)
		{
			int intInput = 0;
			if (int.TryParse(userInput, out intInput))
			{
				Console.WriteLine ("<Skeletor>\nIs that the only thing you can do right?");
			}
			if (intInput < 3) {
				return true;
			}
			Console.WriteLine ("<Skeletor>\nYou, mencing child... he he");
			return false;
		}
	}

	/// <summary>
	/// He man is the player. He has a sword and a lot of bags of chips.
	/// </summary>
	class HeMan
	{
		// public variables
		public int health { get; set; }

		// private variables
		private string[] swordAttacks = { "Sword slash", "Uppercut sword slash", "Weak slash" };
		private string insultAttack = "You raise your sword and yell out: I HAVE THE POWER!!!!";
		private string healingPower = "You reach for a bag of chips and eat them.";
		private Random attackGenerator = new Random();

		// when created
		public HeMan()
		{
			Console.WriteLine ("<He-Man>\nI HAVE THE POWER!!!");
			health = 100;
		}

		// checks if He-Man is dead or not.
		public bool isDead()
		{
			if (health == 0)
			{
				return true;
			}
			return false;
		}

		public void PrintAttacks(){
			Console.WriteLine ("(1) Sword (2) Annoy Shout (3) Eat Food");
		}

		public int Attack(int userInput)
		{
			int attackPower = 0;
			int chances = 0;

			// sword hits
			if (userInput == 1)
			{
				attackPower = attackGenerator.Next(25, 45);
				chances = attackGenerator.Next(1, 100);
				// 70% chances to hit Skeletor
				if (chances < 70)
				{
					if (attackPower >= 35) {
						Console.WriteLine ("You deal {0} damage a great feat with your {1}. Way to go, He-man...", attackPower, swordAttacks [1]);
					} else {
						Console.WriteLine ("You use your {0} and hit Skeletor with {1} damage.", swordAttacks[0], attackPower);
					}

				}else
				{
					// you miss and skeletor insults you
					Console.WriteLine("<Skeletor>\nYou weakling. Go back to your football.\nYou miss with your {0}.", swordAttacks[2]);
				}
			}

			// always hits
			if (userInput == 2)
			{
				attackPower = attackGenerator.Next(1, 15);
				Console.WriteLine(insultAttack);
			}

			// healing food
			if (userInput == 3)
			{
				Console.WriteLine("{0} They restore your health {1} points.", healingPower,Healing(attackGenerator.Next(10, 20)));
				attackPower = 0;  // can't attack Skeletor
			}

			return attackPower;
		}

		/// <summary>
		/// Heals He-man
		/// </summary>
		/// <param name="addHealth"></param>
		/// <returns>the Healing Points</returns>
		public int Healing(int addHealth)
		{
			health += addHealth;
			return addHealth;
		}

		// quit? prompt
		public bool Quitter(){
			Console.WriteLine ("He-Man, are you quiting now? (yes or no)");
			string yesOrNot = Console.ReadLine ();

			if (yesOrNot.ToLower () == "yes") {
				Console.WriteLine ("<Skeletor>\nThis planet, these people, they are nothing to me.");
				return false;
			}
			Console.WriteLine ("I HAVE THE POWER!!!");
			return true;
		}
	}

	/// <summary>
	/// Skeletor is a bad-ass villian. He uses his Havoc Staff, insults, and heals himself.
	/// </summary>
	class Skeletor
	{
		// public variables
		public int health { get; set; }

		// private variables
		private string[] StaffAttacks = { "Bolts", "Thief of Dreams", "Staff missed fires" };
		private string insultAttack = "Your furry, flea bitten fool I'll cover my throne with your hide.";
		private string healingPower = "Skeletor raises his staff over his head, yells out \"Comin' you Royal boob\"";
		private Random attackGenerator = new Random();

		// Creates Skeletor with health of 200. He doesn't say anything because he is a bad-ass
		public Skeletor()
		{
			Console.WriteLine("<Skeletor>\n...");
			health = 200;
		}

		// checks if Skeletor has fallen or not.
		public bool isDead()
		{
			if (health == 0)
			{
				Console.WriteLine ("<Skeletor>\nYou, mencing child...I HATE YOU HE-MAN!!");
				return true;
			}
			Console.WriteLine ("You haven't done me in, yet you boob");
			return false;
		}

		// don't why
		public void PrintAttacks(){
			Console.WriteLine ("(1) Havoc Staff (2) Insult He-Man (3) Healing Power");
		}

		/// <summary>
		/// Attack the specified CompInput where the is evalutes attackPower or healing power
		/// </summary>
		/// <param name="userInput">Computer's input.</param>
		public int Attack(int CompInput)
		{
			int attackPower = 0;
			int chances = 0;

			if (CompInput == 1)
			{
				// randomly generates attack power
				attackPower = attackGenerator.Next(25, 50);

				// the chances that Skeletor attacks
				chances = attackGenerator.Next(1, 100);
				// 70% of the time Skeletor will attack
				if (chances < 70)
				{
					if (attackPower >= 35) {
						Console.WriteLine ("Skeletor deals {0} damage with his {1}.", attackPower, StaffAttacks [1]);
					} else {
						Console.WriteLine ("Skeletor shoots {0} bolts to deal {0} damage with his Havoc Staff.", attackPower);
					}

				} else
				{
					// you miss and skeletor insults you
					Console.WriteLine("Skeletor, you need something to eat. You are just bones.\nYou miss with your {0}.", StaffAttacks[2]);
				}
			}

			// Always hits
			if (CompInput == 2)
			{
				attackPower = attackGenerator.Next(1, 10);
				Console.WriteLine(insultAttack);
			}

			// healing powers
			if (CompInput == 3)
			{
				Console.WriteLine("{0} They restore your health {1} points.", healingPower,Healing(attackGenerator.Next(10, 20)));
				attackPower = 0;  // Don't damage player
			}

			return attackPower;
		}

		/// <summary>
		/// Heals the big boss man
		/// </summary>
		/// <param name="addHealth">Randomly generated integer</param>
		/// <returns>returns addHealth</returns>
		public int Healing(int addHealth)
		{
			health += addHealth;
			return addHealth;
		}
	}
}
