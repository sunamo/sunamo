using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LoginResponse
{
    public LoginResponseType type = LoginResponseType.Alert;
    public string value = null;

    public LoginResponse(LoginResponseType type, string value)
    {
        this.type = type;
        this.value = value;
    }
}

