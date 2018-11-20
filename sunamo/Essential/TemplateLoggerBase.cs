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

        /// <summary>
        /// Return true if number of counts is odd
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="nameOfCollection"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, object[] args)
        {
            if (args.Count() % 2 == 1)
            {
                writeLineDelegate.Invoke(TypeOfMessage.Error, Exceptions.NotEvenNumberOfElements( FullNameOfExecutedCode(type, methodName), nameOfCollection));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Return true if any will be null or empty
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="nameOfCollection"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal bool AnyElementIsNullOrEmpty(Type type, string methodName, string nameOfCollection, IEnumerable args)
        {
            List<int> nulled = CA.IndexesWithNullOrEmpty(args);

            if (nulled.Count > 0)
            {
                writeLineDelegate.Invoke(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(type, methodName), nameOfCollection, nulled));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return true if any will be null
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="nameOfCollection"></param>
        /// <param name="args"></param>
        internal bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, object[] args)
        {
            List<int> nulled = CA.IndexesWithNull(args);

            if (nulled.Count > 0)
            {
                writeLineDelegate.Invoke(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(type, methodName), nameOfCollection, nulled));
                return true;
            }
            return false;
        }

        public void MustHaveValue(string controlNameOrText)
        {
            controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
            writeLineDelegate.Invoke(TypeOfMessage.Appeal, controlNameOrText + " must have value");
        }
        #endregion
    }
}
