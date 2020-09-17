using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AsyncLoadingBase<T, ProgressBar>
{
    public Action<T> statusAfterLoad;
    public ProgressBar pb;
    public long processedCount = 0;
}