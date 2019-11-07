using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class TaskExtensions
{
    public static ConfiguredTaskAwaitable Conf(this Task t )

    {
        return t.ConfigureAwait(true);
    }
}

