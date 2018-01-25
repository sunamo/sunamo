using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.CodeGenerator
{
    public class GeneratorCpp : GeneratorCodeAbstract
    {
        public void MapStringString(int pocetTabu, string mapName, Dictionary<string, string> nameCommentEnums)
        {
            string cn = "map<string, string>";
            NewVariable(pocetTabu, AccessModifiers.Private, cn, mapName, true);
            foreach (var item in nameCommentEnums)
            {
                AppendLine(pocetTabu, mapName + ".insert({\"" + item.Key + "\", \"" + item.Value + "\"});");
            }
        }

        private void NewVariable(int pocetTabu, AccessModifiers _public, string cn, string name, bool createInstance)
        {
            AddTab2(pocetTabu, "");
            sb.AddItem(cn);
            sb.AppendLine(name + ";");
        }

        public void MapNonStringNonString(int pocetTabu, string mapName, string keyType, string valueType, Dictionary<string, string> nameCommentEnums)
        {
            string cn = "map<" + keyType + ", " + valueType + ">";
            NewVariable(pocetTabu, AccessModifiers.Private, cn, mapName, true);
            foreach (var item in nameCommentEnums)
            {
                AppendLine(pocetTabu, mapName + ".insert({" + item.Key + ", " + item.Value + "});");
            }
        }

        public void VectorCustom(int tabsCount, string vectorName, string customType, Dictionary<string, string> dict)
        {
            string cn = "vector<" + customType + ">";
            NewVariable(tabsCount, AccessModifiers.Private, cn, vectorName, true);
            Append(tabsCount, vectorName + "=");
            Append(0, "{");
            foreach (var item in dict)
            {
                Append(0, "{\"" + item.Key + "\", \"" + item.Value + "\"}" + ",");
            }
            AppendLine(0, "};");
        }

        public void Array(int tabsCount, string arrayName, string customType, Dictionary<string, string> dict)
        {
            AddTab2(tabsCount, "");
            sb.AddItem(customType);
            Append(tabsCount, arrayName + "[" + dict.Count + "]=");
            Append(0, "{");
            foreach (var item in dict)
            {
                string d = (item.Key == dict.Last().Key ? "" : ",");
                Append(0, "{\"" + item.Key + "\", \"" + item.Value + "\"}" + d);
            }
            AppendLine(0, "};");
        }
    }
}
