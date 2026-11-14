using System.Globalization;

namespace QualityLog;

public static class ConsoleUi
{

    private record Column(string Header, int Width);
    private static readonly Column PartNumberColumn =   new ("Part Number", -Inspection.MaxPartNumberLength);
    private static readonly Column DateColumn =         new ("Date", -10);
    private static readonly Column ShiftColumn =        new ("Shift", -6);
    private static readonly Column InspectorColumn =    new ("Inspector", -Inspection.MaxInspectionLength);
    private static readonly Column ResultColumn =       new ("Result", -6);
    private static readonly Column DefectColumn =       new ("Defect Type", -Inspection.MaxDefectTypeLength);
    private static readonly List<Column> _columns =
        [PartNumberColumn, DateColumn, ShiftColumn, InspectorColumn, ResultColumn, DefectColumn];

    public static void ShowError(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.Red;
        DisplayDivider();
        Console.WriteLine(message);
        DisplayDivider();
        Console.ResetColor();
        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);
    }

    public static MainMenuOptions DisplayMainMenu()
    {
        Console.Clear();
        foreach (MainMenuOptions option in Enum.GetValues<MainMenuOptions>())
        {
            Console.WriteLine($"{(int)option}. {option.GetDescription()}");
        }
        while (true)
        {
            Console.Write("\nPlease enter your selection: ");
            char key = char.ToLower(Console.ReadKey(true).KeyChar);
            if (Enum.TryParse(key.ToString(), ignoreCase: true, out MainMenuOptions selected) && Enum.IsDefined(selected))
            {
                return selected;
            }
            Console.WriteLine("\nInvalid selection. Please try again.");
        }
    }

    public static Inspection AddInspection()
    {
        string partNumber = GetString(Inspection.MaxPartNumberLength, PartNumberColumn.Header);
        DateOnly date = GetDate();
        Shift shift = GetShift();
        string inspector = GetString(Inspection.MaxInspectionLength, InspectorColumn.Header);
        InspectionResult result = GetResult();
        string? defect = result == InspectionResult.Pass ?
            null :
            GetString(Math.Abs(Inspection.MaxDefectTypeLength), DefectColumn.Header);

        return new(partNumber, date, shift, inspector, result, defect);
    }

    private static string GetString(int maxLength, string name)
    {
        Console.Clear();
        while (true)
        {
            Console.Write($"Please enter the {name}: ");
            string returnName = Console.ReadLine() ?? "";
            returnName = returnName.Trim();
            if (returnName.Length > maxLength)
                Console.WriteLine($"\nThe {name} must be no longer than {maxLength} characters long, try again");
            else if (returnName.Length == 0)
                Console.WriteLine($"\nYou can not enter an empty {name}, try again");
            else
                return returnName;
        }
    }

    private static DateOnly GetDate()
    {
        Console.Clear();
        while (true)
        {
            Console.Write("Enter date (MM/DD/YYYY) that is not in the future: ");
            if (DateOnly.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date)
                && date <= DateOnly.FromDateTime(DateTime.Today))
            {
                return date;
            }
            else
            {
                Console.WriteLine("\nNot a valid date, try again");
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
        while (true)
        {
            Console.Write("Select a Shift: ");
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int choice)
                && choice >= 1
                && choice <= Enum.GetNames<Shift>().Length)
            {
                return (Shift)choice;
            }
            Console.WriteLine("\nNot a valid Shift, try again");
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
            Console.WriteLine("\nNot a valid Inspection Result, try again");
        }
    }

    public static void DisplayAllRecords(InspectionLog records)
    {
        Console.Clear();
        DisplayDivider();
        DisplayHeader();
        DisplayDivider();
        IReadOnlyList<Inspection> log = records.GetAll();
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
        Console.Write($"{PadCell(entry.PartNumber, PartNumberColumn.Width)} ");
        Console.Write($"{PadCell(entry.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), DateColumn.Width)} ");
        Console.Write($"{PadCell(entry.Shift.ToString(), ShiftColumn.Width)} ");
        Console.Write($"{PadCell(entry.Inspector, InspectorColumn.Width)} ");
        Console.Write($"{PadCell(entry.Result.ToString(), ResultColumn.Width)} ");
        if (entry.DefectType != null)
        {
            Console.Write($"{PadCell(entry.DefectType, DefectColumn.Width)}");
        }
        Console.WriteLine();
    }

    private static string PadCell(string text, int width)
    {
        return width < 0
            ? text.PadRight(-width)
            : text.PadLeft(width);
    }

    public static void DisplayReports(IReadOnlyList<Inspection> log)
    {
        Console.Clear();
        DisplayPassRate(InspectionReport.PassRate(log));
        DisplayByType(InspectionReport.DefectsByType(log));
        DisplayResultsByShift(InspectionReport.ResultsByShift(log));
        DisplayMostCommonDefect(InspectionReport.MostCommonDefects(log));
        Console.Write("\nPress any key to continue");
        Console.ReadKey(true);
    }

    private static void DisplayPassRate(double rateAsDecimal)
    {
        Console.WriteLine($"Percentage of tests that passed: {rateAsDecimal:P1}");
    }

    private static void DisplayByType(IReadOnlyList<KeyValuePair<string, int>> defects)
    {
        Console.WriteLine("\nAll defects totaled:");

        if (defects.Count == 0)
        {
            Console.WriteLine("No defects reported");
            return;
        }

        foreach (var defect in defects)
        {
            Console.WriteLine($"{defect.Key}: {defect.Value}");
        }
    }

    private static void DisplayResultsByShift(IReadOnlyCollection<ShiftSummary> results)
    {
        Console.WriteLine("\nResults by shift:");
        foreach (ShiftSummary shift in results)
        {
            Console.Write($"{PadCell(shift.Shift.ToString(), ShiftColumn.Width)} ");
            Console.Write($"{shift.Passed}-Passed ");
            Console.Write($"{shift.Failed}-Failed ");
            Console.WriteLine($"{shift.Reworked}-Reworked");
        }
    }

    private static void DisplayMostCommonDefect(IReadOnlyList<string> defects)
    {
        Console.WriteLine("\nThe most common defect(s):");

        if (defects.Count == 0)
        {
            Console.WriteLine("No defects found");
            return;
        }

        foreach (string entry in defects)
        {
            Console.WriteLine(entry);
        }
    }

}