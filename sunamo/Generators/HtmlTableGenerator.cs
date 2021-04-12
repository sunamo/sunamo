using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class HtmlTableGenerator : HtmlGeneratorExtended
    {
        static Type type = typeof(HtmlTableGenerator);
        //public HtmlGeneratorExtended g = new HtmlGeneratorExtended();

        public void StartTable()
        {
            WriteTag(HtmlTags.table);
        }

        public void EndTable()
        {
            TerminateTag(HtmlTags.table);
        }


        public void EndTr()
        {
            TerminateTag(HtmlTags.tr);
        }

        public void WriteRow(string additionalQuestionCssClass, List<string> possibleAnswersAll)
        {
            WriteTagWithAttr(HtmlTags.tr, HtmlAttrs.c, additionalQuestionCssClass, false);
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
            TerminateTag(HtmlTags.td);
        }

        public void StartTr(string mainQuestionsCssClass)
        {
            WriteTag(HtmlTags.tr);
        }

        public void StartTd(object p)
        {
            WriteTag(HtmlTags.td);
        }
    }
}