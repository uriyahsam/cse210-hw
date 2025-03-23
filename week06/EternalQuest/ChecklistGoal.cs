class ChecklistGoal : Goal
{
    private int TargetCount;
    private int CurrentCount;
    private int BonusPoints;

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints)
        : base(name, points)
    {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        CurrentCount = 0;
    }

    public override int RecordEvent()
    {
        if (CurrentCount < TargetCount)
        {
            CurrentCount++;
            if (CurrentCount == TargetCount)
            {
                IsCompleted = true;
                return Points + BonusPoints;
            }
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return $"[{(IsCompleted ? "X" : " ")}] {Name} - Completed {CurrentCount}/{TargetCount} times";
    }

    public override string SaveFormat()
    {
        return $"ChecklistGoal,{Name},{Points},{TargetCount},{CurrentCount},{BonusPoints}";
    }
}
