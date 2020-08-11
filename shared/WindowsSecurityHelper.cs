using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

public class WindowsSecurityHelper
    {
    
public static bool IsUserAdministrator()
{
    bool isAdmin;
    try
    {
        WindowsIdentity user = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(user);
        isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
    catch (UnauthorizedAccessException)
    {
        isAdmin = false;
    }
    catch (Exception ex)
    {
        isAdmin = false;
    }
    return isAdmin;
}
}