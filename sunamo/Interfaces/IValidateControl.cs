using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IValidateControl
{
     bool Validate(object tb, object control, ref ValidateData d);
     bool Validate(object tbFolder, ref ValidateData d);
    bool Validated { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    object GetContent();
}