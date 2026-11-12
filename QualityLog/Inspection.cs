using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QualityLog;

public record Inspection(string PartNumber, DateOnly Date, Shift Shift, string Inspector, InspectionResult Result, string? DefectType)
{
    public static readonly List<(string Header, int Width)> columns = [
      ("Part Number", -15),
      ("Date",        -10),
      ("Shift",        -6),
      ("Inspector",   -20),
      ("Result",       -6),
      ("Defect Type", -20) 
      ];

    public static bool IsValid(Inspection ToTest)
    {
        if (string.IsNullOrEmpty(ToTest.PartNumber) || ToTest.PartNumber.Length > Math.Abs(columns[0].Width))
            return false;
        if (ToTest.Date == default || ToTest.Date > DateOnly.FromDateTime(DateTime.Today))
            return false;
        if (!Enum.IsDefined(ToTest.Shift))
            return false;
        if (string.IsNullOrEmpty(ToTest.Inspector) || ToTest.Inspector.Length > Math.Abs(columns[3].Width))
            return false;
        if (!Enum.IsDefined(ToTest.Result))
            return false;
        if (ToTest.Result == InspectionResult.Pass && ToTest.DefectType == null)
                return true;
        if (string.IsNullOrEmpty(ToTest.DefectType) || ToTest.DefectType.Length > Math.Abs(columns[5].Width))
            return false;

        return true;
    }
}