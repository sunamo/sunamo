using System;
using System.Windows;



public interface IControlWithResultDebug : IControlWithResult
{
    int CountOfHandlersChangeDialogResult();
    void AttachChangeDialogResult(VoidBoolNullable a, bool throwException = true);
}

/// <summary>
/// Applied to any control
/// 
/// </summary>
public interface IControlWithResult
{
    /// <summary>
    /// Null není pro zavření okna, null je pro 3. tlačítko
    /// Use for attaching AttachChangeDialogResult
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

    //bool IsAttachedChangeDialogResult;
}

/// <summary>
/// Is the same as IControlWithResult
/// IUserControlInWindow - won't enable button OK in WindowWithUserControl, WindowWithUserControl cant capture ChangeDialogResult of Window
/// IControlWithResultDebug (but not IControlWithResult) - will enable button OK in WindowWithUserControl
/// </summary>
public interface IUserControlInWindow : IControlWithResult
{
   
}

