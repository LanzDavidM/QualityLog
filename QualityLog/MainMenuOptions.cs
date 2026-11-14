using System.ComponentModel;

namespace QualityLog;

public enum MainMenuOptions
{
    [Description("Add inspection")]
    AddInspection = 1,

    [Description("View all records")]
    ViewAll = 2,

    [Description("Display reports")]
    DisplayReports = 3,

    [Description("Exit program")]
    Exit = 9
}