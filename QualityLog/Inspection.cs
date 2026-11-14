namespace QualityLog;

public record Inspection(string PartNumber, DateOnly Date, Shift Shift, string Inspector, InspectionResult Result, string? DefectType)
{
    public const int MaxPartNumberLength = 15;
    public const int MaxInspectionLength = 20;
    public const int MaxDefectTypeLength = 20;

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(PartNumber) || PartNumber.Length > MaxPartNumberLength)
            return false;
        if (Date == default || Date > DateOnly.FromDateTime(DateTime.Today))
            return false;
        if (!Enum.IsDefined(Shift))
            return false;
        if (string.IsNullOrWhiteSpace(Inspector) || Inspector.Length > MaxInspectionLength)
            return false;
        if (!Enum.IsDefined(Result))
            return false;
        if (Result == InspectionResult.Pass) // && DefectType == null)
            return DefectType is null;
        if (string.IsNullOrWhiteSpace(DefectType) || DefectType.Length > MaxDefectTypeLength)
            return false;

        return true;
    }
}