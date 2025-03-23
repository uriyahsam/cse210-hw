class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetStatus()
    {
        return "[∞] " + Name;
    }

    public override string SaveFormat()
    {
        return $"EternalGoal,{Name},{Points}";
    }
}
