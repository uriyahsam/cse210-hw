using System;

class Rowing : Activity
{
    private int _strokes;
    private const double StrokeDistance = 0.015; // Approx distance per stroke in km

    public Rowing(DateTime date, int minutes, int strokes)
        : base(date, minutes)
    {
        _strokes = strokes;
    }

    public override double GetDistance() => _strokes * StrokeDistance;
    public override double GetSpeed() => (GetDistance() / Minutes) * 60;
    public override double GetPace() => Minutes / GetDistance();
}
