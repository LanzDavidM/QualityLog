using System.ComponentModel;
using System.Reflection;

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

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? value.ToString();
    }
}