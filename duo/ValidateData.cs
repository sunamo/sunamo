﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Must be in shared because desktop reference PathEditor and therefore this class cant be in desktop 
/// </summary>
public class ValidateData
{
    public readonly static ValidateData Default = new ValidateData();
    public bool trim = true;
    /// <summary>
    /// Strings which are not allowed
    /// </summary>
    public List<string> excludedStrings = new List<string>();
    public bool allowEmpty = false;
    public Func<string, bool> validateMethod;
    public string messageWhenValidateMethodFails;
    public string messageToReallyShow;
}