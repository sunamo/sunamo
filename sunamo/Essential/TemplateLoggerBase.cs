using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    public class TemplateLoggerBase
    {
        VoidTypeOfMessageStringParamsObject writeLineDelegate;

        public TemplateLoggerBase(VoidTypeOfMessageStringParamsObject writeLineDelegate)
        {
            this.writeLineDelegate = writeLineDelegate;
        }

        public void CopiedToClipboard(string what)
        {
            writeLineDelegate.Invoke(TypeOfMessage.Success, what + " was successfully copied to clipboard.");
        }
    }
}
