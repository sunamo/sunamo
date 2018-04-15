
﻿using System;
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

        protected virtual async Task<List<LogMessageAbstract<Color, StorageClass>>> ReadMessagesFromFile(StorageClass fileStream)
        {
            return null;
        }

        protected virtual async void Initialize(string soubor, bool invariant, TextBlock tssl, Langs l)
        {
            
        }

        protected abstract LogMessageAbstract<Color, StorageClass> CreateMessage();

        public abstract Task<LogMessageAbstract<Color, StorageClass>> Add(TypeOfMessage st, string status);
    }
}
