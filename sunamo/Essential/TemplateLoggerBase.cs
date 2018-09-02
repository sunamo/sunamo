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

        public void UnfortunatelyBadFormatPleaseTryAgain()
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Unfortunately bad format. Please try again.");
        }

        public void CopiedToClipboard(string what)
        {
            writeLineDelegate.Invoke(TypeOfMessage.Success, what + " was successfully copied to clipboard.");
        }

        public void FolderDontExists(string folder)
        {
            writeLineDelegate.Invoke(TypeOfMessage.Error, "Folder " + folder + " doesn't exists.");
        }

        public void OperationWasStopped()
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Operation was stopped");
        }

        public void NoData()
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Please enter right input data");
        }
    }
}
