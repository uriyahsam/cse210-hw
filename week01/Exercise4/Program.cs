using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise4 Project.");
        Console.WriteLine();

        List<int> numbers = new List<int>(); // List to store user input

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();
            int number;

            // Validate user input
            if (!int.TryParse(input, out number))
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                continue; // Skip the rest of the loop and ask again
            }

            if (number == 0)
            {
                break; // Stop input when user enters 0
            }

            numbers.Add(number); // Add number to the list
        }

        // Check if the list is empty
        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers entered. Exiting program.");
            return;
        }

        // Compute the sum of the numbers
        int sum = numbers.Sum();
        Console.WriteLine($"The sum is: {sum}");

        // Compute the average of the numbers
        double average = numbers.Average();
        Console.WriteLine($"The average is: {average}");

        // Find the maximum (largest) number
        int maxNumber = numbers.Max();
        Console.WriteLine($"The largest number is: {maxNumber}");

        // Stretch Challenge: Find the smallest positive number (closest to zero)
        int? smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty().Min(); // Finds min positive, handles case when no positive numbers exist
        if (smallestPositive > 0)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers were entered.");
        }

        // Sort the list in ascending order and display it
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}