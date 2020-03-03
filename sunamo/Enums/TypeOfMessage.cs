
/// <summary>
/// Error/Warning are in tbLastErrorOrWarning, other in tbLastOtherMessage
/// </summary>
public enum TypeOfMessage
{
    /// <summary>
    /// tbLastErrorOrWarning
    /// </summary>
    Error,
    /// <summary>
    /// tbLastErrorOrWarning
    /// </summary>
    Warning,
    Information,
    /// <summary>
    /// Returned if from text cant determine value
    /// </summary>
    Ordinal,
    Appeal,
    Success
}