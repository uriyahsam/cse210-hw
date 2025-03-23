using System;

abstract class Goal
{
    protected string Name;
    protected int Points;
    private bool isCompleted;  // Change to private

    public bool IsCompleted  // Public getter and protected setter
    {
        get { return isCompleted; }
        protected set { isCompleted = value; }
    }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        isCompleted = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveFormat();

    public override string ToString()
    {
        return $"{Name} - {Points} points";
    }
}
