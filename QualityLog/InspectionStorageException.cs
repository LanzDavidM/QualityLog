namespace QualityLog;

public class InspectionStorageException : Exception
{
    public InspectionStorageException(string message) 
        : base(message)
    {
    }    
    public InspectionStorageException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

}