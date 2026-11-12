using System.Text.Json;
using System.Text.Json.Serialization;

namespace QualityLog;

public static class InspectionStorage
{
    private static readonly JsonSerializerOptions _jsonOptions = new ()
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
        if (!File.Exists(path))
        {
            return [];
        }
        string fromFile = File.Exists(path) ? File.ReadAllText(path) : "";
        List<Inspection>? newLog;
        try
        {
            newLog = JsonSerializer.Deserialize<List<Inspection>>(fromFile, _jsonOptions);
        }
        catch (JsonException ex)
        {
            throw new InspectionStorageException(
                $"The inspection file {path} is damaged and could not be read.", ex);
        }
        catch (IOException ex)
        {
            throw new InspectionStorageException(
                $"Trouble opening {path}", ex);
        }
        if (newLog is null)
            return [];
        foreach (Inspection record in newLog)
        {
            if (!Inspection.IsValid(record))
                throw new InspectionStorageException("Invalid records in file");
        }
        return newLog;
    }
}