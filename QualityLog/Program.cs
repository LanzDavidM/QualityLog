using QualityLog;

InspectionLog qualityLog = new();
bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("1. Add inspection");
    Console.WriteLine("2. View all");
    Console.Write("\nPlease enter your selection (x to exit): ");
    char key = char.ToLower(Console.ReadKey(true).KeyChar);
    switch (key)
    {
        case '1':
            qualityLog.Add(ConsoleUi.AddInspection());
            break;
        case '2':
            ConsoleUi.DisplayAllRecords(qualityLog);
            break;
        case 'x':
            running = false;
            break;
    }
}