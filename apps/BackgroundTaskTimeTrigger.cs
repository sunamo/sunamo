using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace apps
{
    public class BackgroundTaskTimeTrigger 
    {
        public string progress = "";
        public bool registered = false;
        public string name = "";
        public string entryPoint = "";

        public BackgroundTaskTimeTrigger(string name, string entryPoint)
        {
            this.name = name;
            this.entryPoint = entryPoint;
        }

        public  BackgroundTaskRegistration RegisterBackgroundTask()
        {


            var builder = new BackgroundTaskBuilder();

            builder.Name = name;
            builder.TaskEntryPoint = entryPoint;
            builder.SetTrigger(null);
            BackgroundTaskRegistration task = builder.Register();

            registered = true;

            return task;
        }
    }
}
