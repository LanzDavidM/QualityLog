namespace QualityLog.Test;

public class InspectionStorageTests : IDisposable
{
    private readonly string _path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.josn");

    private readonly List<Inspection> _testList =
        [
        new("P-100", new DateOnly(2026, 1, 15), Shift.First, "Tester", InspectionResult.Pass, null),
        new("Prick-E-5", new DateOnly(2026, 3, 25), Shift.Second, "Ian P. Freely", InspectionResult.Rework, "Missing Stripe"),
        new("Summer Air", new DateOnly(2026, 4, 30), Shift.Third, "John Jacob Schmidt", InspectionResult.Fail, "Cold")
        ];

    public void Dispose()
    {
        if (File.Exists(_path))
            File.Delete(_path);
        if (File.Exists(_path + ".tmp"))
            File.Delete(_path + ".tmp");
    }

    [Fact]
    public void RoundTrip_ReturnsTrue()
    {
        InspectionStorage.Save(_testList, _path);
        List<Inspection> returnList = InspectionStorage.Load(_path);

        Assert.Equal(_testList, returnList);
    }

    [Fact]
    public void EmptyListRoundTrip_ReturnsTrue()
    {
        List<Inspection> testList = [];

        InspectionStorage.Save(testList, _path);
        List<Inspection> returnList = InspectionStorage.Load(_path);

        Assert.Empty(returnList);
    }

    [Fact]
    public void MissingFile_ReturnsTrue()
    {
        List<Inspection> returnList = InspectionStorage.Load(_path);
        Assert.Empty(returnList);
    }

    [Fact]
    public void GarbageFile_ThrowsException()
    {
        File.WriteAllText(_path, "this is not json");

        Assert.Throws<InspectionStorageException>(() => InspectionStorage.Load(_path));
    }

    [Fact]
    public void EmptyFile_ThrowsException()
    {
        File.WriteAllText(_path, "");

        Assert.Throws<InspectionStorageException>(() => InspectionStorage.Load(_path));
    }

    [Fact]
    public void InvalidRecord_ThrowsException()
    {
        List<Inspection> testList =
            [..
            _testList,
            new("Blinker Fluid", new DateOnly(2026, 4, 1), Shift.Third, "John Jacob Jingleheimer Schmidt", InspectionResult.Pass, null)
            ];

        InspectionStorage.Save(testList, _path);

        Assert.Throws<InspectionStorageException>(() => InspectionStorage.Load(_path));
    }

    [Fact]
    public void EnumsReadable_ReturnsTrue()
    {
        InspectionStorage.Save(_testList, _path);

        string fileContent = File.ReadAllText(_path);

        Assert.Contains("\"Second\"", fileContent);
    }

    [Fact]
    public void Save_FolderDoesNotExist_ThrowsException()
    {
        string newPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "x.json");

        Assert.Throws<InspectionStorageException>(() => InspectionStorage.Save(_testList, newPath));
    }

    [Fact]
    public void NullEntry_ThrowsException()
    {
        File.WriteAllText(_path, "[null]");

        Assert.Throws<InspectionStorageException>(() => InspectionStorage.Load(_path));
    }

    [Fact]
    public void FileContainingNull_ReturnsEmptyList()
    {
        File.WriteAllText(_path, "null");

        Assert.Empty(InspectionStorage.Load(_path));
    }
}