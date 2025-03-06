using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise3 Project.");
        Console.WriteLine();

        Random random = new Random(); // Create a random number generator
        bool playAgain = true; // Variable to track if the user wants to play again

        while (playAgain)
        {
            int magicNumber = random.Next(1, 101); // Generate a random number between 1 and 100
            int guess = 0; // Variable to store user's guess
            int attempts = 0; // Variable to count number of guesses

            Console.WriteLine("Welcome to the 'Guess My Number' game!");
            Console.WriteLine("I have picked a number between 1 and 100. Try to guess it!");

            // Loop until the user guesses the correct number
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                string input = Console.ReadLine();

                // Validate user input
                if (!int.TryParse(input, out guess) || guess < 1 || guess > 100)
                {
                    Console.WriteLine("Invalid input! Please enter a number between 1 and 100.");
                    continue; // Skip the rest of the loop and ask again
                }

                attempts++; // Increase guess count

                // Provide hints to the user
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed it in {attempts} attempts.");
                }
            }

            // Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string response = Console.ReadLine().Trim().ToLower();

            // Check if the user wants to continue
            playAgain = response == "yes";
        }

        Console.WriteLine("Thanks for playing! Goodbye.");
    }
}