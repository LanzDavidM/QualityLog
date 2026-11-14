namespace QualityLog.Test;

public class InspectionLogTests
{
    private readonly List<Inspection> _testList =
        [
        new("P-100", new DateOnly(2026, 1, 15), Shift.First, "Tester", InspectionResult.Pass, null),
        new("Prick-E-5", new DateOnly(2026, 3, 25), Shift.Second, "Ian P. Freely", InspectionResult.Rework, "Missing Stripe")
        ];

    [Fact]
    public void Add_AddRecord_IncreaseCount()
    {
        var log = new InspectionLog(_testList);
        int countBefore = log.Count;

        log.Add(new("Summer Air", new DateOnly(2026, 4, 30), Shift.Third, "John Jacob Schmidt", InspectionResult.Fail, "Cold"));

        Assert.Equal(countBefore + 1, log.Count);
    }

    [Fact]
    public void Add_AddingInspection_InspectionAdded()
    {
        var log = new InspectionLog(_testList);
        var added = new Inspection("Summer Air", new DateOnly(2026, 4, 30), Shift.Third, "John Jacob Schmidt", InspectionResult.Fail, "Cold");

        log.Add(added);

        Assert.Contains(added, log.GetAll());
    }

    [Fact]
    public void Add_ToLog_DoesNotChangeOriginalList()
    {
        int originalListCount = _testList.Count;
        var log = new InspectionLog(_testList);

        log.Add(new Inspection("Summer Air", new DateOnly(2026, 4, 30), Shift.Third, "John Jacob Schmidt", InspectionResult.Fail, "Cold"));

        Assert.Equal(originalListCount, _testList.Count);
    }
}