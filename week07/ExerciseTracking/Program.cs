using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2025, 3, 21), 30, 5.0),
            new Cycling(new DateTime(2025, 3, 22), 45, 20.0),
            new Swimming(new DateTime(2025, 3, 23), 30, 40),
            new Rowing(new DateTime(2025, 3, 24), 20, 500)  // Creative addition
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
