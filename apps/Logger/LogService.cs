using sunamo;
using sunamo.Essential;
using sunamo.LoggerAbstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace apps
{

    public class LogService : LogServiceAbstract<Color, StorageFile>
    {
        

        public override Color GetBackgroundBrushOfTypeOfMessage(TypeOfMessage st)
        {
            switch (st)
            {
                case TypeOfMessage.Error:
                    return Colors.LightCoral;
                case TypeOfMessage.Warning:
                    return Colors.LightYellow;
                case TypeOfMessage.Information:
                    return Colors.White;
                case TypeOfMessage.Ordinal:
                    return Colors.White;
                case TypeOfMessage.Appeal:
                    return Colors.LightGray;
                case TypeOfMessage.Success:
                    return Colors.LightGreen;
                default:
                    return Colors.White;
            }
        }

        public override Color GetForegroundBrushOfTypeOfMessage(TypeOfMessage st)
        {
            switch (st)
            {
                case TypeOfMessage.Error:
                    return Colors.DarkRed;
                case TypeOfMessage.Warning:
                    return Colors.DarkOrange;
                case TypeOfMessage.Information:
                    return Colors.Black;
                case TypeOfMessage.Ordinal:
                    return Colors.Black;
                case TypeOfMessage.Appeal:
                    return Colors.Gray;
                case TypeOfMessage.Success:
                    return Colors.LightGreen;
                default:
                    return Colors.White;
            }
        }

        public LogService()
        {

        }


        TextBlock tssl = null;

        LogMessageAbstract<Color, StorageFile> prectenyRadek;

        bool HasRowContent(string s)
        {
            // Must set prectenyRadek here
            prectenyRadek = null;

            return true;
        }

        async Task< LogMessageAbstract<Color, StorageFile>> Parse(LogMessageAbstract<Color, StorageFile> logMessageAbstract)
        {
            return null;
        }

        protected async override Task<List<LogMessageAbstract<Color, StorageFile>>> ReadMessagesFromFile(StorageFile fileStream)
        {
            Stream s = await fileStream.OpenStreamForReadAsync();
            StreamReader sr = new StreamReader(s);
            List<LogMessageAbstract<Color, StorageFile>> vr = new List<LogMessageAbstract<Color, StorageFile>>();
            // Zde by se prazdne radky nemeli vyskytovat, ale v jinych programech ano!
            while (HasRowContent(sr.ReadLine()))
            {
                LogMessageAbstract<Color, StorageFile> zpravaLogu = CreateMessage();
                zpravaLogu = await Parse(prectenyRadek);
                if (zpravaLogu != null)
                {
                    vr.Add(zpravaLogu);
                }
                
            }

            sr.Dispose();
            return vr;
        }

        List<LogMessageAbstract<Color, StorageFile>> messagesOlder = new List<LogMessageAbstract<Color, StorageFile>>();
        List<LogMessageAbstract<Color, StorageFile>> messagesActualSession2 = new List<LogMessageAbstract<Color, StorageFile>>();
        List<LogMessageAbstract<Color, StorageFile>> templates = new List<LogMessageAbstract<Color, StorageFile>>();


        protected override async void Initialize(string soubor, bool invariant, TextBlock tssl, Langs l)
        {
            InitializeAbstract(invariant, l);
            this.tssl = tssl;


        }

        private void InitializeAbstract(bool invariant, Langs l)
        {
            throw new NotImplementedException();
        }

        protected override LogMessageAbstract<Color, StorageFile> CreateMessage()
        {
            return new LogMessage();
        }

        public override Task<LogMessageAbstract<Color, StorageFile>> Add(TypeOfMessage st, string status)
        {
            throw new NotImplementedException();
        }
    }
}
