using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ILoginManager
{
    Func<string, string> DoWebRequest { get; set; }
    Func<string, ExternalLoginResult> DeserializeJson { get; set; }
    bool PairLoginAndPassword(string messSuccessfullyLoginedTo, Func<string, string> EncryptPasswordToBase64, string login, string password, string hostWithSlash,  bool showOnUserRequest = false);
}