using System;
using System.Collections.Generic;

public class ReflectingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time you stood up for someone.",
        "Recall a time when you achieved something difficult.",
        "Remember a time when you helped a friend in need."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful?",
        "What did you learn from it?",
        "How did you feel during this experience?"
    };

    public ReflectingActivity() : base("Reflecting Activity", "This activity will help you reflect on moments of strength and resilience.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();
        
        string prompt = GetRandomPrompt();
        Console.WriteLine($"Consider the following prompt:\n--- {prompt} ---");
        ShowSpinner(3);

        foreach (var question in _questions)
        {
            Console.WriteLine(question);
            ShowSpinner(5);
        }

        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        Random rand = new Random();
        return _prompts[rand.Next(_prompts.Count)];
    }
}
