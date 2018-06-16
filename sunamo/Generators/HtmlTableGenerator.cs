using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class HtmlTableGenerator : HtmlGeneratorEnum
    {
        public HtmlTableGenerator()
        {
            
        }

        /// <summary>
        /// A1 can be empty/null
        /// </summary>
        /// <param name="cssClass"></param>
        public void StartTable(string cssClass)
        {
            if (string.IsNullOrEmpty(cssClass))
            {
                WriteTag(HtmlTag.table);
            }
            else
            {
                WriteTagWithAttr(HtmlTag.table, HtmlAttr.@class, cssClass);
            }
        }

        public void WriteRow(string cssClass, object enumerableOrCount)
        {
            if (string.IsNullOrEmpty(cssClass))
            {
                WriteTag(HtmlTag.tr);
            }
            else
            {
                WriteTagWithAttr(HtmlTag.tr, HtmlAttr.@class, cssClass);
            }


            IEnumerable<string> list = null;
            if (enumerableOrCount is IEnumerable<string>)
            {
                list = enumerableOrCount as IEnumerable<string>;
                
            }
            else if (enumerableOrCount is int)
            {
                list = CA.CreateListWithValue<string>(string.Empty, (int)enumerableOrCount);
            }

            foreach (var item in list)
            {
                Td(null, item);
            }

            TerminateTag(HtmlTag.tr);
        }

        /// <summary>
        /// A1 can be empty/null
        /// </summary>
        /// <param name="cssClass"></param>
        /// <param name="content"></param>
        public void Td(string cssClass, string content)
        {
            if (string.IsNullOrEmpty(cssClass))
            {
                WriteTag(HtmlTag.td);
            }
            else
            {
                WriteTagWithAttr(HtmlTag.td, HtmlAttr.@class, cssClass);
            }

            WriteRaw(content);
            TerminateTag(HtmlTag.td);
        }

        public void StartTr(string cssClass)
        {
            if (string.IsNullOrEmpty(cssClass))
            {
                WriteTag(HtmlTag.tr);
            }
            else
            {
                WriteTagWithAttr(HtmlTag.tr, HtmlAttr.@class, cssClass);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void StartTd(string cssClass)
        {
            if (string.IsNullOrEmpty(cssClass))
            {
                WriteTag(HtmlTag.td);
            }
            else
            {
                WriteTagWithAttr(HtmlTag.td, HtmlAttr.@class, cssClass);
            }
        }

        public void EndTd()
        {
            TerminateTag(HtmlTag.td);
        }

        public void EndTr()
        {
            TerminateTag(HtmlTag.tr);
        }
    }
}
