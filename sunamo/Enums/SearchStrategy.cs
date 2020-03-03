using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// FixedSpace - Contains
/// AnySpaces - 
/// ExactlyName - Is exactly the same
/// </summary>
public enum SearchStrategy
{
    /// <summary>
    /// Contains
    /// </summary>
    FixedSpace,
    /// <summary>
    /// split input by spaces and A1 must contains all parts
    /// </summary>
    AnySpaces,
    /// <summary>
    /// Is exactly the same
    /// </summary>
    ExactlyName
}