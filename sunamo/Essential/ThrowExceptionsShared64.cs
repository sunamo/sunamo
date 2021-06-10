using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowExceptions
{
    public static Type type = typeof(ThrowExceptions);
    public static Action<string, string> writeServerError;
    static string dot = ".";

    public static void IsTheSame(string stacktrace, object type, string methodName, string fst, string sec)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsTheSame(FullNameOfExecutedCode(type, methodName), fst, sec));
    }

    

    public static void ArgumentOutOfRangeException(string stacktrace, object type, string methodName, string paramName, string message = null)
    {
        ThrowIsNotNull(stacktrace, Exceptions.ArgumentOutOfRangeException(FullNameOfExecutedCode(type, methodName, true), paramName, message));
    }

    public static void IsNotWindowsPathFormat(string stacktrace, object type, string methodName, string argName, string argValue)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNotWindowsPathFormat(FullNameOfExecutedCode(type, methodName, true), argName, argValue));
    }

    public static void IsNullOrEmpty(string stacktrace, object type, string methodName, string argName, string argValue)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNullOrEmpty(FullNameOfExecutedCode(type, methodName, true), argName, argValue));
    }

    public static void NotImplementedMethod(string stacktrace, object type, string methodName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.NotImplementedMethod(FullNameOfExecutedCode(type, methodName)));
    }

    /// <summary>
    /// Return & throw exception whether
    /// directory NOT exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="folder1"></param>
    public static void DirectoryWasntFound(string stacktrace, object type, string methodName, string folder1)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DirectoryWasntFound(FullNameOfExecutedCode(type, methodName, true), folder1));
    }



    public static void DivideByZero(string stacktrace, object type, string methodName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DivideByZero(FullNameOfExecutedCode(type, methodName, true)));
    }
    public static void NotImplementedCase(string stacktrace, object type, string methodName, object niCase)
    {
        ThrowIsNotNull(stacktrace, Exceptions.NotImplementedCase(FullNameOfExecutedCode(type, methodName, true), niCase));
    }
    public static void IsNull(string stacktrace, object type, string methodName, string variableName, object variable = null)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNull(FullNameOfExecutedCode(type, methodName, true), variableName, variable));
    }

    /// <summary>
    /// true if everything is OK
    /// false if some error occured
    /// In console app is needed put in into try-catch error due to there is no globally handler of errors
    /// </summary>
    /// <param name="exception"></param>
    public static bool ThrowIsNotNull(string stacktrace, string exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            if (Exc.aspnet)
            {
                exception = exception.Replace("Violation of PRIMARY KEY constraint", ShortenedExceptions.ViolationOfPK);

                //if (HttpRuntime.AppDomainAppId != null)
                //{
                //Debugger.Break();
                // Will be written in globalasax error
                writeServerError(stacktrace, exception);
                if (reallyThrow)
                {
                    throw new Exception(exception);
                }
            }
            else
            {
#if MB
                TranslateDictionary.ShowMb("Throw exc");
#endif

                if (reallyThrow)
                {
                    throw new Exception(exception);
                }
            }
        }
        return true;
    }

    public static void Custom(string stacktrace, object type, string methodName, string message, bool reallyThrow = true)
    {
        ThrowIsNotNull(stacktrace, Exceptions.Custom(FullNameOfExecutedCode(type, methodName, true), message), reallyThrow);
    }

    /// <summary>
    /// First can be Method base, then A2 can be anything
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    public static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowExceptions = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowExceptions)
            {
                depth++;
            }
            methodName = Exc.CallingMethod(depth);
        }
        string typeFullName = string.Empty;
        if (type is Type)
        {
            var type2 = ((Type)type);
            typeFullName = type2.FullName;
        }
        else if (type is MethodBase)
        {
            MethodBase method = (MethodBase)type;
            typeFullName = method.ReflectedType.FullName;
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString();
        }
        else
        {
            Type t = type.GetType();
            typeFullName = t.FullName;
        }
        return string.Concat(typeFullName, dot, methodName);
    }
}
