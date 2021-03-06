using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    /// <summary>
    /// In difference with LoggerBase take type of message as enum
    /// </summary>
    public class TypedLoggerBase
    {
        private static Type type = typeof(TypedLoggerBase);
        private Action<TypeOfMessage, string, object[]> _typedWriteLineDelegate;

        public TypedLoggerBase(Action<TypeOfMessage, string, object[]> typedWriteLineDelegate)
        {
            _typedWriteLineDelegate = typedWriteLineDelegate;
        }

#if !DEBUG2
public TypedLoggerBase ()
{

}
#endif

        /// <summary>
        /// Only due to Old sfw apps
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="name"></param>
        /// <param name="v2"></param>
        public void WriteLineFormat(string v1, params string[] name)
        {
            Ordinal(v1, name);
        }

#region 
        public void Success(string text, params object[] p)
        {
            _typedWriteLineDelegate.Invoke(TypeOfMessage.Success, text, p);
        }

        public void Error(string text, params string[] p)
        {
            _typedWriteLineDelegate.Invoke(TypeOfMessage.Error, text, p);
        }
        public void Warning(string text, params string[] p)
        {
            _typedWriteLineDelegate.Invoke(TypeOfMessage.Warning, text, p);
        }

        public void Appeal(string text, params string[] p)
        {
            _typedWriteLineDelegate.Invoke(TypeOfMessage.Appeal, text, p);
        }

        public void Ordinal(string text, params string[] p)
        {
            _typedWriteLineDelegate.Invoke(TypeOfMessage.Ordinal, text, p);
        }

        public void WriteLine(TypeOfMessage t, string m)
        {
            switch (t)
            {
                case TypeOfMessage.Error:
                    Error(m);
                    break;
                case TypeOfMessage.Warning:
                    Warning(m);
                    break;
                case TypeOfMessage.Information:
                    Information(m);
                    break;
                case TypeOfMessage.Ordinal:
                    Ordinal(m);
                    break;
                case TypeOfMessage.Appeal:
                    Appeal(m);
                    break;
                case TypeOfMessage.Success:
                    Success(m);
                    break;
                default:
                    ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, "WriteLine", t);
                    break;
            }
        }

        public void Information(string text, params string[] p)
        {
            
            _typedWriteLineDelegate.Invoke(TypeOfMessage.Information, text, p);
        }
#endregion
    }
}