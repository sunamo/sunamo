using System.IO;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Security;
using System.Configuration;
using System.Text.RegularExpressions;
// TENTO NS NIKDY NEODSTRA�UJ, SLOU�� K N�KOLIKA V�CEM, MIMO JIN� NEN� K POU�IT� ZE M�CH WEB� A PROGRAM� A TAK� 
namespace sunamo
{
    #region "  Hash"

    /// <summary>
    /// Hash functions are fundamental to modern cryptography. These functions map binary 
    /// strings of an arbitrary length to small binary strings of a fixed length, known as 
    /// hash values. A cryptographic hash function has the property that it is computationally
    /// infeasible to find two distinct inputs that hash to the same value. Hash functions 
    /// are commonly used with digital signatures and for data integrity.
    /// </summary>
    public class Hash
    {
        /// <summary>
        /// Type of hash; some are security oriented, others are fast and simple
        /// V��et mo�n�ch hashovac�ch provider� v .NETu
        /// </summary>
        public enum Provider
        {
            /// <summary>
            /// Cyclic Redundancy Check provider, 32-bit
            /// </summary>
            CRC32,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-1 variant, 160-bit
            /// </summary>
            SHA1,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-2 variant, 256-bit
            /// </summary>
            SHA256,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-2 variant, 384-bit
            /// </summary>
            SHA384,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-2 variant, 512-bit
            /// </summary>
            SHA512,
            /// <summary>
            /// Message Digest algorithm 5, 128-bit
            /// </summary>
            MD5
        }

        /// <summary>
        /// T��da pro po��t�n� Hashe
        /// </summary>
        private HashAlgorithm _Hash;
        /// <summary>
        /// Naposledy vypo��tan� Hash
        /// </summary>
        private DataCrypt _HashValue = new DataCrypt();

        /// <summary>
        /// IK
        /// </summary>
        private Hash()
        {
        }

        /// <summary>
        /// Instantiate a new hash of the specified type
        /// </summary>
        public Hash(Provider p)
        {
            switch (p)
            {
                case Provider.CRC32:
                    _Hash = new CRC32();
                    break;
                case Provider.MD5:
                    _Hash = new MD5CryptoServiceProvider();
                    break;
                case Provider.SHA1:
                    _Hash = new SHA1Managed();
                    break;
                case Provider.SHA256:
                    _Hash = new SHA256Managed();
                    break;
                case Provider.SHA384:
                    _Hash = new SHA384Managed();
                    break;
                case Provider.SHA512:
                    _Hash = new SHA512Managed();
                    break;
            }
        }

        /// <summary>
        /// Returns the previously calculated hash
        /// G vypo��t�nou hodnotu hash.
        /// </summary>
        public DataCrypt Value
        {
            get { return _HashValue; }
        }

        /// <summary>
        /// Calculates hash on a stream of arbitrary length
        /// Vypo��t� hash ze streamu A1.
        /// </summary>
        public DataCrypt Calculate(ref System.IO.Stream s)
        {
            _HashValue.Bytes = _Hash.ComputeHash(s);
            return _HashValue;
        }

        /// <summary>
        /// Calculates hash for fixed length <see cref="DataCrypt"/>
        /// Vypo��t�m hash z A1 M CalculatePrivate a G
        /// </summary>
        public DataCrypt Calculate(DataCrypt d)
        {
            return CalculatePrivate(d.Bytes);
        }

        /// <summary>
        /// Calculates hash for a string with a prefixed salt value. 
        /// A "salt" is random data prefixed to every hashed value to prevent 
        /// common dictionary attacks.
        /// VLo��m do nov�ho pole o velikost A1+A2 nejd��ve A2 a hned za n�m A1. Vypo��t�m Hash M CalculatePrivate
        /// Private znamen� �e ulo��m v�sledek do priv�tn� PP _HashValue
        /// </summary>
        public DataCrypt Calculate(DataCrypt d, DataCrypt salt)
        {
            byte[] nb = new byte[d.Bytes.Length + salt.Bytes.Length];
            salt.Bytes.CopyTo(nb, 0);
            d.Bytes.CopyTo(nb, salt.Bytes.Length);
            return CalculatePrivate(nb);
        }

        /// <summary>
        /// Calculates hash for an array of bytes
        /// Vypo��t�m Hash t��dou HashAlgorithm, ulo��m do _HashValue a G
        /// </summary>
        private DataCrypt CalculatePrivate(byte[] b)
        {
            _HashValue.Bytes = _Hash.ComputeHash(b);
            return _HashValue;
        }

        #region "  CRC32 HashAlgorithm"
        /// <summary>
        /// Vlastn� t��da pro po��t�n� CRC32 Hashe
        /// </summary>
        private class CRC32 : HashAlgorithm
        {
            private uint result = 0xffffffff;
            /// <summary>
            /// Vypo�tu zbytek z p�edchoz�ho v�sledku a pak si logick�m OR vypo�tu index jeho� prvekm z [] crcLookup to za�ifruji 
            /// U p�edchoz�ho v�sledku posledn� 2 ��sla (bajt), vyd�l�m to cel� 256 a vynuluji posledn�ch 6 znak�
            /// Ud�l�m bitov� xor na poli  crcLookup s v�sledkem
            /// </summary>
            /// <param name="array"></param>
            /// <param name="ibStart"></param>
            /// <param name="cbSize"></param>
            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                uint lookup = 0;
                for (int i = ibStart; i <= cbSize - 1; i++)
                {
                    lookup = (result & 0xff) ^ array[i];
                    result = ((result & 0xffffff00) / 0x100) & 0xffffff;
                    // Ud�l�m bitov� xor na poli  crcLookup s v�sledkem
                    result = result ^ crcLookup[lookup];
                }
            }

            /// <summary>
            /// neguji bajty z v�sledku, obr�t�m je a reservuji a G
            /// </summary>
            /// <returns></returns>
            protected override byte[] HashFinal()
            {
                byte[] b = BitConverter.GetBytes(~(result));
                Array.Reverse(b);
                return b;
            }

            /// <summary>
            /// jako defaultn� v�sledek nastav� -1, hexadecim�ln� vyj�d�en� +1 je 0x1
            /// </summary>
            public override void Initialize()
            {
                result = 0xffffffff;
            }

            #region crcLookup
            /// <summary>
            /// Obsahuje 256 hex. ��sel, to znamen� �e velikost je 1024 bajt�
            /// </summary>
            private uint[] crcLookup = {
				0x0,
				0x77073096,
				0xee0e612c,
				0x990951ba,
				0x76dc419,
				0x706af48f,
				0xe963a535,
				0x9e6495a3,
				0xedb8832,
				0x79dcb8a4,
				0xe0d5e91e,
				0x97d2d988,
				0x9b64c2b,
				0x7eb17cbd,
				0xe7b82d07,
				0x90bf1d91,
				0x1db71064,
				0x6ab020f2,
				0xf3b97148,
				0x84be41de,
				0x1adad47d,
				0x6ddde4eb,
				0xf4d4b551,
				0x83d385c7,
				0x136c9856,
				0x646ba8c0,
				0xfd62f97a,
				0x8a65c9ec,
				0x14015c4f,
				0x63066cd9,
				0xfa0f3d63,
				0x8d080df5,
				0x3b6e20c8,
				0x4c69105e,
				0xd56041e4,
				0xa2677172,
				0x3c03e4d1,
				0x4b04d447,
				0xd20d85fd,
				0xa50ab56b,
				0x35b5a8fa,
				0x42b2986c,
				0xdbbbc9d6,
				0xacbcf940,
				0x32d86ce3,
				0x45df5c75,
				0xdcd60dcf,
				0xabd13d59,
				0x26d930ac,
				0x51de003a,
				0xc8d75180,
				0xbfd06116,
				0x21b4f4b5,
				0x56b3c423,
				0xcfba9599,
				0xb8bda50f,
				0x2802b89e,
				0x5f058808,
				0xc60cd9b2,
				0xb10be924,
				0x2f6f7c87,
				0x58684c11,
				0xc1611dab,
				0xb6662d3d,
				0x76dc4190,
				0x1db7106,
				0x98d220bc,
				0xefd5102a,
				0x71b18589,
				0x6b6b51f,
				0x9fbfe4a5,
				0xe8b8d433,
				0x7807c9a2,
				0xf00f934,
				0x9609a88e,
				0xe10e9818,
				0x7f6a0dbb,
				0x86d3d2d,
				0x91646c97,
				0xe6635c01,
				0x6b6b51f4,
				0x1c6c6162,
				0x856530d8,
				0xf262004e,
				0x6c0695ed,
				0x1b01a57b,
				0x8208f4c1,
				0xf50fc457,
				0x65b0d9c6,
				0x12b7e950,
				0x8bbeb8ea,
				0xfcb9887c,
				0x62dd1ddf,
				0x15da2d49,
				0x8cd37cf3,
				0xfbd44c65,
				0x4db26158,
				0x3ab551ce,
				0xa3bc0074,
				0xd4bb30e2,
				0x4adfa541,
				0x3dd895d7,
				0xa4d1c46d,
				0xd3d6f4fb,
				0x4369e96a,
				0x346ed9fc,
				0xad678846,
				0xda60b8d0,
				0x44042d73,
				0x33031de5,
				0xaa0a4c5f,
				0xdd0d7cc9,
				0x5005713c,
				0x270241aa,
				0xbe0b1010,
				0xc90c2086,
				0x5768b525,
				0x206f85b3,
				0xb966d409,
				0xce61e49f,
				0x5edef90e,
				0x29d9c998,
				0xb0d09822,
				0xc7d7a8b4,
				0x59b33d17,
				0x2eb40d81,
				0xb7bd5c3b,
				0xc0ba6cad,
				0xedb88320,
				0x9abfb3b6,
				0x3b6e20c,
				0x74b1d29a,
				0xead54739,
				0x9dd277af,
				0x4db2615,
				0x73dc1683,
				0xe3630b12,
				0x94643b84,
				0xd6d6a3e,
				0x7a6a5aa8,
				0xe40ecf0b,
				0x9309ff9d,
				0xa00ae27,
				0x7d079eb1,
				0xf00f9344,
				0x8708a3d2,
				0x1e01f268,
				0x6906c2fe,
				0xf762575d,
				0x806567cb,
				0x196c3671,
				0x6e6b06e7,
				0xfed41b76,
				0x89d32be0,
				0x10da7a5a,
				0x67dd4acc,
				0xf9b9df6f,
				0x8ebeeff9,
				0x17b7be43,
				0x60b08ed5,
				0xd6d6a3e8,
				0xa1d1937e,
				0x38d8c2c4,
				0x4fdff252,
				0xd1bb67f1,
				0xa6bc5767,
				0x3fb506dd,
				0x48b2364b,
				0xd80d2bda,
				0xaf0a1b4c,
				0x36034af6,
				0x41047a60,
				0xdf60efc3,
				0xa867df55,
				0x316e8eef,
				0x4669be79,
				0xcb61b38c,
				0xbc66831a,
				0x256fd2a0,
				0x5268e236,
				0xcc0c7795,
				0xbb0b4703,
				0x220216b9,
				0x5505262f,
				0xc5ba3bbe,
				0xb2bd0b28,
				0x2bb45a92,
				0x5cb36a04,
				0xc2d7ffa7,
				0xb5d0cf31,
				0x2cd99e8b,
				0x5bdeae1d,
				0x9b64c2b0,
				0xec63f226,
				0x756aa39c,
				0x26d930a,
				0x9c0906a9,
				0xeb0e363f,
				0x72076785,
				0x5005713,
				0x95bf4a82,
				0xe2b87a14,
				0x7bb12bae,
				0xcb61b38,
				0x92d28e9b,
				0xe5d5be0d,
				0x7cdcefb7,
				0xbdbdf21,
				0x86d3d2d4,
				0xf1d4e242,
				0x68ddb3f8,
				0x1fda836e,
				0x81be16cd,
				0xf6b9265b,
				0x6fb077e1,
				0x18b74777,
				0x88085ae6,
				0xff0f6a70,
				0x66063bca,
				0x11010b5c,
				0x8f659eff,
				0xf862ae69,
				0x616bffd3,
				0x166ccf45,
				0xa00ae278,
				0xd70dd2ee,
				0x4e048354,
				0x3903b3c2,
				0xa7672661,
				0xd06016f7,
				0x4969474d,
				0x3e6e77db,
				0xaed16a4a,
				0xd9d65adc,
				0x40df0b66,
				0x37d83bf0,
				0xa9bcae53,
				0xdebb9ec5,
				0x47b2cf7f,
				0x30b5ffe9,
				0xbdbdf21c,
				0xcabac28a,
				0x53b39330,
				0x24b4a3a6,
				0xbad03605,
				0xcdd70693,
				0x54de5729,
				0x23d967bf,
				0xb3667a2e,
				0xc4614ab8,
				0x5d681b02,
				0x2a6f2b94,
				0xb40bbe37,
				0xc30c8ea1,
				0x5a05df1b,
				0x2d02ef8d

			};
            #endregion
            public override byte[] Hash
            {
                get
                {
                    byte[] b = BitConverter.GetBytes(~(result));
                    Array.Reverse(b);
                    return b;
                }
            }
        }

        #endregion

    }
    #endregion

    #region "  Symmetric"

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

    #endregion

    #region "  Asymmetric"

    /// <summary>
    /// Asymmetric encryption uses a pair of keys to encrypt and decrypt.
    /// There is a "public" key which is used to encrypt. Decrypting, on the other hand, 
    /// requires both the "public" key and an additional "private" key. The advantage is 
    /// that people can send you encrypted messages without being able to decrypt them.
    /// </summary>
    /// <remarks>
    /// The only provider supported is the <see cref="RSACryptoServiceProvider"/>
    /// </remarks>
    public class Asymmetric
    {
        /// <summary>
        /// Provider �ifrov�n� RSA
        /// </summary>
        private RSACryptoServiceProvider _rsa;
        /// <summary>
        /// V�choz� jm�no kontejneru, ve kter�m se bude uchov�vat kl��.
        /// </summary>
        private string _KeyContainerName = "Encryption.AsymmetricEncryption.DefaultContainerName";
        /// <summary>
        /// V�choz� velikost kl��e v bytech.
        /// </summary>
        private int _KeySize = 1024;
        #region N�zvy element� pro ukl�d�n� do XML
        private const string _ElementParent = "RSAKeyValue";
        private const string _ElementModulus = "Modulus";
        private const string _ElementExponent = "Exponent";
        private const string _ElementPrimeP = "P";
        private const string _ElementPrimeQ = "Q";
        private const string _ElementPrimeExponentP = "DP";
        private const string _ElementPrimeExponentQ = "DQ";
        private const string _ElementCoefficient = "InverseQ";


        private const string _ElementPrivateExponent = "D";
        #endregion
        // - http://forum.java.sun.com/thread.jsp?forum=9&thread=552022&tstart=0&trange=15 
        #region N�zvy element� pro ukl�d�n� do CM.AS
        private const string _KeyModulus = "PublicKey.Modulus";
        private const string _KeyExponent = "PublicKey.Exponent";
        private const string _KeyPrimeP = "PrivateKey.P";
        private const string _KeyPrimeQ = "PrivateKey.Q";
        private const string _KeyPrimeExponentP = "PrivateKey.DP";
        private const string _KeyPrimeExponentQ = "PrivateKey.DQ";
        private const string _KeyCoefficient = "PrivateKey.InverseQ";

        private const string _KeyPrivateExponent = "PrivateKey.D";
        #endregion

        #region "  PublicKey class"
        /// <summary>
        /// Represents a public encryption key. Intended to be shared, it 
        /// contains only the Modulus and Exponent.
        /// T��da ve�ejn�ho kl��e. M� metody pro na�ten� a ulo�en� z/do r�zn�ch zdroj�.
        /// </summary>
        public class PublicKey
        {
            public string Modulus;

            public string Exponent;
            /// <summary>
            /// IK
            /// </summary>
            public PublicKey()
            {
            }

            /// <summary>
            /// EK. Na�tu z XML A1 obsahy tag� Modulus a Exponent a ulo��m je do stejn� pojm. VV
            /// </summary>
            /// <param name="KeyXml"></param>
            public PublicKey(string KeyXml)
            {
                LoadFromXml(KeyXml);
            }

            /// <summary>
            /// Load public key from App.config or Web.config file
            /// Ulo��m do PP z CM.AS
            /// </summary>
            public void LoadFromConfig()
            {
                this.Modulus = Utils.GetConfigString(Asymmetric._KeyModulus, true);
                this.Exponent = Utils.GetConfigString(Asymmetric._KeyExponent, true);
            }

            /// <summary>
            /// Returns *.config file XML section representing this public key
            /// Vr�t�m 2x tax Add s argumenty PP Modulus a Exponent
            /// </summary>
            public string ToConfigSection()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nev�m zda bych nem�l vytvo�it novou instanci SB
                StringBuilder _with1 = sb;
                _with1.Append(Utils.WriteConfigKey(Asymmetric._KeyModulus, this.Modulus));
                _with1.Append(Utils.WriteConfigKey(Asymmetric._KeyExponent, this.Exponent));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the *.config file representation of this public key to a file
            /// P�ep�u A1 2x tagem Add s argumenty PP Modulus a Exponent
            /// </summary>
            public void ExportToConfigFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToConfigSection());
                sw.Close();
            }

            /// <summary>
            /// Loads the public key from its XML string
            /// Na�tu z XML A1 obsahy tag� Modulus a Exponent a ulo��m je do stejn� pojm. VV
            /// </summary>
            public void LoadFromXml(string keyXml)
            {
                this.Modulus = Utils.GetXmlElement(keyXml, "Modulus");
                this.Exponent = Utils.GetXmlElement(keyXml, "Exponent");
            }

            /// <summary>
            /// Converts this public key to an RSAParameters object
            /// Vr�t� mi pp Modulus a Exponent v O RSAParameters
            /// </summary>
            public RSAParameters ToParameters()
            {
                RSAParameters r = new RSAParameters();
                r.Modulus = Convert.FromBase64String(this.Modulus);
                r.Exponent = Convert.FromBase64String(this.Exponent);
                return r;
            }

            /// <summary>
            /// Converts this public key to its XML string representation
            /// Vr�t� mi Tagy PP Modulus a Exponent v Tagu RSAKeyValue
            /// </summary>
            public string ToXml()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nev�m zda bych nem�l vytvo�it novou instanci SB
                StringBuilder _with2 = sb;
                // Mohl bych to zapsat pomoc� T RSAParameters ale nev�m jak by se to vypo��dalo s ve�ejn�m kl��em.
                _with2.Append(Utils.WriteXmlNode(Asymmetric._ElementParent, false));
                _with2.Append(Utils.WriteXmlElement(Asymmetric._ElementModulus, this.Modulus));
                _with2.Append(Utils.WriteXmlElement(Asymmetric._ElementExponent, this.Exponent));
                _with2.Append(Utils.WriteXmlNode(Asymmetric._ElementParent, true));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the Xml representation of this public key to a file
            /// P�ep�e A1 Tagy PP Modulus a Exponent v Tagu RSAKeyValue
            /// </summary>
            public void ExportToXmlFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToXml());
                sw.Close();
            }

        }
        #endregion

        #region "  PrivateKey class"

        /// <summary>
        /// Represents a private encryption key. Not intended to be shared, as it 
        /// contains all the elements that make up the key.
        /// </summary>
        public class PrivateKey
        {
            #region P�evedou se na base64 a vlo�� se do objektu RSAParameters
            public string Modulus;
            public string Exponent;
            public string PrimeP;
            public string PrimeQ;
            public string PrimeExponentP;
            public string PrimeExponentQ;
            public string Coefficient;
            public string PrivateExponent;
            #endregion

            /// <summary>
            /// IK
            /// </summary>
            public PrivateKey()
            {
            }

            /// <summary>
            /// Na�tu z XML A1 obsahy tag� Modulus a Exponent a dal�� tagy a ulo��m je do stejn� pojm. VV
            /// </summary>
            /// <param name="keyXml"></param>
            public PrivateKey(string keyXml)
            {
                LoadFromXml(keyXml);
            }

            /// <summary>
            /// Load private key from App.config or Web.config file
            /// Ulo��m do PPs z CM.AS
            /// </summary>
            public void LoadFromConfig()
            {
                this.Modulus = Utils.GetConfigString(Asymmetric._KeyModulus, true);
                this.Exponent = Utils.GetConfigString(Asymmetric._KeyExponent, true);
                this.PrimeP = Utils.GetConfigString(Asymmetric._KeyPrimeP, true);
                this.PrimeQ = Utils.GetConfigString(Asymmetric._KeyPrimeQ, true);
                this.PrimeExponentP = Utils.GetConfigString(Asymmetric._KeyPrimeExponentP, true);
                this.PrimeExponentQ = Utils.GetConfigString(Asymmetric._KeyPrimeExponentQ, true);
                this.Coefficient = Utils.GetConfigString(Asymmetric._KeyCoefficient, true);
                this.PrivateExponent = Utils.GetConfigString(Asymmetric._KeyPrivateExponent, true);
            }

            /// <summary>
            /// Converts this private key to an RSAParameters object
            /// P�evedu PPs z Base64 a vlo��m do O RSAParameter, kter� G 
            /// </summary>
            public RSAParameters ToParameters()
            {
                RSAParameters r = new RSAParameters();
                r.Modulus = Convert.FromBase64String(this.Modulus);
                r.Exponent = Convert.FromBase64String(this.Exponent);
                r.P = Convert.FromBase64String(this.PrimeP);
                r.Q = Convert.FromBase64String(this.PrimeQ);
                r.DP = Convert.FromBase64String(this.PrimeExponentP);
                r.DQ = Convert.FromBase64String(this.PrimeExponentQ);
                r.InverseQ = Convert.FromBase64String(this.Coefficient);
                r.D = Convert.FromBase64String(this.PrivateExponent);
                return r;
            }

            /// <summary>
            /// Returns *.config file XML section representing this private key
            /// Vr�t�m xx tax Add s argumenty PP Modulus a Exponent
            /// </summary>
            public string ToConfigSection()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nev�m zda bych nem�l vytvo�it novou instanci SB
                StringBuilder _with3 = sb;
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyModulus, this.Modulus));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyExponent, this.Exponent));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyPrimeP, this.PrimeP));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyPrimeQ, this.PrimeQ));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyPrimeExponentP, this.PrimeExponentP));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyPrimeExponentQ, this.PrimeExponentQ));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyCoefficient, this.Coefficient));
                _with3.Append(Utils.WriteConfigKey(Asymmetric._KeyPrivateExponent, this.PrivateExponent));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the *.config file representation of this private key to a file
            /// P�ep�u A1 2x tagem Add s argumenty PP Modulus a Exponent a dal��
            /// </summary>
            public void ExportToConfigFile(string strFilePath)
            {
                StreamWriter sw = new StreamWriter(strFilePath, false);
                sw.Write(this.ToConfigSection());
                sw.Close();
            }

            /// <summary>
            /// Loads the private key from its XML string
            /// Na�tu z XML A1 obsahy tag� Modulus a Exponent a dal�� tagy a ulo��m je do stejn� pojm. VV
            /// </summary>
            public void LoadFromXml(string keyXml)
            {
                this.Modulus = Utils.GetXmlElement(keyXml, "Modulus");
                this.Exponent = Utils.GetXmlElement(keyXml, "Exponent");
                this.PrimeP = Utils.GetXmlElement(keyXml, "P");
                this.PrimeQ = Utils.GetXmlElement(keyXml, "Q");
                this.PrimeExponentP = Utils.GetXmlElement(keyXml, "DP");
                this.PrimeExponentQ = Utils.GetXmlElement(keyXml, "DQ");
                this.Coefficient = Utils.GetXmlElement(keyXml, "InverseQ");
                this.PrivateExponent = Utils.GetXmlElement(keyXml, "D");
            }

            /// <summary>
            /// Converts this private key to its XML string representation
            /// Vr�t� mi Tagy PP Modulus a Exponent a dal�� v Tagu RSAKeyValue
            /// </summary>
            public string ToXml()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nev�m zda bych nem�l vytvo�it novou instanci SB
                StringBuilder _with4 = sb;
                _with4.Append(Utils.WriteXmlNode(Asymmetric._ElementParent, false));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementModulus, this.Modulus));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementExponent, this.Exponent));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementPrimeP, this.PrimeP));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementPrimeQ, this.PrimeQ));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementPrimeExponentP, this.PrimeExponentP));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementPrimeExponentQ, this.PrimeExponentQ));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementCoefficient, this.Coefficient));
                _with4.Append(Utils.WriteXmlElement(Asymmetric._ElementPrivateExponent, this.PrivateExponent));
                _with4.Append(Utils.WriteXmlNode(Asymmetric._ElementParent, true));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the Xml representation of this private key to a file
            /// P�ep�e A1 Tagy PP Modulus a Exponent a dal�� v Tagu RSAKeyValue
            /// </summary>
            public void ExportToXmlFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToXml());
                sw.Close();
            }


            public static string FromFile(string p)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        /// <summary>
        /// Instantiates a new asymmetric encryption session using the default key size; 
        /// this is usally 1024 bits
        /// Vytvo��m obejkt _rsa se kter�m budu prov�d�t �ifrovac� operace
        /// </summary>
        public Asymmetric()
        {
            _rsa = GetRSAProvider();
        }

        /// <summary>
        /// Instantiates a new asymmetric encryption session using a specific key size
        /// OOP A1 _KeySize a do _rsa vlo��m provider M GetRSAProvider. Vytvo��m instance pro novou asymetrickou krypt. session s velikost� kl��e A1
        /// </summary>
        public Asymmetric(int keySize)
        {
            _KeySize = keySize;
            _rsa = GetRSAProvider();
        }

        /// <summary>
        /// Sets the name of the key container used to store this 
        /// key on disk; this is an 
        /// unavoidable side effect of the underlying 
        /// Microsoft CryptoAPI. 
        /// Nastav� jm�no kontejneru na kl�� u��van�ho k uchov�n� tohoto kl��e na disku.
        /// Toto je vedlej�� efekt n�zko�rov�ov� Microsoft CryptoAPI
        /// </summary>
        /// <remarks>
        /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&amp;NoWebContent=1
        /// </remarks>
        public string KeyContainerName
        {
            get { return _KeyContainerName; }
            set { _KeyContainerName = value; }
        }

        /// <summary>
        /// Returns the current key size, in bits
        /// G akt. velikost kl��e
        /// </summary>
        public int KeySizeBits
        {
            get { return _rsa.KeySize; }
        }

        /// <summary>
        /// Returns the maximum supported key size, in bits
        /// Vr�t�m max. velikost kl��e v bitech dle _rsa.LegalKeySizes[0]
        /// </summary>
        public int KeySizeMaxBits
        {
            get { return _rsa.LegalKeySizes[0].MaxSize; }
        }

        /// <summary>
        /// Returns the minimum supported key size, in bits
        /// Vr�t�m min. velikost kl��e v bitech dle _rsa.LegalKeySizes[0]
        /// </summary>
        public int KeySizeMinBits
        {
            get { return _rsa.LegalKeySizes[0].MinSize; }
        }

        /// <summary>
        /// Returns valid key step sizes, in bits
        /// Vr�t�m  velikost kroku v bitech dle _rsa.LegalKeySizes[0]
        /// </summary>
        public int KeySizeStepBits
        {
            get { return _rsa.LegalKeySizes[0].SkipSize; }
        }

        /// <summary>
        /// Returns the default public key as stored in the *.config file
        /// Vr�t�m PublicKey z CM.AS a G
        /// </summary>
        public PublicKey DefaultPublicKey
        {
            get
            {
                PublicKey pubkey = new PublicKey();
                pubkey.LoadFromConfig();
                return pubkey;
            }
        }

        /// <summary>
        /// Returns the default private key as stored in the *.config file
        /// Vr�t�m PrivateKey z CM.AS a G
        /// </summary>
        public PrivateKey DefaultPrivateKey
        {
            get
            {
                PrivateKey privkey = new PrivateKey();
                privkey.LoadFromConfig();
                return privkey;
            }
        }

        /// <summary>
        /// Generates a new public/private key pair as objects
        /// VO RSA a vlo��m do As ve�ejn� a p�iv�tn� kl��, kter� vygeneruji v t�to t��d�.
        /// Vlo��m do typovan�ch objekt�
        /// </summary>
        public void GenerateNewKeyset(ref PublicKey publicKey, ref PrivateKey privateKey)
        {
            string PublicKeyXML = null;
            string PrivateKeyXML = null;
            GenerateNewKeyset(ref PublicKeyXML, ref PrivateKeyXML);
            publicKey = new PublicKey(PublicKeyXML);
            privateKey = new PrivateKey(PrivateKeyXML);
        }

        /// <summary>
        /// Generates a new public/private key pair as XML strings
        /// VO RSA a vlo��m do As ve�ejn� a p�iv�tn� kl��, kter� vygeneruji v t�to t��d�.
        /// </summary>
        public void GenerateNewKeyset(ref string publicKeyXML, ref string privateKeyXML)
        {
            RSA rsa = RSACryptoServiceProvider.Create();
            publicKeyXML = rsa.ToXmlString(false);
            privateKeyXML = rsa.ToXmlString(true);
        }

        /// <summary>
        /// Encrypts data using the default public key
        /// Zakryptuje A1 kl��em v DefaultPublicKey 
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d)
        {
            PublicKey PublicKey = DefaultPublicKey;
            return Encrypt(d, PublicKey);
        }

        /// <summary>
        /// Encrypts data using the provided public key
        /// P�evede A2 na parametr, kter� vlo�� do _rsa a za�ifruje A2.
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d, PublicKey publicKey)
        {
            _rsa.ImportParameters(publicKey.ToParameters());
            return EncryptPrivate(d);
        }

        /// <summary>
        /// Encrypts data using the provided public key as XML
        /// Na�te z xml A2 kl�� a za�ifruje A1
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d, string publicKeyXML)
        {
            LoadKeyXml(publicKeyXML, false);
            return EncryptPrivate(d);
        }

        /// <summary>
        /// Dekryptuje A1, VV p�i nezdaru.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private DataCrypt EncryptPrivate(DataCrypt d)
        {
            try
            {
                return new DataCrypt(_rsa.Encrypt(d.Bytes, false));
            }
            catch (CryptographicException ex)
            {
                if (ex.Message.ToLower().IndexOf("bad length") > -1)
                {
                    throw new CryptographicException("Your data is too large; RSA encryption is designed to encrypt relatively small amounts of data. The exact byte limit depends on the key size. To encrypt more data, use symmetric encryption and then encrypt that symmetric key with asymmetric RSA encryption.", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Decrypts data using the default private key
        /// Na�te kl�� z CM.AS a dekryptuje A1 s t�mto kl��em.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
        {
            PrivateKey PrivateKey = new PrivateKey();
            PrivateKey.LoadFromConfig();
            return Decrypt(encryptedDataCrypt, PrivateKey);
        }

        /// <summary>
        /// Decrypts data using the provided private key
        /// Importuji kl�� A2 jako parametr do _rsa
        /// Dekryptuje A1.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, PrivateKey PrivateKey)
        {
            _rsa.ImportParameters(PrivateKey.ToParameters());
            return DecryptPrivate(encryptedDataCrypt);
        }

        /// <summary>
        /// Decrypts data using the provided private key as XML
        /// Na�te kl�� z xml A2 - pou��v� intern� .net metodu.
        /// Dekryptuje data A1.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, string PrivateKeyXML)
        {
            LoadKeyXml(PrivateKeyXML, true);
            return DecryptPrivate(encryptedDataCrypt);
        }

        /// <summary>
        /// Na�tu do O _rsa ze XML A1 net metodou. A2 slou�� k tomu aby se vypsalo ve v�jimce jak� kl�� se nezda�iloi na��st. 
        /// </summary>
        /// <param name="keyXml"></param>
        /// <param name="isPrivate"></param>
        private void LoadKeyXml(string keyXml, bool isPrivate)
        {
            try
            {
                _rsa.FromXmlString(keyXml);
            }
            catch (XmlSyntaxException ex)
            {
                string s = null;
                if (isPrivate)
                {
                    s = "private";
                }
                else
                {
                    s = "public";
                }
                throw new System.Security.XmlSyntaxException(string.Format("The provided {0} encryption key XML does not appear to be valid.", s), ex);
            }
        }

        /// <summary>
        /// Dekryptuje data v A1 pomoc� RSA
        /// </summary>
        /// <param name="encryptedDataCrypt"></param>
        /// <returns></returns>
        private DataCrypt DecryptPrivate(DataCrypt encryptedDataCrypt)
        {
            return new DataCrypt(_rsa.Decrypt(encryptedDataCrypt.Bytes, false));
        }

        /// <summary>
        /// gets the default RSA provider using the specified key size; 
        /// note that Microsoft's CryptoAPI has an underlying file system dependency that is unavoidable
        /// Inicializuji krypt. t��du RSA s PP a  _KeySize a _KeyContainerName
        /// Kl�� se bude uchov�vat v ulo�i�ti kl��� PC, nikoliv v u�. profilu
        /// Pokud se nepoda�� na��st, VV
        /// </summary>
        /// <remarks>
        /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&amp;NoWebContent=1
        /// </remarks>
        private RSACryptoServiceProvider GetRSAProvider()
        {
            RSACryptoServiceProvider rsa = null;
            CspParameters csp = null;
            try
            {
                csp = new CspParameters();
                csp.KeyContainerName = _KeyContainerName;
                rsa = new RSACryptoServiceProvider(_KeySize, csp);
                rsa.PersistKeyInCsp = false;
                // Kl�� se bude uchov�vat v ulo�i�ti kl��� PC, nikoliv v u�. profilu
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                return rsa;
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                if (ex.Message.ToLower().IndexOf("csp for this implementation could not be acquired") > -1)
                {
                    throw new Exception("Unable to obtain Cryptographic Service Provider. " + "Either the permissions are incorrect on the " + "'C:\\Documents and Settings\\All Users\\Application DataCrypt\\Microsoft\\Crypto\\RSA\\MachineKeys' " + "folder, or the current security context '" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "'" + " does not have access to this folder.", ex);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if ((rsa != null))
                {
                    rsa = null;
                }
                if ((csp != null))
                {
                    csp = null;
                }
            }
        }

    }

    #endregion

    #region "  DataCrypt"

    /// <summary>
    /// represents Hex, Byte, Base64, or String data to encrypt/decrypt;
    /// use the .Text property to set/get a string representation 
    /// use the .Hex property to set/get a string-based Hexadecimal representation 
    /// use the .Base64 to set/get a string-based Base64 representation 
    /// T��da kter� uchov�v� bajty a p�ev�d� je mezi r�zn�mi form�ty.
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

    #endregion

    #region "  Utils"

    /// <summary>
    /// Friend class for shared utility methods used by multiple Encryption classes
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// converts an array of bytes to a string Hex representation
        /// P�evedu pole byt� A1 na hexadecim�ln� �et�zec.
        /// </summary>
        static public string ToHex(byte[] ba)
        {
            if (ba == null || ba.Length == 0)
            {
                return "";
            }
            const string HexFormat = "{0:X2}";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(string.Format(HexFormat, b));
            }
            return sb.ToString();
        }

        /// <summary>
        /// converts from a string Hex representation to an array of bytes
        /// P�evedu �et�zec v hexadexim�ln� form�tu A1 na pole byt�. Pokud nebude hex form�t(nap��kal nebude m�t sud� po�et znak�), VV
        /// </summary>
        static public byte[] FromHex(string hexEncoded)
        {
            if (hexEncoded == null || hexEncoded.Length == 0)
            {
                return null;
            }
            try
            {
                int l = Convert.ToInt32(hexEncoded.Length / 2);
                byte[] b = new byte[l];
                for (int i = 0; i <= l - 1; i++)
                {
                    b[i] = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16);
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new System.FormatException("The provided string does not appear to be Hex encoded:" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from a string Base64 representation to an array of bytes
        /// pokud je A1 null/L0, GN. Jinak se pokus�m p�ev�st na pole byt�-pokud A1 nebbude Base64 �et�zec, VV
        /// </summary>
        static public byte[] FromBase64(string base64Encoded)
        {
            if (base64Encoded == null || base64Encoded.Length == 0)
            {
                return null;
            }
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (System.FormatException ex)
            {
                throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from an array of bytes to a string Base64 representation
        /// Pokud A1 null nebo L0, G SE. Jinak mi p�evede na Base64
        /// </summary>
        static public string ToBase64(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return "";
            }
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// retrieve an element from an XML string
        /// V XML A1 najde p�rov� prvek A2 a vr�t� jeho obsah. Pokud nenajde, VV.
        /// </summary>
        static public string GetXmlElement(string xml, string element)
        {
            Match m = null;
            m = Regex.Match(xml, "<" + element + ">(?<Element>[^>]*)</" + element + ">", RegexOptions.IgnoreCase);
            if (m == null)
            {
                throw new Exception("Could not find <" + element + "></" + element + "> in provided Public Key XML.");
            }
            return m.Groups["Element"].ToString();
        }

        /// <summary>
        /// Returns the specified string value from the application .config file
        /// G �et�zec z ConfigurationManager.AppSettings kl��e A1. Pokud se nepoda�� z�skat a A2, VV
        /// </summary>
        static public string GetConfigString(string key, bool isRequired)
        {

            string s = Convert.ToString(ConfigurationManager.AppSettings.Get(key));
            if (s == null)
            {
                if (isRequired)
                {
                    throw new ConfigurationErrorsException("key <" + key + "> is missing from .config file");
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// Vr�t� mi �et�zec <add key=\"A1\" value=\"A2\" />
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string WriteConfigKey(string key, string value)
        {
            string s = "<add key=\"{0}\" value=\"{1}\" />" + Environment.NewLine;
            return string.Format(s, key, value);
        }

        /// <summary>
        /// G element A1 s hodnotou A2
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string WriteXmlElement(string element, string value)
        {
            string s = "<{0}>{1}</{0}>" + Environment.NewLine;
            return string.Format(s, element, value);
        }

        /// <summary>
        /// Pokud A2, vr�t� mi ukon. tag A1, jinak po�. tag A1.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="isClosing"></param>
        /// <returns></returns>
        static public string WriteXmlNode(string element, bool isClosing)
        {
            string s = null;
            if (isClosing)
            {
                s = "</{0}>" + Environment.NewLine;
            }
            else
            {
                s = "<{0}>" + Environment.NewLine;
            }
            return string.Format(s, element);
        }

    }

    #endregion

}
