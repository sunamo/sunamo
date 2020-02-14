using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class HtmlTableGenerator : HtmlGenerator
    {
        static Type type = typeof(HtmlTableGenerator);
        public HtmlGeneratorExtended extended = new HtmlGeneratorExtended();

        public void StartTable(object p)
        {
            ThrowExceptions.NotImplementedMethod(type, RH.CallingMethod());
        }

        public void EndTr()
        {
            ThrowExceptions.NotImplementedMethod(type, RH.CallingMethod());
        }

        public void WriteRow(string additionalQuestionCssClass, List<string> possibleAnswersAll)
        {
            WriteTagWithAttr(HtmlTags.tr, HtmlAttrs.c, additionalQuestionCssClass, true);
            foreach (var item in possibleAnswersAll)
            {
                WriteTd(item);
            }
            TerminateTag(HtmlTags.tr);
        }

        private void WriteTd(string item)
        {
            WriteTag(HtmlTags.td);
            WriteRaw(item);
            TerminateTag(HtmlTags.td);
        }

        public void WriteRow(string additionalQuestionCssClass, int count)
        {
            List<string> list = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(string.Empty);
            }
            WriteRow(additionalQuestionCssClass, list);
        }

        public void EndTd()
        {
            ThrowExceptions.NotImplementedMethod(type, RH.CallingMethod());
        }

        public void StartTr(string mainQuestionsCssClass)
        {
            ThrowExceptions.NotImplementedMethod(type, RH.CallingMethod());
        }

        public void StartTd(object p)
        {
            ThrowExceptions.NotImplementedMethod(type, RH.CallingMethod());
        }
    }
}
