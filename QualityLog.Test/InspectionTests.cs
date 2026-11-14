namespace QualityLog.Test;

public class InspectionTests
{
    private static Inspection Valid()
    {
        return new Inspection(
            "P-100",
            new DateOnly(2026, 1, 15),
            Shift.First,
            "Tester",
            InspectionResult.Pass,
            null);
    }

    [Fact]
    public void IsValid_ValidInspection_Valid()
    {
        var inspection = Valid();

        Assert.True(inspection.IsValid());
    }

    [Fact]
    public void IsValid_DefectWithResultFail_Valid()
    {
        var inspection = Valid() with
        {
            Result = InspectionResult.Fail,
            DefectType = "Holes"
        };

        Assert.True(inspection.IsValid());
    }

    [Fact]
    public void IsValid_DefectWithResultRework_Valid()
    {
        var inspection = Valid() with
        {
            Result = InspectionResult.Rework,
            DefectType = "Missing Tab"
        };

        Assert.True(inspection.IsValid());
    }

    [Fact]
    public void IsValid_DefectNullWithResultFail_Invalid()
    {
        var inspection = Valid() with
        {
            Result = InspectionResult.Fail,
            DefectType = null
        };

        Assert.False(inspection.IsValid());
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void IsValid_DefectEmptyWithResultFail_Invalid(string value)
    {
        var inspection = Valid() with
        {
            Result = InspectionResult.Fail,
            DefectType = value
        };

        Assert.False(inspection.IsValid());
    }

    [Fact]
    public void IsValid_DefectNotNullWithResultPass_Invalid()
    {
        var inspection = Valid() with
        {
            Result = InspectionResult.Pass,
            DefectType = "Holes"
        };

        Assert.False(inspection.IsValid());
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void IsValid_PartNumberEmpty_Invalid(string value)
    {
        var inspection = Valid() with { PartNumber = value };

        Assert.False(inspection.IsValid());
    }

    [Theory]
    [InlineData(15, true)]
    [InlineData(16, false)]
    public void IsValid_PartNumberLength_RespectsLimit(int value, bool expected)
    {
        var inspection = Valid() with { PartNumber = new string('A', value) };

        Assert.Equal(expected, inspection.IsValid());
    }

    [Theory]
    [InlineData(20, true)]
    [InlineData(21, false)]
    public void IsValid_InspectorLength_RespectsLimit(int value, bool expected)
    {
        var inspection = Valid() with { Inspector = new string('A', value) };

        Assert.Equal(expected, inspection.IsValid());
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsValid_DateIsNotFuture_RespectsDate(int dateModifier, bool expected)
    {
        var inspection = Valid() with { Date = DateOnly.FromDateTime(DateTime.Now).AddDays(dateModifier) };

        Assert.Equal(expected, inspection.IsValid());
    }

    [Fact]
    public void IsValid_DefaultDate_Invalid()
    {
        var inspection = Valid() with { Date = default };

        Assert.False(inspection.IsValid());
    }

    [Fact]
    public void IsValid_Shift99_Invalid()
    {
        var inspection = Valid() with { Shift = (Shift)99 };

        Assert.False(inspection.IsValid());
    }

    [Fact]
    public void IsValid_Result99_Invalid()
    {
        var inspection = Valid() with { Result = (InspectionResult)99 };

        Assert.False(inspection.IsValid());
    }
}