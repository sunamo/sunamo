
﻿using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Must be LoggerAbstract because same class exists in uap
/// </summary>
namespace sunamo.LoggerAbstract
{
    public abstract class LogMessageAbstract<Color, StorageClass> : ILogMessage<Color, StorageClass>
    {
        private DateTime dateTime;
        private TypeOfMessage typeOfMessage;
        private string message;
        private Color bg;

        public DateTime Dt { get { return dateTime; } }
        public TypeOfMessage st { get { return typeOfMessage; } }
        public string Message { get { return message; } }
        public Color Bg { get { return Bg;  }  set { Bg = value; } }

        /// <summary>
        /// Must be method because call await ThisApp.cd.RunAsync (works with controls)
        /// </summary>
        /// <param name="c"></param>
        protected virtual async void SetBg(Color c)
        {

        }

        public LogMessageAbstract()
        {

        }

        /// <summary>
        /// Is here for easy cast LogMessage to generic version
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="st"></param>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public async Task< LogMessageAbstract<Color, StorageClass>> Initialize(DateTime dt, TypeOfMessage st, string message, Color color)
            {
            this.dateTime = dt;
            this.typeOfMessage = st;
            this.message = message;
            this.bg = color;
            return this;
            }
    }
}
