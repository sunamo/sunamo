using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace webforms
{
    public class RegistrationLogic
    {
        public Registrace reg = new Registrace();

        public HtmlSB sb
        {
            get
            {
                return reg.sb;
            }
        }

        public bool Sex
        {
            set
            {
                reg.Sex = value;
            }
        }

        public string Login
        {
            set
            {
                reg.Login = value;
            }
            get
            {
                return reg.Login;
            }
        }

        public string Heslo
        {
            set
            {
                reg.Heslo = value;
            }
        }

        public string HesloZnovu
        {
            set
            {
                reg.HesloZnovu = value;
            }
        }

        public string Email
        {
            set
            {
                reg.Email = value;
            }
            get
            {
                return reg.Email;
            }
        }

        public MailSettings MailSettings
        {
            set
            {
                reg.MailSettings = value;
            }
        }

        public DateTime DateBorn
        {
            set
            {
                reg.DateBorn = value;
            }
            get
            {
                return reg.DateBorn;
            }
        }

        public byte IDState
        {
            set
            {
                reg.IDState = value;
            }
            get
            {
                return reg.IDState;
            }
        }

        public short IDRegion
        {
            set
            {
                reg.IDRegion = value;
            }
            get
            {
                return reg.IDRegion;
            }
        }

        public byte IDDistrict
        {
            set
            {
                reg.IDDistrict = value;
            }
            get
            {
                return reg.IDDistrict;
            }
        }

        /// <summary>
        /// Konstruktor, který se používá, chci li nstvit všechny položky pomocí vlastností
        /// Nezapomeň zkontrolovat zda je všechno OK metodou reg.HtmlAreAllItemsOK()
        /// </summary>
        public RegistrationLogic()
        {

        }

        /// <summary>
        /// Vrací ID vloženého uživatele
        /// </summary>
        public string DoRegister() //out string login, out string heslo)
        {
            string code = Logins.CreateSc();

            TableRowUsersActivates tru = null;
            string secQ = "";
            string secA = "";
            secQ = reg.SecQ;
            secA = reg.SecA;
            //}
            while (global::MSStoredProceduresI.ci.SelectExists(Tables.UsersActivates, "Code", code))
            {
                code = Logins.CreateSc();
            }
            tru = new TableRowUsersActivates(code, reg.Login, reg.Heslo, reg.Email, DateTime.Today.AddDays(7), secQ, secA, reg.Sex, reg.DateBorn, (byte)reg.MailSettings, IDState, IDRegion, IDDistrict, reg.IDCity);
            tru.InsertToTable();
            return code;
        }
    }
}
