namespace QualityLog;

public static class InspectionReport
{
    public static double PassRate(IReadOnlyList<Inspection> log)
    {
        if (log.Count == 0)
        {
            return 0;
        }
        return (double)log.Count(c => c.Result == InspectionResult.Pass) / (double)log.Count;
    }

    public static IReadOnlyList<KeyValuePair<string, int>> DefectsByType(IReadOnlyList<Inspection> log)
    {
        var defects = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (Inspection record in log)
        {
            if (record.DefectType is not null)
            {
                defects[record.DefectType] = defects.GetValueOrDefault(record.DefectType) + 1;
            }
        }

        return [.. defects.OrderByDescending(c => c.Value).ThenBy(c => c.Key)];
    }

    public static IReadOnlyList<ShiftSummary> ResultsByShift(IReadOnlyList<Inspection> log)
    {
        List<ShiftSummary> results = [];

        foreach (Shift shift in Enum.GetValues<Shift>())
        {
            int passed = log.Count(c => c.Shift == shift && c.Result == InspectionResult.Pass);
            int failed = log.Count(c => c.Shift == shift && c.Result == InspectionResult.Fail);
            int rework = log.Count(c => c.Shift == shift && c.Result == InspectionResult.Rework);
            results.Add(new ShiftSummary(shift, passed, failed, rework));
        }
        return results;
    }

    public static IReadOnlyList<string> MostCommonDefects(IReadOnlyList<Inspection> log)
    {
        var defects = DefectsByType(log);

        if (defects.Count == 0)
        {
            return [];
        }

        int highest = defects.Max(c => c.Value);

        return [.. defects.Where(c => c.Value == highest)
                      .Select(c => c.Key)
                      .Order()];
    }

}