using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.CodeGenerator
{
    public class GeneratorCpp : GeneratorCodeAbstract
    {
        public void MapStringString(int tabCount, string mapName, Dictionary<string, string> nameCommentEnums)
        {
            string cn = "map<string, string>";
            NewVariable(tabCount, AccessModifiers.Private, cn, mapName, true);
            foreach (var item in nameCommentEnums)
            {
                AppendLine(tabCount, mapName + ".insert({\"" + item.Key + "\", \"" + item.Value + "\"});");
            }
        }

        private void NewVariable(int tabCount, AccessModifiers _public, string cn, string name, bool createInstance)
        {
            AddTab2(tabCount, "");
            sb.AddItem(cn);
            sb.AppendLine(name + AllStrings.sc);
        }

        public void MapNonStringNonString(int tabCount, string mapName, string keyType, string valueType, Dictionary<string, string> nameCommentEnums)
        {
            string cn = "map<" + keyType + ", " + valueType + AllStrings.gt;
            NewVariable(tabCount, AccessModifiers.Private, cn, mapName, true);
            foreach (var item in nameCommentEnums)
            {
                AppendLine(tabCount, mapName + "." + "insert(" + "{" + item.Key + ", " + item.Value + "});");
            }
        }

        public void VectorCustom(int tabsCount, string vectorName, string customType, Dictionary<string, string> dict)
        {
            string cn = "vector<" + customType + AllStrings.gt;
            NewVariable(tabsCount, AccessModifiers.Private, cn, vectorName, true);
            Append(tabsCount, vectorName + "=");
            Append(0, AllStrings.cbl);
            foreach (var item in dict)
            {
                Append(0, "{\"" + item.Key + "\", \"" + item.Value + AllStrings.cbr + AllStrings.comma);
            }
            AppendLine(0, "};");
        }

        public void Array(int tabsCount, string arrayName, string customType, Dictionary<string, string> dict)
        {
            AddTab2(tabsCount, "");
            sb.AddItem(customType);
            Append(tabsCount, arrayName + AllStrings.lsf + dict.Count + "]=");
            Append(0, AllStrings.cbl);
            foreach (var item in dict)
            {
                string d = (item.Key == dict.Last().Key ? "" : AllStrings.comma);
                Append(0, "{\"" + item.Key + "\", \"" + item.Value + AllStrings.cbr + d);
            }
            AppendLine(0, "};");
        }
    }
}
