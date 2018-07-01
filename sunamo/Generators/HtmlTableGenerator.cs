using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class HtmlTableGenerator : HtmlGenerator
    {
        public HtmlGeneratorExtended extended = new HtmlGeneratorExtended();

        public void StartTable(object p)
        {
            throw new NotImplementedException();
        }

        public void EndTr()
        {
            throw new NotImplementedException();
        }

        public void WriteRow(string additionalQuestionCssClass, List<string> possibleAnswersAll)
        {
            WriteTagWithAttr(HtmlTags.tr, HtmlAttrs.@class, additionalQuestionCssClass, true);
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
            throw new NotImplementedException();
        }

        public void StartTr(string mainQuestionsCssClass)
        {
            throw new NotImplementedException();
        }

        public void StartTd(object p)
        {
            throw new NotImplementedException();
        }
    }
}
