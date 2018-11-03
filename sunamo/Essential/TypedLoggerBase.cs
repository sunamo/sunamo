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
        public void Information(string text, params string[] p)
        {
            typedWriteLineDelegate.Invoke(TypeOfMessage.Information, text, p);
        }
        #endregion
    }
}
