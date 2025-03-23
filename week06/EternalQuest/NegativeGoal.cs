class NegativeGoal : Goal
{
    private int PenaltyPoints;

    public NegativeGoal(string name, int penaltyPoints) : base(name, -penaltyPoints)
    {
        PenaltyPoints = penaltyPoints;
    }

    public override int RecordEvent()
    {
        return -PenaltyPoints;
    }

    public override string GetStatus()
    {
        return $"[!] {Name} - Lose {PenaltyPoints} points if completed.";
    }

    public override string SaveFormat()
    {
        return $"NegativeGoal,{Name},{PenaltyPoints}";
    }
}
