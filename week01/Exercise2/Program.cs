using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise2 Project.");
        Console.WriteLine();

        // Ask the user to enter their grade percentage
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int gradePercentage;

        // Validate user input
        if (!int.TryParse(input, out gradePercentage) || gradePercentage < 0 || gradePercentage > 100)
        {
            Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            return;
        }

        string letter = ""; // Variable to store the letter grade
        string sign = "";   // Variable to store the sign (+ or -)

        // Determine the letter grade based on the percentage
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the sign (+ or -) if the grade is not an 'F'
        int lastDigit = gradePercentage % 10; // Get the last digit of the grade

        if (letter != "A" && letter != "F") // A+ doesn't exist, and F doesn't have signs
        {
            if (lastDigit >= 7)
            {
                sign = "+"; // Add plus if last digit is 7, 8, or 9
            }
            else if (lastDigit < 3)
            {
                sign = "-"; // Add minus if last digit is 0, 1, or 2
            }
        }
        else if (letter == "A" && lastDigit < 3) 
        {
            sign = "-"; // A- exists, but not A+
        }

        // Print the final grade with sign
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Determine pass or fail message
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("You did not pass. Keep trying and you'll do better next time!");
        }
    }
}