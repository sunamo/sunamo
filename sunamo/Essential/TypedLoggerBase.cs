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
        static Type type = typeof(TypedLoggerBase);
        private Action<TypeOfMessage, string, object[]> typedWriteLineDelegate;

        public TypedLoggerBase(Action<TypeOfMessage,string, object[]> typedWriteLineDelegate)
        {
            this.typedWriteLineDelegate = typedWriteLineDelegate;
        }
        #region 
        public void Success(string text, params object[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Success, text, p);
        }

        public void Error(string text, params string[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Error, text, p);
        }
        public void Warning(string text, params string[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Warning, text, p);
        }

        public void Appeal(string text, params string[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Appeal, text, p);
        }

        public void Ordinal(string text, params string[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Ordinal, text, p);
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
                    ThrowExceptions.NotImplementedCase(type, "WriteLine");
                    break;
            }
        }

        public void Information(string text, params string[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Information, text, p);
        }
        #endregion
    }
}
