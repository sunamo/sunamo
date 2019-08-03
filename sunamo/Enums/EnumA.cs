using System;
using System.Collections.Generic;
using System.Text;

/*
 * Can use hexadecimal or decimal - its not important
 * 
 */
/*
 * Flags allow an enum value to contain many values. An enum type with the [Flags] attribute can have multiple constant values assigned to it. And it is still possible to test for these values in switches and if-statements.
 */
[Flags]
public enum EnumA
{
    None = 0x0,
    All = 0x1,
    a = 0x2,
    b = 0x4,
    c = 0x8
}