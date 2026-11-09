using QualityLog;

namespace QualityLog.Test;

public class InspectionReportTests
{
    
    private static Inspection Make(Shift shift, InspectionResult result, string? defect = null)
    {
        return new Inspection("P-1", new DateOnly(2026, 1, 1), shift, "Tester", result, defect);
    }

    private static List<Inspection> SampleLog()
    {
        return
        [
            Make(Shift.First,  InspectionResult.Fail,   "12345678901234567890"),
            Make(Shift.Second, InspectionResult.Rework, "Missing Pin"),
            Make(Shift.Third,  InspectionResult.Pass),
            Make(Shift.First,  InspectionResult.Fail,   "Holes"),
            Make(Shift.Second, InspectionResult.Pass),
            Make(Shift.Third,  InspectionResult.Pass),
            Make(Shift.First,  InspectionResult.Fail,   "Holes"),
            Make(Shift.Second, InspectionResult.Rework, "Missing Tab"),
            Make(Shift.Third,  InspectionResult.Fail,   "Wrinkles"),
            Make(Shift.First,  InspectionResult.Rework, "Missing TaB")
        ];
    }  
    
    [Fact]
    public void PassRate_EmptyLog_ReturnsZero()
    {
        var log = new List<Inspection>();

        double result = InspectionReport.PassRate(log);

        Assert.Equal(0, result);

    }

    [Fact]
    public void PassRate_SampleLog_ReturnThirtyPercent()
    {
        var log = SampleLog();

        double result = InspectionReport.PassRate(log);

        Assert.Equal(0.3, result, 10);
    }

    [Fact]
    public void DefectsByType_SampleLog_ReturnsGroupedDefectsWithCount()
    {
        var log = SampleLog();

        IReadOnlyList<KeyValuePair<string, int>> result = InspectionReport.DefectsByType(log);

        var expected = new List<KeyValuePair<string, int>>
        {
            new("Holes", 2),
            new("Missing Tab", 2),
            new("12345678901234567890", 1),
            new("Missing Pin", 1),
            new("Wrinkles", 1)
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DefectsByType_EmptyList_ReturnsEmptyList()
    {
        var log = new List<Inspection>();

        IReadOnlyList<KeyValuePair<string, int>> result = InspectionReport.DefectsByType(log);

        Assert.Empty(result);
    }

    [Fact]
    public void MostCommonDefects_SampleLog_ReturnsTopDefect()
    {
        var log = SampleLog();

        IReadOnlyList<string> result = InspectionReport.MostCommonDefects(log);

        var expected = new List<string>
        {
            "Holes",
            "Missing Tab"
        };


        Assert.Equal(expected, result);
    }

    [Fact]
    public void MostCommonDefects_EmptyList_ReturnsEmptyList()
    {
        var log = new List<Inspection>();

        IReadOnlyList<string> result = InspectionReport.MostCommonDefects(log);

        Assert.Empty(result);
    }

    [Fact]
    public void ResultByShift_SampleLog_ReturnsFilledOutShiftSummary()
    {
        var log = SampleLog();

        IReadOnlyList<ShiftSummary> result = InspectionReport.ResultsByShift(log);

        var expected = new List<ShiftSummary>
        {
            new (Shift.First, 0, 3, 1),
            new (Shift.Second, 1, 0, 2),
            new (Shift.Third, 2, 1, 0)
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ResultByShift_EmptyLog_ReturnsThreeRollsAllZeros()
    {
        var log = new List<Inspection>();

        IReadOnlyList<ShiftSummary> result = InspectionReport.ResultsByShift(log);
        var expected = new List<ShiftSummary>
        {
            new (Shift.First, 0, 0, 0),
            new (Shift.Second, 0, 0, 0),
            new (Shift.Third, 0, 0, 0)
        };


        Assert.Equal(expected, result);
    }
}