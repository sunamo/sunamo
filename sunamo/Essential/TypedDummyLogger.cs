using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Text;


public class TypedDummyLogger : TypedLoggerBase
{
    public static TypedDummyLogger Instance = new TypedDummyLogger();

    private TypedDummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {

    }
}