using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Text;


public class DummyTemplateLogger : TemplateLoggerBase
{
    public static DummyTemplateLogger Instance = new DummyTemplateLogger();

    private DummyTemplateLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {

    }
}