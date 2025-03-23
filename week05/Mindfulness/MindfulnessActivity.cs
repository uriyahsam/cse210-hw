using System;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int duration;

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine($"Starting {this.GetType().Name}...");
        Console.WriteLine(GetDescription());
        Console.Write("Enter duration (in seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithAnimation(3);
        RunActivity();
        EndActivity();
    }

    protected abstract void RunActivity();
    protected abstract string GetDescription();

    protected void PauseWithAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\rStarting in {i}...");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected void EndActivity()
    {
        Console.WriteLine("Well done! You have completed the activity.");
        Console.WriteLine($"You completed {this.GetType().Name} for {duration} seconds.");
        PauseWithAnimation(3);
    }
}
