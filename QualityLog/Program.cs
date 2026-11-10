using QualityLog;

InspectionLog qualityLog = new();
bool running = true;

LoadTestData();

while (running)
{
    Console.Clear();
    Console.WriteLine("1. Add inspection");
    Console.WriteLine("2. View all");
    Console.WriteLine("3. Display Reports");
    Console.WriteLine("4. Json Test");
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
        case '3':
            ConsoleUi.DisplayReports(qualityLog.GetAll());
            break;
        case '4':
            InspectionStorage.Save(qualityLog.GetAll(), "Test.txt");
            break;
        case 'x':
            running = false;
            break;
    }
}

void LoadTestData()
{
    qualityLog.Add(new Inspection(
        "123456789012345", new DateOnly(2001, 01, 01), Shift.First,
        "12345678901234567890", InspectionResult.Fail, "12345678901234567890"));
    qualityLog.Add(new Inspection(
        "FOA547422642108", new DateOnly(2002, 02, 02), Shift.Second,
        "John Doe", InspectionResult.Rework, "Missing Pin"));
    qualityLog.Add(new Inspection(
        "6024-001", new DateOnly(2003, 03, 03), Shift.Third,
        "Ian P. Freely", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "Window Spacer", new DateOnly(2004, 04, 04), Shift.First,
        "Jane Doe", InspectionResult.Fail, "Holes"));
    qualityLog.Add(new Inspection(
        "LAM-6180 27", new DateOnly(2005, 05, 05), Shift.Second,
        "Hugh Mann", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "ID10T form", new DateOnly(2006, 06, 06), Shift.Third,
        "John E. Five", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "6024-001", new DateOnly(2007, 07, 07), Shift.First,
        "Avarage Joe", InspectionResult.Fail, "Holes"));
    qualityLog.Add(new Inspection(
        "Mattel-1", new DateOnly(2008, 08, 08), Shift.Second,
        "Stan Dard", InspectionResult.Rework, "Missing Tab"));
    qualityLog.Add(new Inspection(
        "Blinker Fluid", new DateOnly(2009, 09, 09), Shift.Third,
        "Late for Dinner", InspectionResult.Fail, "Wrinkles"));
    qualityLog.Add(new Inspection(
        "Winter Air", new DateOnly(2010, 10, 10), Shift.First,
        "Stacys Mom", InspectionResult.Rework, "Missing TaB"));
    /*
    qualityLog.Add(new Inspection(
    "123456789012345", new DateOnly(2001, 01, 01), Shift.First,
    "12345678901234567890", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "FOA547422642108", new DateOnly(2002, 02, 02), Shift.Second,
        "John Doe", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "6024-001", new DateOnly(2003, 03, 03), Shift.Third,
        "Ian P. Freely", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "Window Spacer", new DateOnly(2004, 04, 04), Shift.First,
        "Jane Doe", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "LAM-6180 27", new DateOnly(2005, 05, 05), Shift.Second,
        "Hugh Mann", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "ID10T form", new DateOnly(2006, 06, 06), Shift.Third,
        "John E. Five", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "6024-001", new DateOnly(2007, 07, 07), Shift.First,
        "Avarage Joe", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "Mattel-1", new DateOnly(2008, 08, 08), Shift.Second,
        "Stan Dard", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "Blinker Fluid", new DateOnly(2009, 09, 09), Shift.Third,
        "Late for Dinner", InspectionResult.Pass, null));
    qualityLog.Add(new Inspection(
        "Winter Air", new DateOnly(2010, 10, 10), Shift.First,
        "Stacys Mom", InspectionResult.Pass, null));
    */
}