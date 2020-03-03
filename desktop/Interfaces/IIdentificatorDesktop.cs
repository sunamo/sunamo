using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public interface IIdentificatorDesktop<T> : IIdentificator<T>
{
    Visibility Visibility { get; set; }
}