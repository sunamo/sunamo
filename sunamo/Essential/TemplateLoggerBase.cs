using System;
using System.Collections;
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

        #region Success
        public void CopiedToClipboard(string what)
        {
            writeLineDelegate.Invoke(TypeOfMessage.Success, what + " was successfully copied to clipboard.");
        }
        #endregion

        #region Error
        public void FolderDontExists(string folder)
        {
            writeLineDelegate.Invoke(TypeOfMessage.Error, "Folder " + folder + " doesn't exists.");
        }
        #endregion

        #region Information
        public void UnfortunatelyBadFormatPleaseTryAgain()
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Unfortunately bad format. Please try again.");
        }

        public void OperationWasStopped()
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Operation was stopped");
        }

        public void NoData()
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Please enter right input data");
        }

        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        /// <param name="fn"></param>
        public void SuccessfullyResized(string fn)
        {
            writeLineDelegate.Invoke(TypeOfMessage.Information, "Successfully resized to " + fn);
        }

        string FullNameOfExecutedCode(object type, string methodName)
        {
            return ThrowExceptions.FullNameOfExecutedCode(type, methodName);
        }

        internal void NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, object[] args)
        {
            if (args.Count() % 2 == 0)
            {
                writeLineDelegate.Invoke(TypeOfMessage.Error, Exceptions.NotEvenNumberOfElements( FullNameOfExecutedCode(type, methodName), nameOfCollection));
            }
        }

        internal void AnyElementIsNullOrEmpty(Type type, string methodName, string nameOfCollection, IEnumerable args)
        {
            List<int> nulled = CA.IndexesWithNull(args);

            if (nulled.Count > 0)
            {
                writeLineDelegate.Invoke(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(type, methodName), nameOfCollection, nulled));
            }
        }

        internal void AnyElementIsNull(Type type, string methodName, string nameOfCollection, object[] args)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
