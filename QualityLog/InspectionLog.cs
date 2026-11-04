namespace QualityLog;

public class InspectionLog
{
    private readonly List<Inspection> _inspections = [];

    public int Count => _inspections.Count;

    public void Add(Inspection inspection)
    {
        _inspections.Add(inspection);
    }

    public IReadOnlyList<Inspection> GetAll()
    {
        return _inspections.AsReadOnly();
    }
}