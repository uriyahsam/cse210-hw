using System;
using System.Collections.Generic;
using System.IO;

class GoalManager
{
    public List<Goal> Goals { get; private set; }
    public int Score { get; private set; }

    public GoalManager()
    {
        Goals = new List<Goal>();
        Score = 0;
    }

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public void RecordGoalEvent(int index)
    {
        if (index >= 0 && index < Goals.Count)
        {
            int pointsEarned = Goals[index].RecordEvent();
            Score += pointsEarned;
            Console.WriteLine($"You earned {pointsEarned} points! Total Score: {Score}");
        }
    }

    public void ShowGoals()
    {
        for (int i = 0; i < Goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Goals[i].GetStatus()}");
        }
    }

    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(Score);
            foreach (var goal in Goals)
            {
                writer.WriteLine(goal.SaveFormat());
            }
        }
    }

    public void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            string[] lines = File.ReadAllLines("goals.txt");
            Score = int.Parse(lines[0]);
            Goals.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                switch (parts[0])
                {
                    case "SimpleGoal":
                        SimpleGoal simpleGoal = new SimpleGoal(parts[1], int.Parse(parts[2]));
                        if (bool.Parse(parts[3]))  // Convert saved string to bool
                        {
                            simpleGoal.RecordEvent();  // Mark it as completed
                        }
                        Goals.Add(simpleGoal);
                        break;
                    case "EternalGoal":
                        Goals.Add(new EternalGoal(parts[1], int.Parse(parts[2])));
                        break;
                    case "ChecklistGoal":
                        Goals.Add(new ChecklistGoal(parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[5])));
                        break;
                    case "NegativeGoal":
                        Goals.Add(new NegativeGoal(parts[1], int.Parse(parts[2])));
                        break;
                }
            }
        }
    }
}
