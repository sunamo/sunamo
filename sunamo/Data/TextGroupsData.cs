using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class TextGroupsData
    {
    public List<string> entries = new List<string>();
        public List<string> categories = new List<string>();
        public Dictionary<int, List<string>> sortedValues = new Dictionary<int, List<string>>();

    public static Dictionary<string, List<string>> SortedValuesWithKeyString(TextGroupsData d)
    {
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

        foreach (var item in d.sortedValues)
        {
            result.Add(d.categories[item.Key], item.Value);
            
        }

        return DictionaryHelper.GetDictionaryFromIEnumerable<string, List<string>>( result.Reverse());

        //return result;
    }
    }