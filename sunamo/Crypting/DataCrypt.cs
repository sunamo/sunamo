﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Crypting
{
    /// <summary>
    /// represents Hex, Byte, Base64, or String data to encrypt/decrypt;
    /// use the .Text property to set/get a string representation 
    /// use the .Hex property to set/get a string-based Hexadecimal representation 
    /// use the .Base64 to set/get a string-based Base64 representation 
    /// Trida ktera uchovava bajty a p�ev�d� je mezi r�zn�mi form�ty.
    /// </summary>
    public class DataCrypt
    {
        /// <summary>
        /// Obsahuje bajty.
        /// </summary>
        private byte[] _b;
        private int _MaxBytes = 0;
        private int _MinBytes = 0;

        private int _StepBytes = 0;

        /// <summary>
        /// Determines the default text encoding across ALL DataCrypt instances
        /// V�choz� �k�dov�n�
        /// </summary>
        public static System.Text.Encoding DefaultEncoding = System.Text.Encoding.GetEncoding("Windows-1252");
        /// <summary>
        /// Determines the default text encoding for this DataCrypt instance
        /// K�dov�n� pro z�skav�n� string� a bajt�
        /// </summary>
        public System.Text.Encoding Encoding = DefaultEncoding;

        /// <summary>
        /// IK
        /// Creates new, empty encryption data
        /// </summary>
        public DataCrypt()
        {
        }

        /// <summary>
        /// EK, OOP
        /// Creates new encryption data with the specified byte array
        /// </summary>
        public DataCrypt(byte[] b)
        {
            _b = b;
        }

        /// <summary>
        /// EK, OOP.
        /// Creates new encryption data with the specified string; 
        /// will be converted to byte array using default encoding
        /// </summary>
        public DataCrypt(string s)
        {
            this.Text = s;
        }

        /// <summary>
        /// Creates new encryption data using the specified string and the 
        /// specified encoding to convert the string to a byte array.
        /// Pokud je A1 v jin�m k�dov�n� ne� cp1250, pou�ij tento konstruktor
        /// </summary>
        public DataCrypt(string s, System.Text.Encoding encoding)
        {
            this.Encoding = encoding;
            this.Text = s;
        }

        /// <summary>
        /// returns true if no data is present
        /// G zda je _b N nebo L0
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (_b == null)
                {
                    return true;
                }
                if (_b.Length == 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// allowed step interval, in bytes, for this data; if 0, no limit
        /// NSN, pouze se do n� jednou ukl�d�
        /// </summary>
        public int StepBytes
        {
            get { return _StepBytes; }
            set { _StepBytes = value; }
        }

        /// <summary>
        /// allowed step interval, in bits, for this data; if 0, no limit
        /// NSN, pouze se do n� jednou ukl�d�
        /// </summary>
        public int StepBits
        {
            get { return _StepBytes * 8; }
            set { _StepBytes = value / 8; }
        }

        /// <summary>
        /// minimum number of bytes allowed for this data; if 0, no limit
        /// Minimim�ln� po�et bajt� v tomto O - PP _b
        /// </summary>
        public int MinBytes
        {
            get { return _MinBytes; }
            set { _MinBytes = value; }
        }

        /// <summary>
        /// minimum number of bits allowed for this data; if 0, no limit
        /// Minim�ln� po�et byt� v t�to PP.
        /// </summary>
        public int MinBits
        {
            get { return _MinBytes * 8; }
            set { _MinBytes = value / 8; }
        }

        /// <summary>
        /// maximum number of bytes allowed for this data; if 0, no limit
        /// Maxim�ln� po�et byt� v t�to PP.
        /// </summary>
        public int MaxBytes
        {
            get { return _MaxBytes; }
            set { _MaxBytes = value; }
        }

        /// <summary>
        /// maximum number of bits allowed for this data; if 0, no limit
        /// Maxim�ln� po�et bit� v t�to PP.
        /// </summary>
        public int MaxBits
        {
            get { return _MaxBytes * 8; }
            set { _MaxBytes = value / 8; }
        }

        /// <summary>
        /// Returns the byte representation of the data; 
        /// This will be padded to MinBytes and trimmed to MaxBytes as necessary!
        /// Pokud M�m limit byt� a _b je nad limitem, ulo��m do _b jen bajty do limitu. 
        /// Pokud m�m nopak v _b m�n� bajt� ne� je v _MinBytes, zkop�ruji bajty do pole byte[_MinBytes] a t�m je dopln�m.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                if (_MaxBytes > 0)
                {
                    if (_b.Length > _MaxBytes)
                    {
                        byte[] b = new byte[_MaxBytes];
                        Array.Copy(_b, b, b.Length);
                        _b = b;
                    }
                }
                if (_MinBytes > 0)
                {
                    if (_b.Length < _MinBytes)
                    {
                        byte[] b = new byte[_MinBytes];
                        Array.Copy(_b, b, _b.Length);
                        _b = b;
                    }
                }
                return _b;
            }
            set { _b = value; }
        }

        /// <summary>
        /// Sets or returns text representation of bytes using the default text encoding
        /// P�i S p�evedu do bajt� PP _b
        /// P�i G z�sk�m �et�zec z pp _b - z�sk�m prvn� ��slo v _b a pokud bude 0 nebo v�t��, z�sk�m v�e do tohoto indexu z _b. Pokud bude _b null, G SE
        /// </summary>
        public string Text
        {
            get
            {
                if (_b == null)
                {
                    return "";
                }
                else
                {
                    int i = Array.IndexOf(_b, Convert.ToByte(0));
                    if (i >= 0)
                    {
                        return this.Encoding.GetString(_b, 0, i);
                    }
                    else
                    {
                        return this.Encoding.GetString(_b);
                    }
                }
            }
            set { _b = this.Encoding.GetBytes(value); }
        }

        /// <summary>
        /// Sets or returns Hex string representation of this data
        /// P�evede z/na PP _b
        /// </summary>
        public string Hex
        {
            get { return Utils.ToHex(_b); }
            set { _b = Utils.FromHex(value); }
        }

        /// <summary>
        /// Sets or returns Base64 string representation of this data
        /// P�evede z/na PP _b
        /// </summary>
        public string Base64
        {
            get { return Utils.ToBase64(_b); }
            set { _b = Utils.FromBase64(value); }
        }

        /// <summary>
        /// Returns text representation of bytes using the default text encoding
        /// G PP Text
        /// </summary>
        public new string ToString()
        {
            return this.Text;
        }

        /// <summary>
        /// returns Base64 string representation of this data
        /// G PP Base64
        /// </summary>
        public string ToBase64()
        {
            return this.Base64;
        }

        /// <summary>
        /// returns Hex string representation of this data
        /// G PP Hex
        /// </summary>
        public string ToHex()
        {
            return this.Hex;
        }


        public static DataCrypt FromFile(string var)
        {
            DataCrypt d = new DataCrypt();
            d.Text = TF.ReadFile(var);
            return d;
        }
    }
}
