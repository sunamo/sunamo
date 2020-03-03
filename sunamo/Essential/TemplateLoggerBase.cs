using sunamo.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    public class TemplateLoggerBase
    {
        private VoidTypeOfMessageStringParamsObject _writeLineDelegate;

        public TemplateLoggerBase(VoidTypeOfMessageStringParamsObject writeLineDelegate)
        {
            _writeLineDelegate = writeLineDelegate;
        }

        public void EndRunTime()
        {
            WriteLine(TypeOfMessage.Ordinal, Messages.AppWillBeTerminated);
        }

        #region Success
        public void CopiedToClipboard(string what)
        {
            WriteLine(TypeOfMessage.Success, what + " " + "was successfully copied to clipboard" + ".");
        }


        #endregion

        #region Error
        public void CouldNotBeParsed(string entity, string text)
        {
            WriteLine(TypeOfMessage.Error, entity + " " + "with value " + text + " could not be parsed");
        }

        public void SomeErrorsOccuredSeeLog()
        {
            WriteLine(TypeOfMessage.Error, "Some errors occured, see log");
        }

        public void FolderDontExists(string folder)
        {
            WriteLine(TypeOfMessage.Error, "Folder" + " " + folder + " " + "doesn't exists" + ".");
        }

        public void FileDontExists(string selectedFile)
        {
            WriteLine(TypeOfMessage.Error, "File" + " " + selectedFile + " " + "doesn't exists" + ".");
        }

        private void WriteLine(TypeOfMessage error, string v)
        {
            _writeLineDelegate(error, v);
        }
        #endregion

        #region Information
        public void InsertAsIndexesZeroBased()
        {
            WriteLine(TypeOfMessage.Information, "Insert as indexes (zero based)");
        }

        public void UnfortunatelyBadFormatPleaseTryAgain()
        {
            WriteLine(TypeOfMessage.Information, "Unfortunately bad format. Please try again" + ".");
        }

        public void OperationWasStopped()
        {
            WriteLine(TypeOfMessage.Information, "Operation was stopped");
        }

        public void NoData()
        {
            WriteLine(TypeOfMessage.Information, "Please enter right input data");
        }



        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        /// <param name="fn"></param>
        public void SuccessfullyResized(string fn)
        {
            WriteLine(TypeOfMessage.Information, "Successfully resized to" + " " + fn);
        }

        private string FullNameOfExecutedCode(object type, string methodName)
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
        public bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, object[] args)
        {
            if (args.Count() % 2 == 1)
            {
                WriteLine(TypeOfMessage.Error, Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(type, methodName), nameOfCollection));
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
        public bool AnyElementIsNullOrEmpty(Type type, string methodName, string nameOfCollection, IEnumerable args)
        {
            List<int> nulled = CA.IndexesWithNullOrEmpty(args);

            if (nulled.Count > 0)
            {
                WriteLine(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(type, methodName), nameOfCollection, nulled));
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
        public bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, object[] args)
        {
            List<int> nulled = CA.IndexesWithNull(args);

            if (nulled.Count > 0)
            {
                WriteLine(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(type, methodName), nameOfCollection, nulled));
                return true;
            }
            return false;
        }

        public void HaveUnallowedValue(string controlNameOrText)
        {
            controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
            WriteLine(TypeOfMessage.Appeal, controlNameOrText + " " + "have unallowed value");
        }

        public void MustHaveValue(string controlNameOrText)
        {
            controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
            WriteLine(TypeOfMessage.Appeal, controlNameOrText + " " + "must have value");
        }
        #endregion
    }
}