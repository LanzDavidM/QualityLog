using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading.Channels;

namespace QualityLog;

public static class ConsoleUi
{
    private static readonly List<(string Header, int Width)> _columns = [
        ("Part Number", -15),
        ("Date",        -10),
        ("Shift",        -6),
        ("Inspector",   -20),
        ("Result",       -6),
        ("Defect Type", -20)
        ];

    public static Inspection AddInspection()
    {
        (string name, int width) = _columns[0];
        string partNumber = GetString(Math.Abs(width), name);
        DateOnly date = GetDate();
        Shift shift = GetShift();
        (name, width) = _columns[3];
        string inspector = GetString(Math.Abs(width), name);
        InspectionResult result = GetResult();
        (name, width) = _columns[5];
        string? defect = null;
        if (result != InspectionResult.Pass)
        {
            defect = GetString(Math.Abs(width), name);
        }

        return new(partNumber, date, shift, inspector, result, defect);
    }

    private static string GetString(int maxLength, string name)
    {
        Console.Clear();
        while (true)
        {
            Console.Write($"Enter the {name}: ");
            string returnName = Console.ReadLine() ?? "";
            returnName = returnName.Trim();
            if (returnName.Length > maxLength)
            {
                Console.WriteLine($"\nThe {name} must be no longer than {maxLength} long, try again");
            }
            else if (returnName.Length == 0)
            {
                Console.WriteLine($"\nYou can not enter an empty {name}, try again");
            }
            else
            {
                return returnName;
            }
        }
    }
    
    private static DateOnly GetDate()
    {
        Console.Clear();
        while (true)
        {
            Console.Write("Enter date (MM/DD/YYYY):");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly date))
            {
                return date;
            }
            else
            {
                Console.WriteLine($"\n{date} is not a valid date, try again");
            }
        }
    }

    private static Shift GetShift()
    {
        Console.Clear();
        foreach (Shift shift in Enum.GetValues<Shift>())
        {
            Console.WriteLine($"{(int)shift}. {shift}");
        }
        while(true)
        {
            Console.Write("Select a Shift: ");
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int choice)
                && choice >= 1
                && choice <= Enum.GetNames<Shift>().Length)
            {
                return (Shift)choice;
            }
            Console.WriteLine("Not a valid Shift, try again");
        }
    }

    private static InspectionResult GetResult()
    {
        Console.Clear();
        foreach (InspectionResult result in Enum.GetValues<InspectionResult>())
        {
            Console.WriteLine($"{(int)result}. {result}");
        }
        while (true)
        {
            Console.Write("Select an Inspection Result: ");
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int choice)
                && choice >= 1
                && choice <= Enum.GetNames<InspectionResult>().Length)
            {
                return (InspectionResult)choice;
            }
            Console.WriteLine("Not a valid Inspection Result, try again");
        }
    }

    public static void DisplayAllRecords(InspectionLog Records)
    {
        Console.Clear();
        DisplayDivider();
        DisplayHeader();
        DisplayDivider();
        IReadOnlyList<Inspection> log = Records.GetAll();
        if (log.Count == 0)
        {
            Console.WriteLine("No inspections recorded");
        }
        else
        {
            foreach (Inspection entry in log)
            {
                DisplayRow(entry);
            }
        }
        DisplayDivider();
        Console.Write("\nPress any key to continue");
        Console.ReadKey(true);
    }

    private static void DisplayDivider()
    {
        foreach (var (_, width) in _columns)
        {
            for (int i = 0; i < Math.Abs(width) + 1; i++)
            {
                Console.Write("-");
            }
        }
        Console.WriteLine();
    }

    private static void DisplayHeader()
    {
        foreach (var (header, width) in _columns)
        {
            Console.Write($"{PadCell(header, -Math.Abs(width))} ");
        }
        Console.WriteLine();
    }

    private static void DisplayRow(Inspection entry)
    {
        Console.Write($"{PadCell(entry.PartNumber, _columns[0].Width)} ");
        Console.Write($"{PadCell(entry.Date.ToString("MM/dd/yyyy"), _columns[1].Width)} ");
        Console.Write($"{PadCell(entry.Shift.ToString(), _columns[2].Width)} ");
        Console.Write($"{PadCell(entry.Inspector, _columns[3].Width)} ");
        Console.Write($"{PadCell(entry.Result.ToString(), _columns[4].Width)} ");
        if (entry.DefectType != null)
        {
            Console.Write($"{PadCell(entry.DefectType, _columns[5].Width)}");
        }
        Console.WriteLine();
    }

    private static string PadCell(string text, int width)
    {
        return width < 0
            ? text.PadRight(-width)
            : text.PadLeft(width);
    }
}