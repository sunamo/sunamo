﻿using System;
using System.Collections.Generic;
using System.Text;

public class RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs
{
    internal static RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs Default = new RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs();
    /// <summary>
    /// default var
    /// </summary>
    public bool removeWholeTransUnit = true;
    public bool save = true;
}