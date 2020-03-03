using ConfigurableWindow.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IConfigurableWindow
{
    /// <summary>
    /// Derived classes must return the object which exposes 
    /// persisted window settings. This method is only invoked 
    /// once per Window, during construction.
    /// </summary>
    IConfigurableWindowSettings CreateSettings();
    ApplicationDataContainer data { get; set; }
    ConfigurableWindowWrapper configurableWindowWrapper { get; set; }
}