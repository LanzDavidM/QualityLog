using QualityLog;

string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QualityLog");
Directory.CreateDirectory(savePath);
string saveFile = Path.Combine(savePath, "inspections.json");

InspectionLog qualityLog;

try
{
    qualityLog = new InspectionLog(InspectionStorage.Load(saveFile));
}
catch (InspectionStorageException ex)
{
    ConsoleUi.ShowError(ex.Message, "Press any key to exit");
    return;
}

bool running = true;

while (running)
{
    switch (ConsoleUi.DisplayMainMenu())
    {
        case MainMenuOptions.AddInspection:
            qualityLog.Add(ConsoleUi.AddInspection());
            try
            {
                InspectionStorage.Save(qualityLog.GetAll(), saveFile);
            }
            catch (InspectionStorageException ex)
            {
                ConsoleUi.ShowError($"{ex.Message}\nYour last entry was not saved.", "Press any key to continue");
            }
            break;
        case MainMenuOptions.ViewAll:
            ConsoleUi.DisplayAllRecords(qualityLog);
            break;
        case MainMenuOptions.DisplayReports:
            ConsoleUi.DisplayReports(qualityLog.GetAll());
            break;
        case MainMenuOptions.Exit:
            running = false;
            break;
    }
}