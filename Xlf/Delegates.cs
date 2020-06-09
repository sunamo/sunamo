/// <summary>
/// For redirect to FSApps or FS - can't add
/// Return bool? due to signalling unauthorized access - for example access .xlf in uwp 
/// </summary>
/// <param name="path"></param>
public delegate bool? ExistsDirectory(string path);