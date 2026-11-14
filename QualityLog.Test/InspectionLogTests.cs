using QualityLog;

namespace QualityLog.Test;

public  class InspectionLogTests
{

    [Fact]
    public void Add_GetAllHasItem_ItemAdded()
    {
        List<Inspection> testList =
            [
            new("P-100", new DateOnly(2026, 1, 15), Shift.First, "Tester", InspectionResult.Pass, null),
            new("Prick-E-5", new DateOnly(2026, 3, 25), Shift.Second, "Ian P. Freely", InspectionResult.Rework, "Missing Stripe")
            ];

        InspectionLog testLog = new InspectionLog(testList);

        testLog.Add(new("Summer Air", new DateOnly(2026, 4, 30), Shift.Third, "John Jacob Schmidt", InspectionResult.Fail, "Cold"));

        Assert.False(testLog.GetAll() == testList);
    }
}