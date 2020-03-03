
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.LoggerAbstract
{
    public abstract class LogServiceAbstract<Color, StorageClass, TextBlock>
    {
        public abstract Color GetBackgroundBrushOfTypeOfMessage(TypeOfMessage st);
        public abstract Color GetForegroundBrushOfTypeOfMessage(TypeOfMessage st);

        protected virtual List<LogMessageAbstract<Color, StorageClass>> ReadMessagesFromFile(StorageClass fileStream)
        {
            return null;
        }

        public virtual void Initialize(string soubor, bool invariant, TextBlock tssl, Langs l)
        {
        }

        public abstract void SaveToFile();

        protected abstract LogMessageAbstract<Color, StorageClass> CreateMessage();

        public abstract LogMessageAbstract<Color, StorageClass> Add(TypeOfMessage st, string status);
    }
}