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
		const int WIDTH = 80;
		const int HEIGHT = 58;

		/// <summary>
		/// Initializes a new instance of the <see cref="CombatSimulator.Game"/> class.
		/// </summary>
		public Game(){
			player = new HeMan ();
			boss = new Skeletor ();
			gameState = 1;
			// set window size
			Console.SetWindowSize (WIDTH, HEIGHT);
			Console.CursorVisible = false;
		}

		// the game loop
		public void Run()
		{
			ConsoleKeyInfo keyInfo;
			bool quit = true;
			int user_Input = 0;

			PrintTitle ();
			PrintStory ();
			Console.WriteLine ("[Enter] to play the game....");
			
			//keyInfo = Console.ReadKey (true);

			while (quit) {
				keyInfo = Console.ReadKey (true);
				if (keyInfo.Key == ConsoleKey.Enter) {
					Console.Clear ();
					PrintNames ();
					PrintHealth ();
					PrintLineBreak ();
					PrintAttacks ();
					// user input and converts it to int and bool value
					if (Validator (Console.ReadLine (), out user_Input)) {
						boss.health -= player.Attack (user_Input);
					}

					player.health -= boss.Attack ();

					// quit prompt ** note: check for either the player is dead or skeletor
					if (boss.isDead () || player.isDead ()) {
						quit = player.Quitter ();
						if (quit)
							gameState = 2;
					} else {
						Console.WriteLine ("[Enter] to continue....");
					}
				}
				if (gameState == 2) {
					player = new HeMan ();
					boss = new Skeletor ();
					gameState = 1;
				}
			}

			// exiting the program and when debugging it will stop.
			Console.WriteLine ("Skeletor and He-man retreat...\n[Enter] to exit...");
			keyInfo = Console.ReadKey (true);
			if (keyInfo.Key == ConsoleKey.Enter) {
				Console.Write ("Bye... Bye...");
			}
		}

		/// <summary>
		/// Prints the title with a black background and yellow foreground
		/// </summary>
		public void PrintTitle(){
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear ();

			string title = "#_____________________________________________________________#\n|#####_______#_______#_______#_______#_______##______#########|\n|#####|##|##|#|#####|#|##########|####|#######|#####|#########|\n|#####|##|##|#|_____|#|______####|####|______#|_____/#########|\n|#####|##|##|#|#####|#######|####|####|#######|###\\###########|\n|#_###|#_|__|#|#_###|#______|####|####|______#|#___\\___#______|\n|#|#####|#|\\####|#_____#_##of#the_____#_______##|#######|#####|\n|#|#####|#|#\\###|###|####\\####/#|______#|#####|#|______#|_____|\n|#|#####|#|##\\##|###|#####\\##/##|______#|_____/#######|#|#####|\n|#|_____|#|###\\_|#__|__####\\/###|______#|####\\_#______|#|_____|\n|_____________________________________________________________|\n";

			for(int i = 0; i < title.Length; i++){
				if (title [i] == '#') {
					Console.ForegroundColor = ConsoleColor.Black;
				}
				if (title [i] == '_' || title[i] == '|' || title[i] == '/' || title[i] == '\\') {
					Console.ForegroundColor = ConsoleColor.Yellow;
				}
				if (Char.IsLetter (title [i])) {
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				Console.Write(title[i]);
			}
			Console.ForegroundColor = ConsoleColor.White;
		}

		public void PrintStory(){
			string text = "You finally found Skeletor and trade off insults back and forth.\n\nSkeletor is a bad ass villain that loves to think of ways to conquer the\n universe.  On his spare time he likes to play Counter-Strike, but you keep his from this.\n\nYou can use your sword, you raise your sword and yell out “I have the power!!!”, or you can eat a bag of chips.\n\nYour sword does most of the damage, but for 70% of the time. When you raise your sword and yell out “I have the power!!!” always hits Skeletor because He-man is a stupid jock that doesn't know anything else to yell out and deals a little\n damage. Or you can eat a bag of chips to heal you.\n\n<Skeletor>\tHe-man, you are a foolish child. Why don't just leave alone?";
			Console.WriteLine (text);
		}

		/// <summary>
		///  displays the names of our players.
		/// </summary>
		public void PrintNames(){
			Console.SetCursorPosition (10, 0);
			Console.Write (player.name);
			Console.SetCursorPosition (Console.WindowWidth - boss.name.Length - 10, 0);
			Console.WriteLine (boss.name);
		}

		/// <summary>
		/// Prints the health of the players
		/// </summary>
		public void PrintHealth(){
			Console.SetCursorPosition (5, 1);
			Console.BackgroundColor = ConsoleColor.DarkRed;
			for (int hp = 0; hp < player.health; hp+=5) {
				Console.Write (" ");
			}
			Console.SetCursorPosition (Console.WindowWidth - boss.health/5 - 5, 1);
			for (int hp = 0; hp < boss.health; hp+=5) {
				Console.Write (" ");
			}
			Console.WriteLine ();
			Console.BackgroundColor = ConsoleColor.Black;
		}

		/// <summary>
		/// Prints the line break.
		/// </summary>
		public void PrintLineBreak(){
			for (int line = 0; line < Console.WindowWidth; line++) {
				Console.Write ("_");
			}
		}

		public void PrintAttacks(){
			Console.SetCursorPosition (0,4);
			player.PrintAttacks ();
		}

		/// <summary>
		/// Validator the specified userInput.
		/// </summary>
		/// <param name="userInput">User input.</param>
		/// <returns>Invalid input</returns>
		static bool Validator(string userInput, out int output)
		{
			if (int.TryParse(userInput, out output))
			{
				Console.WriteLine ("<Skeletor>\nIs that the only thing you can do right?");
			}
			if (output <= 3 && output != 0) {
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
		public string name = "He-Man";

		// private variables
		private string[] swordAttacks = { "Sword slash", "Uppercut sword slash", "Weak slash" };
		private string insultAttack = "You raise your sword and yell out: I HAVE THE POWER!!!!";
		private string healingPower = "You reach for a bag of chips and eat them.";
		private Random attackGenerator = new Random();

		// when created
		public HeMan()
		{
			health = 100;
		}

		// checks if He-Man is dead or not.
		public bool isDead()
		{
			if (health <= 0)
			{
				Console.WriteLine ("You fail to defeat Skeletor");
				return true;
			}
			Console.WriteLine ("<He-Man>\nDamn, old man come back here and fight me.");
			return false;
		}

		public void PrintAttacks(){
			Console.WriteLine ("(1) Sword\n(2) Annoy Shout\n(3) Eat Food");
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
						Console.WriteLine ("You deal {0} damage a great feat with your {1}.\nWay to go, He-man...", attackPower, swordAttacks [1]);
					} else {
						Console.WriteLine ("You use your {0} and hit Skeletor with {1} damage.", swordAttacks[0], attackPower);
					}

				}else
				{
					// you miss and skeletor insults you
					Console.WriteLine("<Skeletor>\nYou weakling, go back to your football.\nYou miss with your {0}.", swordAttacks[2]);
					attackPower = 0;
				}
			}

			// always hits
			if (userInput == 2)
			{
				attackPower = attackGenerator.Next(1, 15);
				Console.WriteLine("{0}\nYou deal {1} damage to Skeletor.",insultAttack, attackPower);
			}

			// healing food
			if (userInput == 3)
			{
				attackPower = Healing (attackGenerator.Next (10, 20));
				Console.WriteLine("{0}\n They restore your health {1} points.", healingPower, attackPower);
				if (attackPower < 0) {
					Console.WriteLine ("You were full.");
				}
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
			if (health + addHealth >= 100) {
				addHealth = 100 - health - addHealth;
			}
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
		public string name = "Skeletor";
		// private variables
		private string[] StaffAttacks = { "Bolts", "Thief of Dreams", "Staff missed fires" };
		private string insultAttack = "Your furry, flea bitten fool I'll cover my throne with your hide.";
		private string healingPower = "Skeletor raises his staff over his head, yells out \"Comin' you Royal boob\"";
		private Random attackGenerator = new Random();

		// Creates Skeletor with health of 200. He doesn't say anything because he is a bad-ass
		public Skeletor()
		{
			health = 200;
		}

		// checks if Skeletor has fallen or not.
		public bool isDead()
		{
			if (health <= 0)
			{
				Console.WriteLine ("<Skeletor>\nYou, mencing child...I HATE YOU HE-MAN!!");
				return true;
			}
			Console.WriteLine ("<Skeletor>\nYou haven't done me in, yet you boob");
			return false;
		}


		public void PrintAttack(int input){
			switch (input){
			case 1:
				{
					Console.WriteLine ("Skeletor uses his staff attack.");
					break;
				}
			case 2:
				{
					Console.WriteLine ("Skeletor insults you.");
					break;
				}
			case 3:
				{
					Console.WriteLine ("Skeletor uses his machines to heal.");
					break;
				}

			}
		}

		/// <summary>
		/// Attack the specified CompInput where the is evalutes attackPower or healing power
		/// </summary>
		/// <param name="userInput">Computer's input.</param>
		public int Attack()
		{
			int CompInput = attackGenerator.Next(1,3);
			int chances = 0;
			int attackPower = 0;

			if (health < 15) {
				CompInput = 3;
			}

			//PrintAttack (CompInput);
			Console.WriteLine ();

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
					Console.WriteLine("<He-Man>\nSkeletor, you need something to eat. You are just bones.\nSkeletor miss with his {0}.", StaffAttacks[2]);
					attackPower = 0;
				}
			}

			// Always hits
			if (CompInput == 2)
			{
				attackPower = attackGenerator.Next(1, 10);
				Console.WriteLine("<Skeletor>\n{0}", insultAttack);
				Console.WriteLine ("Skeletor dealt {0} damage to you.", attackPower);
			}

			// healing powers
			if (CompInput == 3)
			{
				attackPower = Healing(attackGenerator.Next(10, 20));
				Console.WriteLine("{0} That restore his health {1} points.", healingPower, attackPower);
				if (attackPower < 0) {
					Console.WriteLine ("Skeletor was full.");
				}
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
			if (health + addHealth >= 200) {
				addHealth = 200 - health - addHealth;
			}
			health += addHealth;
			return addHealth;
		}
	}
}
