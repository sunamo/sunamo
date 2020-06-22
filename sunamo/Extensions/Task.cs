using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

/// <summary>
/// Never use Task, async, await!!! 
/// Async really is not debuggable!!
/// 3h I change from Task, async, await to normal!!!
/// </summary>
public static class TaskExtensions
{
    public static ConfiguredTaskAwaitable Conf(this Task t)
    {
        return t.ConfigureAwait(true);
    }

    public static ConfiguredTaskAwaitable<T> Conf<T>(this Task<T> t)
    {
        return t.ConfigureAwait(true);
    }

    public static void LogExceptions(this Task task)
    {
        task.ContinueWith(t =>
        {
            var aggException = t.Exception.Flatten();
            foreach (var exception in aggException.InnerExceptions)
            {
                ThisApp.SetStatus(TypeOfMessage.Error, Exceptions.TextOfExceptions(exception));
            }
        },
        TaskContinuationOptions.OnlyOnFaulted);
    }
}