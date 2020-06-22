using System;
using System.Collections.Generic;
using System.Text;


public enum DumpProvider
{
    /// <summary>
    /// When use JsonParser return empty.
    /// </summary>
    Json,
    /// <summary>
    /// 
    /// </summary>
    Reflection,
    /// <summary>
    /// Throw NullReferenceException, DONT USE
    /// </summary>
    Yaml,
    /// <summary>
    /// NI. COuld reference another dll
    /// </summary>
    ObjectDumper
}