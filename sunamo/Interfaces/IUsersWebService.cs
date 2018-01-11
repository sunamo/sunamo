using System;
using System.Collections.Generic;
using System.Linq;

public interface IUsersWebService
{
    string GetOddIndexesOfHash(string login);
    bool IsPairLoginAndPw(string login, string pw, out int fce, out string dph);
    string NameOfUserWithID(int ID);
}
