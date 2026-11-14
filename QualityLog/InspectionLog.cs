namespace QualityLog;


public class InspectionLog(List<Inspection> records)
{
    private readonly List<Inspection> _inspections = [.. records];

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