using System;
using System.Collections.Generic;
using System.Text;


public class DummyLogger : LoggerBase
{
    public static DummyLogger Instance = new DummyLogger();

    private DummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {

    }


}