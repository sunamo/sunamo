using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IValidateControl
{
     bool Validate(object tb, object control, ValidateData d = null);
     bool Validate(object tbFolder, ValidateData d = null);
    bool Validated { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    object GetContent();
}