using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetFilesEveryFolderBgWorker
{
    BackgroundWorker bgWorker = null;
    GetFilesEveryFolder e = null;
    public event RunWorkerCompletedEventHandler RunWorkerCompleted;

    public GetFilesEveryFolderBgWorker(GetFilesEveryFolder e)
    {
        bgWorker = new BackgroundWorker();
        bgWorker.DoWork += BgWorker_DoWork;
        bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
    }

    private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        RunWorkerCompleted(sender, e);
    }

    public List<string> result = null;

    private void BgWorker_DoWork(object sender, DoWorkEventArgs ea)
    {
        //result = Task.Run<List<string>>(async () => await FS.GetFilesEveryFolder(e.path, e.masc, e.searchOption, new GetFilesEveryFolderArgs { _trimA1 = e._trimA1 })).Result;
        
        // Automatically after process method call Completed
    }
}