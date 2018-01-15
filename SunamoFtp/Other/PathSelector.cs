using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoFtp
{
    public class PathSelector
    {
        string firstToken = "";
        public List<string> tokens = new List<string>();
        bool firstTokenMustExists = false;
        string delimiter = "";
        public string Delimiter
        {
            get
            {
                return delimiter;
            }
        }
        public int indexZero = 0;

        public string FirstToken
        {
            get
            {
                return firstToken;
            }
        }

        public  string[] DivideToTokens(string r)
        {
            return SH.Split(r, delimiter);
        }

        /// <summary>
        /// A1 je složka, která je nejvyšší. Může být nastavena na C:\, www, SE nebo cokoliv jiného
        /// Pracuje buď s \ nebo s / - podle toho co najde v A1. Libovolně lze přidat další oddělovače
        /// </summary>
        /// <param name="initialDirectory"></param>
        public PathSelector(string initialDirectory)
        {
            if (initialDirectory.Contains(":\\") || initialDirectory != "")
            {
                firstTokenMustExists = true;

            }
            if (initialDirectory.Contains("\\"))
            {
                delimiter = "\\";
            }
            else
            {
                delimiter = "/";
                if (initialDirectory.Contains(delimiter))
                {
                    if (initialDirectory.StartsWith("/"))
                    {
                        throw new Exception("Počáteční složka nemůže začínat s lomítkem na začátku");
                        int druhy = initialDirectory.IndexOf('/', 1);
                        firstToken = initialDirectory.Substring(0, druhy);
                    }
                    else
                    {
                        int prvni = initialDirectory.IndexOf('/');
                        firstToken = initialDirectory.Substring(0, prvni);
                    }
                }
            }

            if (firstTokenMustExists)
            {
                indexZero = 1;
            }
            ActualPath = initialDirectory;
        }

        int Count
        {
            get
            {
                return tokens.Count;
            }
        }
        public void RemoveLastTokenForce()
        {
            tokens.RemoveAt(Count - 1);
        }

        public void RemoveLastToken()
        {
            if (CanGoToUpFolder)
            {
                tokens.RemoveAt(Count - 1);
            }
            else
            {
                throw new Exception();
            }
        }

        public string GetLastToken()
        {
            return tokens[Count - 1];
        }

        public void AddToken(string token)
        {
            tokens.Add(token);
        }

        public bool CanGoToUpFolder
        {
            get
            {
                return Count > indexZero;
            }
        }

        public string 
            ActualPath
        {
            get
            {
                if (tokens.Count != 0)
                {
                    return SH.JoinWithoutEndTrimDelimiter(delimiter, tokens.ToArray());
                }
                else
                {
                    return "/";
                }
            }
            set
            {
                tokens.Clear();
                tokens.AddRange(SH.Split(value, delimiter));
            }
        }

        
    }
}
