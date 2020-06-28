using System.IO;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Security;
using System.Configuration;
using System.Text.RegularExpressions;

namespace shared.Crypting
{
    /// <summary>
    /// Symmetric encryption uses a single key to encrypt and decrypt. 
    /// Both parties (encryptor and decryptor) must share the same secret key.
    /// Symetricke sifrovani pouzivajici jeden klic pro krypt i dekrypt.
    /// Obe casti dekryptor i kryptor musi sdilet stejny klic.
    /// </summary>
    public class Symmetric
    {
        private const string _DefaultIntializationVector = "%1Az=-@qT";
        private const int _BufferSize = 2048;

        /// <summary>
        /// Provideri symetrickeho sifrovani.
        /// </summary>
        public enum Provider
        {
            /// <summary>
            /// The DataCrypt Encryption Standard provider supports a 64 bit key only
            /// </summary>
            DES,
            /// <summary>
            /// The Rivest Cipher 2 provider supports keys ranging from 40 to 128 bits, default is 128 bits
            /// </summary>
            RC2,
            /// <summary>
            /// The Rijndael (also known as AES) provider supports keys of 128, 192, or 256 bits with a default of 256 bits
            /// </summary>
            Rijndael,
            /// <summary>
            /// The TripleDES provider (also known as 3DES) supports keys of 128 or 192 bits with a default of 192 bits
            /// </summary>
            TripleDES
        }

        private DataCrypt _data;
        private DataCrypt _key;
        private DataCrypt _iv;
        private SymmetricAlgorithm _crypto;
        /// <summary>
        /// IK
        /// </summary>
        private Symmetric()
        {
        }

        /// <summary>
        /// Instantiates a new symmetric encryption object using the specified Provider.
        /// Ulozim do PP _crypto spravnou instanci dle A1. Vygeneruje nahodny klic a pokud !A2, tak i IV. Pokud A2, jako iV nastavim _DefaultIntializationVector 
        /// Automaticky vypocte nahodny klic a ulozi jej do PP Key.
        /// </summary>
        public Symmetric(Provider provider, bool useDefaultInitializationVector)
        {
            switch (provider)
            {
                case Provider.DES:
                    _crypto = new DESCryptoServiceProvider();
                    break;
                case Provider.RC2:
                    _crypto = new RC2CryptoServiceProvider();
                    break;
                case Provider.Rijndael:
                    _crypto = new RijndaelManaged();
                    _crypto.Mode = CipherMode.CBC;

                    break;
                case Provider.TripleDES:
                    _crypto = new TripleDESCryptoServiceProvider();
                    break;
            }

            // - make sure key and IV are always set, no matter what
            this.Key = RandomKey();
            if (useDefaultInitializationVector)
            {
                this.IntializationVector = new DataCrypt(_DefaultIntializationVector);
            }
            else
            {
                this.IntializationVector = RandomInitializationVector();
            }
        }

        /// <summary>
        /// Key size in bytes. We use the default key size for any given provider; if you 
        /// want to force a specific key size, set this property
        /// Velikost klice v bajtech. My pouzivame vychozi velikost klice pro jakohokoliv pouzivaneho providera. Pokud chces nastavit vlastni velikost klice, pouiij tuto VV
        /// Uklada se do PP _crypto a _key, ale neprenes se na zadne bity - leda ze by si to ty objekty delali sami.
        /// </summary>
        public int KeySizeBytes
        {
            get { return _crypto.KeySize / 8; }
            set
            {
                _crypto.KeySize = value * 8;
                _key.MaxBytes = value;
            }
        }

        /// <summary>
        /// Key size in bits. We use the default key size for any given provider; if you 
        /// want to force a specific key size, set this property
        /// Velikost klice v bajtech. My pouzivame vychozi velikost klice pro jakohokoliv pouzivaneho providera. Pokud chces nastavit vlastni velikost klice, pouzij tuto VV
        /// Uklada se do PP _crypto a _key, ale neprenes se na zadne bajty - leda ze by si to ty objekty delali sami.
        /// </summary>
        public int KeySizeBits
        {
            get { return _crypto.KeySize; }
            set
            {
                _crypto.KeySize = value;
                _key.MaxBits = value;
            }
        }

        /// <summary>
        /// The key used to encrypt/decrypt data
        /// GS klic. Pri S nastavim Min Max a Step hodnoty z _crypto.LegalKeySizes[0] prevede na byty(/8)
        /// </summary>
        public DataCrypt Key
        {
            get { return _key; }
            set
            {
                _key = value;
                _key.MaxBytes = _crypto.LegalKeySizes[0].MaxSize / 8;
                _key.MinBytes = _crypto.LegalKeySizes[0].MinSize / 8;
                _key.StepBytes = _crypto.LegalKeySizes[0].SkipSize / 8;
            }
        }

        /// <summary>
        /// Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
        /// the value derived from the previous block; the first data block has no previous data block
        /// to use, so it needs an InitializationVector to feed the first block
        /// GS PP. Pri S ulozim do IV do Min a Max Bytes stejnou hodnotu - osminu _crypto.BlockSize(cili defakto velikost bloku
        /// </summary>
        public DataCrypt IntializationVector
        {
            get { return _iv; }
            set
            {
                _iv = value;
                _iv.MaxBytes = _crypto.BlockSize / 8;
                _iv.MinBytes = _crypto.BlockSize / 8;
            }
        }

        /// <summary>
        /// generates a random Initialization Vector, if one was not provided
        /// G nahodne IV
        /// </summary>
        public DataCrypt RandomInitializationVector()
        {
            _crypto.GenerateIV();
            DataCrypt d = new DataCrypt(_crypto.IV);
            return d;
        }

        /// <summary>
        /// generates a random Key, if one was not provided
        /// Vygeneruje nahodny klic O _crypto a vratim jej v O DataCrypt
        /// </summary>
        public DataCrypt RandomKey()
        {
            _crypto.GenerateKey();
            DataCrypt d = new DataCrypt(_crypto.Key);
            return d;
        }

        static Type type = typeof(Symmetric);

        /// <summary>
        /// Ensures that _crypto object has valid Key and IV
        /// prior to any attempt to encrypt/decrypt anythingv
        /// A2 zda se kryptuje. Pokud _key.IsEmpty a A2, vygeneruji do _crypto.Key nahodny klic, jinak VV. To same jako s klicem i s IV
        /// </summary>
        private void ValidateKeyAndIv(bool isEncrypting)
        {
            if (_key.IsEmpty)
            {
                if (isEncrypting)
                {
                    _key = RandomKey();
                }
                else
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.NoKeyWasProvidedForTheDecryptionOperation) + "!");
                }
            }
            if (_iv.IsEmpty)
            {
                if (isEncrypting)
                {
                    _iv = RandomInitializationVector();
                }
                else
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.NoInitializationVectorWasProvidedForTheDecryptionOperation) + "!");
                }
            }
            _crypto.Key = _key.Bytes;
            _crypto.IV = _iv.Bytes;
        }

        /// <summary>
        /// Encrypts the specified DataCrypt using provided key
        /// Zakoduuji data A1 s klicem A2. A2 OOP.
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d, DataCrypt key)
        {
            this.Key = key;
            return Encrypt(d);
        }

        /// <summary>
        /// Encrypts the specified DataCrypt using preset key and preset initialization vector
        /// Zkontroluji platnost klice a IV a pokud nebudou platne, vygeneruji je. 
        /// Zakryptuji A1 objektem _crypto a G
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            ValidateKeyAndIv(true);

            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(d.Bytes, 0, d.Bytes.Length);
            cs.Close();
            ms.Close();

            return new DataCrypt(ms.ToArray());
        }

        /// <summary>
        /// Encrypts the stream to memory using provided key and provided initialization vector
        /// Zakryptuji proud A1 s klicem A2 a IV A3. A2,3 OOP
        /// </summary>
        public DataCrypt Encrypt(Stream s, DataCrypt key, DataCrypt iv)
        {
            this.IntializationVector = iv;
            this.Key = key;
            return Encrypt(s);
        }

        /// <summary>
        /// Encrypts the stream to memory using specified key
        /// Zakryptuji proud A1 s klicem A1. A1 OOP
        /// </summary>
        public DataCrypt Encrypt(Stream s, DataCrypt key)
        {
            this.Key = key;
            return Encrypt(s);
        }

        /// <summary>
        /// Encrypts the specified stream to memory using preset key and preset initialization vector
        /// Zasifruje proud A1. Pokud neude platny Iv nebo key, vygeneruji novy.
        /// </summary>
        public DataCrypt Encrypt(Stream s)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] b = new byte[_BufferSize + 1];
            int i = 0;

            ValidateKeyAndIv(true);

            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            i = s.Read(b, 0, _BufferSize);
            while (i > 0)
            {
                cs.Write(b, 0, i);
                i = s.Read(b, 0, _BufferSize);
            }

            cs.Close();
            ms.Close();

            return new DataCrypt(ms.ToArray());
        }

        /// <summary>
        /// Decrypts the specified data using provided key and preset initialization vector
        /// Dekryptuje data A1 s klicem A2 ktery OOP
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, DataCrypt key)
        {
            this.Key = key;
            return Decrypt(encryptedDataCrypt);
        }

        /// <summary>
        /// Decrypts the specified stream using provided key and preset initialization vector
        /// Dekryptuje proud A1 s klicem A2. A2 OOP
        /// </summary>
        public DataCrypt Decrypt(Stream encryptedStream, DataCrypt key)
        {
            this.Key = key;
            return Decrypt(encryptedStream);
        }

        /// <summary>
        /// Decrypts the specified stream using preset key and preset initialization vector
        /// Dekryptuje A1. Pokud klic a IV nebyly zadany, a budou prazdne, VV
        /// </summary>
        public DataCrypt Decrypt(Stream encryptedStream)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] b = new byte[_BufferSize + 1];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(encryptedStream, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            int i = 0;
            i = cs.Read(b, 0, _BufferSize);

            while (i > 0)
            {
                ms.Write(b, 0, i);
                i = cs.Read(b, 0, _BufferSize);
            }
            cs.Close();
            ms.Close();

            return new DataCrypt(ms.ToArray());
        }

        /// <summary>
        /// Decrypts the specified data using preset key and preset initialization vector
        /// Dekryptuje data A1. Musi byt zadan platny klic a IV.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(encryptedDataCrypt.Bytes, 0, encryptedDataCrypt.Bytes.Length);
            byte[] b = new byte[encryptedDataCrypt.Bytes.Length];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(ms, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            try
            {
                cs.Read(b, 0, encryptedDataCrypt.Bytes.Length - 1);
            }
            catch (CryptographicException ex)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.UnableToDecryptDataTheProvidedKeyMayBeInvalid) + ".");
            }
            finally
            {
                cs.Close();
            }
            return new DataCrypt(b);
        }
    }
}