using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "List things you are grateful for.",
        "List people who have influenced you positively.",
        "List things that make you happy."
    };

    public ListingActivity() : base("Listing Activity", "This activity will help you focus on positive aspects of life by listing meaningful things.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();
        
        string prompt = GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        ShowSpinner(3);

        List<string> responses = GetListFromUser();
        Console.WriteLine($"You listed {responses.Count} items.");

        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        Random rand = new Random();
        return _prompts[rand.Next(_prompts.Count)];
    }

    private List<string> GetListFromUser()
    {
        List<string> responses = new List<string>();
        Console.WriteLine("Start listing your responses (press Enter after each, type 'done' to finish):");

        while (true)
        {
            string response = Console.ReadLine();
            if (response.ToLower() == "done")
                break;
            responses.Add(response);
        }

        return responses;
    }
}
