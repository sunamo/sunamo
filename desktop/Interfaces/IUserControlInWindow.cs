using System.Windows;

public interface IUserControlWithResult
{
    /// <summary>
    /// Null není pro zavření okna, null je pro 3. tlačítko
    /// </summary>
    event VoidBoolNullable ChangeDialogResult;
    /// <summary>
    /// Do Set zapiš jen ChangeDialogResult(value); 
    /// It is construction from WF apps and protect if handler will be null.
    /// 
    /// </summary>
    bool? DialogResult {set;}
}

public interface IUserControlInWindow : IUserControlWithResult
{

}
