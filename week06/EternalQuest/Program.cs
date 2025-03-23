using System;

class Program
{
    static void Main()
    {
        GoalManager goalManager = new GoalManager();
        goalManager.LoadGoals();

        while (true)
        {
            Console.WriteLine("\n=== Eternal Quest ===");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Goal Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Save and Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Goal Name: ");
                string name = Console.ReadLine();
                Console.Write("Points: ");
                int points = int.Parse(Console.ReadLine());

                Console.WriteLine("1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal\n4. Negative Goal");
                int type = int.Parse(Console.ReadLine());

                switch (type)
                {
                    case 1: goalManager.AddGoal(new SimpleGoal(name, points)); break;
                    case 2: goalManager.AddGoal(new EternalGoal(name, points)); break;
                    case 3:
                        Console.Write("Target Count: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Bonus Points: ");
                        int bonus = int.Parse(Console.ReadLine());
                        goalManager.AddGoal(new ChecklistGoal(name, points, target, bonus));
                        break;
                    case 4: goalManager.AddGoal(new NegativeGoal(name, points)); break;
                }
            }
            else if (choice == 2)
            {
                goalManager.ShowGoals();
                Console.Write("Enter goal number to record: ");
                goalManager.RecordGoalEvent(int.Parse(Console.ReadLine()) - 1);
            }
            else if (choice == 3) goalManager.ShowGoals();
            else if (choice == 4) { goalManager.SaveGoals(); break; }
        }
    }
}
