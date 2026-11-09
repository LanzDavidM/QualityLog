namespace QualityLog;

public record ShiftSummary(Shift Shift, int Passed, int Failed, int Reworked);