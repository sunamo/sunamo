using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using sunamo.ObjectsCommon;
using sunamo;

namespace apps.XmlStorage
{
    public class XmlConnection
    {

        OuterObjectMapping tMap = null;

        public XmlConnection(OuterObjectMapping tMap)
        {
            this.tMap = tMap;
        }

        public async void CreateTableAsync<T>(IXmlCollectionEnumerable<T> emptyCol) where T : new()
        {
            await XmlLayer.SaveDbFile(emptyCol);
        }

        /// <summary>
        /// Před použitím této metody si musíš najít správný prvek pomocí LINQ a tento prvek následně předat do A1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flat"></param>
        /// <returns></returns>
        public async void UpdateAsync<T>(T flat,  IXmlCollectionEnumerable<T> elements) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo pi2 = type.GetRuntimeProperty(tMap.primaryKey.Name);
            string d = pi2.GetValue(flat).ToString();
            string hledaneID = tMap.primaryKey.GetValue(flat).ToString();

            foreach (var item in elements)
            {
                string o = tMap.primaryKey.GetValue(item).ToString();
                if (hledaneID == o)
                {
                    foreach (var pi in tMap.propertyInfos)
                    {
                        //PropertyInfo pi = type.GetRuntimeProperty(tMap.primaryKey.Name);
                        pi.SetValue(item, pi.GetValue(flat));
                    }
                    break;
                }
                
            }

            await XmlLayer.SaveDbFile(elements);

            //type.Pro
        }

        public  T GetAsync<T>(object pk, IXmlCollectionEnumerable<T> elements) where T : new()
        {
            string p = pk.ToString();
            foreach (T item in elements)
            {
                string o = tMap.primaryKey.GetValue(item).ToString();
                if (p == o)
                {
                    return item;
                }
            }
            return default(T);
        }

        public async Task DropTableAsync<T>(IXmlCollectionEnumerable<T> emptyElements)
        {
            await XmlLayer.SaveDbFile(emptyElements);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_flats"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public async Task InsertAllAsync<T>(IEnumerable<T> _flats, IXmlCollectionEnumerable<T> elements)
        {
            // Nejdříve zjistím které ID mám v A2
            List<string> ids = new List<string>();
            foreach (T item in elements)
            {
                ids.Add(tMap.primaryKey.GetValue(item).ToString());
            }
            // Poté přidám jen ty elementy z A1 které v A2 nejsou
            foreach (var item in _flats)
            {
                string id = tMap.primaryKey.GetValue(item).ToString();
                if (!ids.Contains(id))
                {
                    elements.Add(item);
                }
            }

            await XmlLayer.SaveDbFile(elements);
        }

        public async Task<List<T>> QueryAsync<T>(string p)
        {
            throw new NotImplementedException();
        }
    }
}
