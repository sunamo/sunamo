using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class CompareCollectionsResults : List<CompareCollectionsResult<string>>
    {
        public static string TextOutput(List<string> onlyInFirst, List<string> onlyInSecond, List<string> both = null)
        {
            return TextOutput(new CompareCollectionsResult<string>() { Both = both, OnlyInFirst = onlyInFirst, OnlyInSecond = onlyInSecond });
        }

        public static string TextOutput(CompareCollectionsResult<string> result)
        {
            if (result != null)
            {
                TextOutputGenerator textOutputGenerator = new TextOutputGenerator();

                textOutputGenerator.Header(sess.i18n(XlfKeys.Managed) + ":");

                foreach (var item in result.OnlyInFirst)
                {
                    textOutputGenerator.sb.AppendLine(item.ToString());
                }

                textOutputGenerator.Header(sess.i18n(XlfKeys.Restored) + ":");

                foreach (var item in result.OnlyInSecond)
                {
                    textOutputGenerator.sb.AppendLine(item.ToString());
                }

                if (result.Both != null)
                {
                    textOutputGenerator.Header(sess.i18n(XlfKeys.Founded) + ":");

                    foreach (var item in result.Both)
                    {
                        textOutputGenerator.sb.AppendLine(item.ToString());
                    }
                }

                return textOutputGenerator.ToString();
            }

            return string.Empty;
        }
    }
}