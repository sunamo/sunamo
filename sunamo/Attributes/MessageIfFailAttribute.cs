using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MessageIfFailAttribute : Attribute
{
    public string message = null;

    public MessageIfFailAttribute(string message)
    {
        this.message = this.message;
    }
    
}