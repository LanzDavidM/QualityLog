using System.Text.Json;
using System.Text.Json.Serialization;

namespace QualityLog;

public static class InspectionStorage
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        Converters = { new JsonStringEnumConverter() },
        WriteIndented = true
    };

    public static void Save(IReadOnlyList<Inspection> inspection, string path)
    {
        string jsonString = JsonSerializer.Serialize(inspection, _jsonOptions);
        File.WriteAllText(path, jsonString);
    }

    public static List<Inspection> Load(string path)
    {
        string fromFile = File.Exists(path) ? File.ReadAllText(path) : "";
        List<Inspection>? newLog = JsonSerializer.Deserialize<List<Inspection>>(fromFile, _jsonOptions);
        
        return newLog is null ? new List<Inspection>() : newLog;
    }
}