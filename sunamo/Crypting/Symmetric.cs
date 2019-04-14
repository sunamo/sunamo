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
    /// Symetrick� �ifrov�n� pou��vaj�c� jeden kl�� pro krypt i dekrypt.
    /// Ob� ��sti dekryptor i kryptor mus� sd�let stejn� kl��.
    /// </summary>
    public class Symmetric
    {
        private const string _DefaultIntializationVector = "%1Az=-@qT";
        private const int _BufferSize = 2048;

        /// <summary>
        /// Provide�i symetrick�ho �ifrov�n�.
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
        /// Ulo��m do PP _crypto spr�vnou instanci dle A1. Vygeneruje n�hodn� kl�� a pokud !A2, tak i IV. Pokud A2, jako iV nastav�m _DefaultIntializationVector 
        /// Automaticky vypo�te n�hodn� kl�� a ulo�� jej do PP Key.
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
        /// Velikost kl��e v bajtech. My pou��v�me v�choz� velikost kl��e pro jak�hokoliv pou��van�ho providera. Pokud chce� nastavit vlastn� velikost kl��e, pou�ij tuto VV
        /// Ukl�d� se do PP _crypto a _key, ale nep�en�� se na ��dn� bity - leda �e by si to ty objekty d�lali sami.
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
        /// Velikost kl��e v bajtech. My pou��v�me v�choz� velikost kl��e pro jak�hokoliv pou��van�ho providera. Pokud chce� nastavit vlastn� velikost kl��e, pou�ij tuto VV
        /// Ukl�d� se do PP _crypto a _key, ale nep�en�� se na ��dn� bajty - leda �e by si to ty objekty d�lali sami.
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
        /// GS kl��. P�i S nastav�m Min Max a Step hodnoty z _crypto.LegalKeySizes[0] p�evede na byty(/8)
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
        /// GS PP. P�i S ulo��m do IV do Min a Max Bytes stejnou hodnotu - osminu _crypto.BlockSize(�ili defakto velikost bloku
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
        /// G n�hodn� IV
        /// </summary>
        public DataCrypt RandomInitializationVector()
        {
            _crypto.GenerateIV();
            DataCrypt d = new DataCrypt(_crypto.IV);
            return d;
        }

        /// <summary>
        /// generates a random Key, if one was not provided
        /// Vygeneruje n�hodn� kl�� O _crypto a vr�t�m jej v O DataCrypt
        /// </summary>
        public DataCrypt RandomKey()
        {
            _crypto.GenerateKey();
            DataCrypt d = new DataCrypt(_crypto.Key);
            return d;
        }

        /// <summary>
        /// Ensures that _crypto object has valid Key and IV
        /// prior to any attempt to encrypt/decrypt anythingv
        /// A2 zda se kryptuje. Pokud _key.IsEmpty a A2, vygeneruji do _crypto.Key n�hodn� kl��, jinak VV. To sam� jako s kl��em i s IV
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
                    throw new CryptographicException("No key was provided for the decryption operation!");
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
                    throw new CryptographicException("No initialization vector was provided for the decryption operation!");
                }
            }
            _crypto.Key = _key.Bytes;
            _crypto.IV = _iv.Bytes;
        }

        /// <summary>
        /// Encrypts the specified DataCrypt using provided key
        /// Zak�duuji data A1 s kl��em A2. A2 OOP.
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d, DataCrypt key)
        {
            this.Key = key;
            return Encrypt(d);
        }

        /// <summary>
        /// Encrypts the specified DataCrypt using preset key and preset initialization vector
        /// Zkontroluji platnost kl��e a IV a pokud nebudou platn�, vygeneruji je. 
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
        /// Zakryptuji proud A1 s kl��em A2 a IV A3. A2,3 OOP
        /// </summary>
        public DataCrypt Encrypt(Stream s, DataCrypt key, DataCrypt iv)
        {
            this.IntializationVector = iv;
            this.Key = key;
            return Encrypt(s);
        }

        /// <summary>
        /// Encrypts the stream to memory using specified key
        /// Zakryptuji proud A1 s kl��em A1. A1 OOP
        /// </summary>
        public DataCrypt Encrypt(Stream s, DataCrypt key)
        {
            this.Key = key;
            return Encrypt(s);
        }

        /// <summary>
        /// Encrypts the specified stream to memory using preset key and preset initialization vector
        /// Za�ifruje proud A1. Pokud neude platn� Iv nebo key, vygeneruji nov�.
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
        /// Dekryptuje data A1 s kl��em A2 kter� OOP
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, DataCrypt key)
        {
            this.Key = key;
            return Decrypt(encryptedDataCrypt);
        }

        /// <summary>
        /// Decrypts the specified stream using provided key and preset initialization vector
        /// Dekryptuje proud A1 s kl��em A2. A2 OOP
        /// </summary>
        public DataCrypt Decrypt(Stream encryptedStream, DataCrypt key)
        {
            this.Key = key;
            return Decrypt(encryptedStream);
        }

        /// <summary>
        /// Decrypts the specified stream using preset key and preset initialization vector
        /// Dekryptuje A1. Pokud kl�� a IV nebyly zad�ny, a budou pr�zdn�, VV
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
        /// Dekryptuje data A1. Mus� b�t zad�n platn� kl�� a IV.
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
                throw new CryptographicException("Unable to decrypt data. The provided key may be invalid.", ex);
            }
            finally
            {
                cs.Close();
            }
            return new DataCrypt(b);
        }

    }
}
