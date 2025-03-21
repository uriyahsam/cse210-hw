using System;

class Fraction
{
    private int _numerator;
    private int _denominator;

    // Constructor 1: Default to 1/1
    public Fraction()
    {
        _numerator = 1;
        _denominator = 1;
    }

    // Constructor 2: Only numerator, denominator defaults to 1
    public Fraction(int numerator)
    {
        _numerator = numerator;
        _denominator = 1;
    }

    // Constructor 3: Both numerator and denominator
    public Fraction(int numerator, int denominator)
    {
        _numerator = numerator;
        // Prevent division by zero
        _denominator = (denominator == 0) ? 1 : denominator;
    }

    // Getter for Numerator
    public int GetNumerator()
    {
        return _numerator;
    }

    // Setter for Numerator
    public void SetNumerator(int numerator)
    {
        _numerator = numerator;
    }

    // Getter for Denominator
    public int GetDenominator()
    {
        return _denominator;
    }

    // Setter for Denominator (Prevents denominator from being zero)
    public void SetDenominator(int denominator)
    {
        if (denominator != 0)
        {
            _denominator = denominator;
        }
        else
        {
            Console.WriteLine("Denominator cannot be zero. Keeping the previous value.");
        }
    }

    // Returns fraction as "A/B"
    public string GetFractionString()
    {
        return $"{_numerator}/{_denominator}";
    }

    // Returns decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)_numerator / _denominator;
    }
}
