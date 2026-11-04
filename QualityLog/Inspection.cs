namespace QualityLog;

public record Inspection(string PartNumber, DateOnly Date, Shift Shift, string Inspector, InspectionResult Result, string? DefectType);