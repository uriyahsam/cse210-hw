using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        string fileName = "journal_entries.csv"; // Default filename

        journal.LoadFromFile(fileName); // Load previous entries on startup

        while (true)
        {
            Console.WriteLine("\n======== Journal Program Menu ========");
            Console.WriteLine("1. Write a New Entry");
            Console.WriteLine("2. Display All Entries");
            Console.WriteLine("3. Save Journal");
            Console.WriteLine("4. Load Journal");
            Console.WriteLine("5. Search Entries by Keyword");
            Console.WriteLine("6. Search Entries by Date");
            Console.WriteLine("7. Edit an Entry");
            Console.WriteLine("8. Delete an Entry");
            Console.WriteLine("9. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    journal.SaveToFile(fileName); // Auto-save after writing
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    journal.SaveToFile(fileName);
                    Console.WriteLine("✅ Journal saved successfully.");
                    break;
                case "4":
                    journal.LoadFromFile(fileName);
                    Console.WriteLine("✅ Journal loaded successfully.");
                    break;
                case "5":
                    Console.Write("🔍 Enter keyword to search: ");
                    string keyword = Console.ReadLine();
                    journal.SearchEntries(keyword);
                    break;
                case "6":
                    Console.Write("📅 Enter date (MM/DD/YYYY) to search: ");
                    string date = Console.ReadLine();
                    journal.SearchByDate(date);
                    break;
                case "7":
                    Console.Write("✏️ Enter the entry number to edit: ");
                    if (int.TryParse(Console.ReadLine(), out int editIndex))
                        journal.EditEntry(editIndex - 1);
                    else
                        Console.WriteLine("❌ Invalid input.");
                    break;
                case "8":
                    Console.Write("🗑️ Enter the entry number to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteIndex))
                        journal.DeleteEntry(deleteIndex - 1);
                    else
                        Console.WriteLine("❌ Invalid input.");
                    break;
                case "9":
                    Console.WriteLine("👋 Exiting Journal. Goodbye!");
                    return;
                default:
                    Console.WriteLine("❌ Invalid choice. Try again.");
                    break;
            }
        }
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private static readonly string[] prompts =
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void AddEntry()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Length)];
        Console.WriteLine($"\n📝 Prompt: {prompt}");
        Console.Write("Your Response: ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();
        entries.Add(new Entry(date, prompt, response));
        Console.WriteLine("✅ Entry added successfully.");
    }

    public void DisplayAll()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("⚠️ No entries found.");
            return;
        }

        Console.WriteLine("\n📖 Your Journal Entries:");
        for (int i = 0; i < entries.Count; i++)
        {
            Console.WriteLine($"\n📌 Entry {i + 1}:");
            entries[i].Display();
        }
    }

    public void SaveToFile(string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Entry entry in entries)
                    writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error saving journal: {ex.Message}");
        }
    }

    public void LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("⚠️ No saved journal found.");
            return;
        }

        try
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts.Length == 3)
                    entries.Add(new Entry(parts[0], parts[1], parts[2]));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error loading journal: {ex.Message}");
        }
    }

    public void SearchEntries(string keyword)
    {
        var results = entries.Where(e => e.Response.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        if (results.Count == 0)
        {
            Console.WriteLine("🔍 No matching entries found.");
            return;
        }

        Console.WriteLine("\n🔎 Search Results:");
        foreach (var entry in results)
            entry.Display();
    }

    public void SearchByDate(string date)
    {
        var results = entries.Where(e => e.Date == date).ToList();
        if (results.Count == 0)
        {
            Console.WriteLine("📅 No entries found for that date.");
            return;
        }

        Console.WriteLine("\n📅 Entries on " + date + ":");
        foreach (var entry in results)
            entry.Display();
    }

    public void EditEntry(int index)
    {
        if (index < 0 || index >= entries.Count)
        {
            Console.WriteLine("❌ Invalid entry number.");
            return;
        }

        Console.WriteLine("\n✏️ Current Entry:");
        entries[index].Display();
        Console.Write("Enter new response: ");
        entries[index].Response = Console.ReadLine();
        Console.WriteLine("✅ Entry updated successfully.");
    }

    public void DeleteEntry(int index)
    {
        if (index < 0 || index >= entries.Count)
        {
            Console.WriteLine("❌ Invalid entry number.");
            return;
        }

        entries.RemoveAt(index);
        Console.WriteLine("🗑️ Entry deleted successfully.");
    }
}

class Entry
{
    public string Date { get; private set; }
    public string Prompt { get; private set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public void Display()
    {
        Console.WriteLine($"📆 Date: {Date}");
        Console.WriteLine($"📝 Prompt: {Prompt}");
        Console.WriteLine($"✍️ Response: {Response}\n" + new string('-', 30));
    }
}
