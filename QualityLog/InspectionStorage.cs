using System.Text.Json;
using System.Text.Json.Serialization;

namespace QualityLog;

public static class InspectionStorage
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        WriteIndented = true
    };

    public static void Save(IReadOnlyList<Inspection> inspections, string path)
    {
        string tempPath = path + ".tmp";
        string jsonString = JsonSerializer.Serialize(inspections, _jsonOptions);
        try
        {
            File.WriteAllText(tempPath, jsonString);
            File.Move(tempPath, path, overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new InspectionStorageException(
                $"Problem accessing file at: {path}", ex);
        }
    }

    public static List<Inspection> Load(string path)
    {
        string fromFile;

        if (!File.Exists(path))
        {
            return [];
        }
        try
        {
            fromFile = File.Exists(path) ? File.ReadAllText(path) : "";
        }
        catch (IOException ex)
        {
            throw new InspectionStorageException(
                $"Trouble opening {path}", ex);
        }
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
        if (newLog is null)
            return [];
        for (int i = 0; i < newLog.Count; i++)
        {
            if (newLog[i] is null || !newLog[i].IsValid())
                throw new InspectionStorageException($"Invalid data at record number: {i}");
        }
        return newLog;
    }
}