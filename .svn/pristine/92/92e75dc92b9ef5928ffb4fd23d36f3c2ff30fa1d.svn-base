using System;
using System.Collections.Generic;
using System.Linq;

namespace webforms
{
    /// <summary>
    /// Summary description for Registrace
    /// </summary>
    public class Registrace
    {
        public string Login = null;
        public string Heslo = null;
        public string HesloZnovu = null;
        public string Email = null;
        public DateTime DateBorn = global::MSStoredProceduresI.DateTimeMinVal;
        public string SecQ = "";
        public string SecA = "";
        public bool Sex = false;
        public byte IDState = 0;
        public short IDRegion = -1;
        public byte IDDistrict = 0;
        public int IDCity = -1;
        //public string City = null;
        public MailSettings MailSettings = MailSettings.MailOnlyFromApps;

        public Registrace()
        {
            // TODO: Complete member initialization
        }

        public HtmlSB sb = new HtmlSB();

        /// <summary>
        /// Předává se do této metody i město, které se ale uloží a zpracuje pouze když bude všechno OK v této třídě
        /// </summary>
        /// <param name="mesto"></param>
        /// <returns></returns>
        public string HtmlAreAllItemsOK(string mesto, HtmlSB sb)
        {

            if (Login.ToLower() == "all")
            {
                sb.AddItem("Login 'all' je vyhrazen pro jiné účely.");
            }
            if (Login.Contains(" "))
            {
                sb.AddItem("Přezdívka nemůže obsahovat mezery");
            }

            if (Login.Contains('@'))
            {
                sb.AddItem("Přezdívka nemůže obsahovat @(zavináč)");
            }

            if (Login.Contains('&'))
            {
                sb.AddItem("Přezdívka nemůže obsahovat &(ampersand)");
            }

            if (Login.Contains('='))
            {
                sb.AddItem("Přezdívka nemůže obsahovat =(rovnítko)");
            }
            if (Login.ToLower().Trim() == "null")
            {
                sb.AddItem("Přezdívka nemůže být null");
            }
            if (Heslo.Contains(" "))
            {
                sb.AddItem("Heslo nemůže obsahovat mezery");
            }

            sb.AddItems(BasicTest20("Login", Login), BasicTest20("Heslo", Heslo), BasicTest20("Heslo znovu", HesloZnovu));
            if (Logins.OccupateLogin("Login", Login))
            {
                sb.AddItem("Uživatel se stejným loginem již v DB existuje");
            }

            List<char> ch = new List<char>();
            ch.AddRange(AllChars.lowerChars);
            ch.AddRange(AllChars.upperChars);
            ch.AddRange(AllChars.numericChars);
            ch.Add('_');
            ch.Add('.');
            ch.Add('-');

            foreach (char item in Login)
            {
                if (!ch.Contains(item))
                {
                    sb.AddItem("Login obsahuje nepovolené znaky. Jsou povoleny velká, malá písmena, čísla, podržítko, tečka a pomlčka");
                    break;
                }
            }

            sb.AddItems(BasicTest1000("Bezpečnostní otázka", SecQ), BasicTest1000("Odpověď na bezpečnostní otázku", SecA));
            IsPasswordSecure(sb, Heslo);

            if (Heslo != HesloZnovu)
            {
                sb.AddItem("Zadaná hesla nejsou shodná");
            }

            return HtmlAreAllItemsOKCommon(true, Email, DateBorn, IDState, ref IDCity, mesto, sb);
        }

        /// <summary>
        /// Společná metoda jak pro EditProfile.aspx tak i pro Register.aspx
        /// </summary>
        /// <param name="checkMailInDB"></param>
        /// <param name="Email"></param>
        /// <param name="Telefon"></param>
        /// <param name="DateBorn"></param>
        /// <param name="IDState"></param>
        /// <param name="IDCity"></param>
        /// <param name="mesto"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static string HtmlAreAllItemsOKCommon(bool checkMailInDB, string Email, DateTime DateBorn, byte IDState, ref int IDCity, string mesto, HtmlSB sb)
        {
            // BasicTest20("Telefonní číslo", Telefon), 
            sb.AddItems(BasicTest40("Email", Email));
            if (!RegexHelper.IsEmail(Email))
            {
                sb.AddItem("Email nebyl zadán ve platném formátu");
            }
            else
            {
                if (checkMailInDB)
                {
                    if (Logins.OccupateLogin("Email", Email))
                    {
                        sb.AddItem("Uživatel se stejným mailem již v DB existuje");
                    }
                }
            }

            if (DateBorn > global::MSStoredProceduresI.DateTimeMaxVal || DateBorn < global::MSStoredProceduresI.DateTimeMinVal)
            {
                sb.AddItem("Datum narození nebylo v platném rozsahu. Zkuste zadat reálné ;-).");
            }

            string vr = sb.ToString();
            if (vr == "")
            {
                if (mesto != "")
                {
                    int cityID = GeneralHelper.IDOfCity_Name(mesto);
                    //city.SelectInTable("Name", mesto);
                    if (cityID == -1)
                    {
                        TableRowCities city = new TableRowCities();
                        city.ID = cityID;
                        city.Name = mesto;
                        city.InsertToTable();
                    }
                    IDCity = cityID;
                }
                else
                {
                    IDCity = -1;
                }
            }
            return vr;
        }

        public static void IsPasswordSecure(HtmlSB sb, string Heslo)
        {
            ComplexInfoString cis = new ComplexInfoString(Heslo);
            if (cis.QuantityNumbers < 2)
            {
                sb.AddItem("Heslo musí mít alespoň 2 číslice");
            }
            if (cis.QuantitySpecialChars == 0)
            {
                sb.AddItem("Heslo musí mít aspoň jeden speciální znak");
            }
            if (Heslo.Length < 10 || Heslo.Length > 20)
            {
                sb.AddItem("Heslo musí mít mezi 10-20 znaky");
            }
        }

        static string BasicTest40(string name, string Login)
        {
            return TableRowHelper.BasicTest(name, Login, 40);
        }

        static string BasicTest1000(string name, string Login)
        {
            return TableRowHelper.BasicTest(name, Login, 1000);
        }

        /// <summary>
        /// Zkontroluje zda text není prázdný a zda má 30 znaků a méně
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Login"></param>
        /// <returns></returns>
        static string BasicTest30(string name, string Login)
        {
            return TableRowHelper.BasicTest(name, Login, 30);
        }

        static string BasicTest100(string name, string Login)
        {
            return TableRowHelper.BasicTest(name, Login, 100);
        }

        static string BasicTest20(string name, string Login)
        {
            return TableRowHelper.BasicTest(name, Login, 20);
        }

        static string BasicTest25(string name, string Login)
        {
            return TableRowHelper.BasicTest(name, Login, 25);
        }
    }
}
