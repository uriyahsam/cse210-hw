/*
============================================================
Scripture Memorizer Program - CSE 210 | Encapsulation
============================================================

Author: URIYAH SAM
Date: March, 21, 2025
Description:
This program helps users memorize scriptures by progressively hiding words.
Users can select difficulty levels, request hints, and enable a timed challenge mode.

Features Added Beyond Requirements:
------------------------------------------------------------
✅ Multiple Scriptures: Loads scriptures from a file (`scriptures.txt`).
✅ Random Scripture Selection: A scripture is chosen randomly at the start.
✅ Intelligent Word Hiding: Ensures unique words are hidden progressively.
✅ Difficulty Levels:
   - Easy: Hides 1 word per round.
   - Medium: Hides 3 words per round.
   - Hard: Hides 5 words per round.
✅ Hint Feature: Users can type "hint" to reveal a hidden word.
✅ Timed Challenge Mode: Users can set a countdown timer to memorize within a limited time.
✅ Live Countdown Display: Shows time remaining during timed mode.
✅ Encapsulation & Clean Code: Uses separate classes for Scripture, Reference, and Word.

------------------------------------------------------------
Future Enhancements (To Be Implemented):
🔲 Sound Alert: Beep when time is almost up (not yet implemented).
🔲 High Score Tracker: Store user progress across sessions.

Instructions:
- Run the program and choose a scripture to memorize.
- Press "Enter" to hide words progressively.
- Type "hint" to reveal a word.
- Type "quit" to exit the program.

============================================================
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static bool timeUp = false;

    static void Main()
    {
        List<Scripture> scriptures = LoadScriptures("scriptures.txt");
        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found. Please check the scriptures file.");
            return;
        }

        Random random = new Random();
        Scripture scripture = scriptures[random.Next(scriptures.Count)];

        Console.WriteLine("Choose difficulty level: (1) Easy (2) Medium (3) Hard");
        int difficulty = GetUserChoice(1, 3);
        int wordsToHide = difficulty == 1 ? 1 : difficulty == 2 ? 3 : 5;

        Console.WriteLine("Would you like to enable timed mode? (y/n)");
        bool timedMode = Console.ReadLine().Trim().ToLower() == "y";

        int timerSeconds = 30; // Default timer
        if (timedMode)
        {
            Console.WriteLine("Enter the number of seconds for the timer:");
            timerSeconds = GetUserChoice(10, 120);
        }

        // Start the countdown timer in a separate thread
        if (timedMode)
        {
            Task.Run(() => StartTimer(timerSeconds));
        }

        while (!scripture.IsCompletelyHidden() && !timeUp)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (timedMode)
            {
                Console.WriteLine($"\n⏳ Time Left: {timerSeconds - (int)(DateTime.Now - startTime).TotalSeconds} seconds");
            }

            Console.WriteLine("\nOptions: Press Enter to hide words | Type 'hint' for a hint | Type 'quit' to exit");

            if (timeUp)
            {
                break; // Exit the loop when the timer reaches zero
            }

            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit")
                return;
            else if (input == "hint")
                scripture.RevealOneWord();
            else
                scripture.HideRandomWords(wordsToHide);
        }

        Console.Clear();
        if (timeUp)
        {
            Console.WriteLine("⏰ Time's up! Better luck next time.");
            Console.WriteLine($"Scripture: {scripture.GetOriginalText()}");
        }
        else
        {
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\n🎉 Memorization complete!");
        }
    }

    static DateTime startTime;
    
    static void StartTimer(int seconds)
    {
        startTime = DateTime.Now;

        while ((int)(DateTime.Now - startTime).TotalSeconds < seconds)
        {
            Thread.Sleep(1000);
        }

        timeUp = true;
    }

    static List<Scripture> LoadScriptures(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        Reference reference = new Reference(parts[0]);
                        scriptures.Add(new Scripture(reference, parts[1]));
                    }
                }
            }
        }
        return scriptures;
    }

    static int GetUserChoice(int min, int max)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        }
        return choice;
    }
}

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string reference)
    {
        string[] parts = reference.Split(' ');
        Book = string.Join(" ", parts.Take(parts.Length - 1));

        string[] verseParts = parts.Last().Split(':');
        Chapter = int.Parse(verseParts[0]);

        string[] verses = verseParts[1].Split('-');
        StartVerse = int.Parse(verses[0]);
        EndVerse = verses.Length > 1 ? int.Parse(verses[1]) : (int?)null;
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}" : $"{Book} {Chapter}:{StartVerse}";
    }
}

class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide() => IsHidden = true;
    public void Reveal() => IsHidden = false;
    public string GetDisplayText() => IsHidden ? new string('_', Text.Length) : Text;
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        List<Word> visibleWords = _words.Where(w => !w.IsHidden).ToList();
        if (visibleWords.Count == 0) return;

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            Word wordToHide = visibleWords[_random.Next(visibleWords.Count)];
            wordToHide.Hide();
            visibleWords.Remove(wordToHide);
        }
    }

    public void RevealOneWord()
    {
        List<Word> hiddenWords = _words.Where(w => w.IsHidden).ToList();
        if (hiddenWords.Count > 0)
        {
            Word wordToReveal = hiddenWords[_random.Next(hiddenWords.Count)];
            wordToReveal.Reveal();
            Console.WriteLine($"🔍 Hint: {wordToReveal.Text}");
            Thread.Sleep(1500);  // Pause to show the hint
        }
        else
        {
            Console.WriteLine("All words are already visible!");
        }
    }

    public string GetDisplayText()
    {
        return $"{_reference}\n" + string.Join(" ", _words.Select(w => w.GetDisplayText()));
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden);
    }

    public string GetOriginalText()
    {
        return $"{_reference}\n" + string.Join(" ", _words.Select(w => w.Text));
    }
}
