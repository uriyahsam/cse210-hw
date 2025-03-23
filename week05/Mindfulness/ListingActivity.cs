using System;
using System.Collections.Generic;

class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new()
    {
        "List people that you appreciate:",
        "List personal strengths you have:",
        "List moments when you felt happy:"
    };

    protected override string GetDescription()
    {
        return "This activity encourages you to list positive aspects.";
    }

    protected override void RunActivity()
    {
        Random rnd = new Random();
        Console.WriteLine(Prompts[rnd.Next(Prompts.Count)]);
        PauseWithAnimation(3);

        List<string> responses = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("Enter an item: ");
            responses.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {responses.Count} items!");
    }
}
