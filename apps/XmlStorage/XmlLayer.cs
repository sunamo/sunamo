using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using sunamo.ObjectsCommon;
using System.Globalization;
using Windows.Storage;

using System.IO;
using Windows.Storage.Streams;
using sunamo;

namespace apps.XmlStorage
{
    public class XmlLayer
    {
        static StorageFolder sf2 = null;

        public XmlLayer()
        {

        }

        static OuterObjectMapping _tMap = null;
        public static OuterObjectMapping tMap
        {
            get
            {
                return _tMap;
            }
            set
            {
                _tMap = value;
                _conn = new XmlConnection(value);
            }
        }

        public static XmlConnection _conn = null;
        public static XmlConnection conn
        {
            get
            {
                return _conn;
            }
        }

        

        public static string extOfDB;
        public static PluralConverter pluralConverter;
        public bool SaveImmediately = true;
        //public static IRandomAccessStream storageFile;
        public static StorageFile sf = null;

        /// <summary>
        /// Může se ukládat pouze když věci neukládám okamžitě, resp. nemusí se používat pouze takhle ale je to zbytečné mrhání výpočetním výkonem
        /// Bezparametrová metoda není, v opačném případě se soubor nemusí uzavírat
        /// </summary>
        public async static void CloseDbFile(IXmlCollection xml)
        {
            await SaveDbFile(xml);
        }

        public async static Task SaveDbFile(IXmlCollection content)
        {
            await TF.SaveFile(content.ToXml(), sf);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public async static Task< string> ReadDbFile()
        {
            return await TF.ReadFile(sf);
        }



        public static OuterObjectMapping GenerateMapping<T>()
        {
            OuterObjectMapping outersMapping = new OuterObjectMapping();
            var MappedType = typeof(T);

            string TableName = MappedType.Name;

            var tableAttr = (OuterObjectAttribute)System.Reflection.CustomAttributeExtensions
                .GetCustomAttribute(MappedType.GetTypeInfo(), typeof(OuterObjectAttribute), true);

            var props = from p in MappedType.GetRuntimeProperties()
                        where ((p.GetMethod != null && p.GetMethod.IsPublic) || (p.SetMethod != null && p.SetMethod.IsPublic))// || (p.GetMethod != null && p.GetMethod.IsStatic) || (p.SetMethod != null && p.SetMethod.IsStatic))
                        select p;


            foreach (var p in props)
            {
                var ignore = p.GetCustomAttributes(typeof(IgnoreAttribute), true).Count() > 0;

                if (p.CanWrite && !ignore)
                {
                    outersMapping.propertyInfos.Add(p);
                    var primaryKey = p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() > 0;
                    if (primaryKey)
                    {
                        if (outersMapping.primaryKey == null)
                        {
                            outersMapping.primaryKey = p;
                        }
                        else
                        {
                            if (AppLangHelper.currentUICulture.TwoLetterISOLanguageName == "cs")
                            {
                                throw new Exception("Program se pokouší vytvořit tabulku se 2mi primárními klíči");
                            }
                            else
                            {
                                throw new Exception("The program is attempting to create a table with two primary keys");
                            }
                        }
                    }

                }
            }
            tMap = outersMapping;
            return outersMapping;
        }

        public async static Task RenameDbFile(string d)
        {
            await sf.RenameAsync(d + extOfDB, NameCollisionOption.GenerateUniqueName);
            sf = await sf2.CreateFileAsync(d + extOfDB, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Pouze odstrní aktuální soubor, je pak na mě abych si otevřel zdejšími metodami jinou DB
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteDbFile()
        {
            await FSApps.DeleteFile(sf);
        }
    }
}
