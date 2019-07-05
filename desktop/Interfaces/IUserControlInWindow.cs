using System.Windows;
using System.Windows.Controls.Primitives;

public interface IUserControlWithResult : IControlWithResult
{ 

}

/// <summary>
/// Applied to any control
/// 
/// </summary>
public interface IControlWithResult
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
    bool? DialogResult { set; }
    //ButtonBase AcceptButton { get; }
    void Accept(object input);
}

/// <summary>
/// Is the same as IUserControlWithResult
/// </summary>
public interface IUserControlInWindow : IUserControlWithResult
{
   
}

