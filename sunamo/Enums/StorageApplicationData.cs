/// <summary>
/// Tento výčet říká kam se mají ukládat uživatelská data aplikace ve formě řetězců
/// </summary>
public enum StorageApplicationData
{
    Registry,
    /// <summary>
    /// App.Config nebo Web.Config
    /// </summary>
    Config,
    IniFile,
    TextFile,
    /// <summary>
    /// Stejný případ jako internalSaveLogic = false
    /// </summary>
    NoWhere
}
