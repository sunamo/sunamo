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
        /// Provider sifrovani RSA
        /// </summary>
        private RSACryptoServiceProvider _rsa;
        /// <summary>
        /// Vychozy jmeno kontejneru, ve kterem se bude uchovavat klic.
        /// </summary>
        private string _KeyContainerName = "Encryption.AsymmetricEncryption.DefaultContainerName";
        /// <summary>
        /// Vychozi velikost klice v bytech.
        /// </summary>
        private int _KeySize = 1024;
        #region Nazvy elementu pro ukladani do XML
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
        #region Nazvy elementu pro ukladani do CM.AS
        private const string _KeyModulus = "PublicKey.Modulus";
        private const string _KeyExponent = "PublicKey.Exponent";
        private const string _KeyPrimeP = "PrivateKey.P";
        private const string _KeyPrimeQ = "PrivateKey.Q";
        private const string _KeyPrimeExponentP = "PrivateKey.DP";
        private const string _KeyPrimeExponentQ = "PrivateKey.DQ";
        private const string _KeyCoefficient = "PrivateKey.InverseQ";

        private const string _KeyPrivateExponent = "PrivateKey.D";
        #endregion

        #region "  " + "PublicKey class"
        /// <summary>
        /// Represents a public encryption key. Intended to be shared, it 
        /// contains only the Modulus and Exponent.
        /// Ttrda verejneho klice. Ma metody pro nacteni a ulozeni z/do ruznych zdroju.
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
            /// EK. Nactu z XML A1 obsahy tagu Modulus a Exponent a ulozim je do stejne pojm. VV
            /// </summary>
            /// <param name="KeyXml"></param>
            public PublicKey(string KeyXml)
            {
                LoadFromXml(KeyXml);
            }

            /// <summary>
            /// Load public key from App.config or Web.config file
            /// Ulozim do PP z CM.AS
            /// </summary>
            public void LoadFromConfig()
            {
                this.Modulus = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyModulus, true);
                this.Exponent = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyExponent, true);
            }

            /// <summary>
            /// Returns *.config file XML section representing this public key
            /// Vratim 2x tax Add s argumenty PP Modulus a Exponent
            /// </summary>
            public string ToConfigSection()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nevim zda bych nemel vytvorit novou instanci SB
                StringBuilder _with1 = sb;
                _with1.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyModulus, this.Modulus));
                _with1.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyExponent, this.Exponent));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the *.config file representation of this public key to a file
            /// Prepnu A1 2x tagem Add s argumenty PP Modulus a Exponent
            /// </summary>
            public void ExportToConfigFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToConfigSection());
                sw.Close();
            }

            /// <summary>
            /// Loads the public key from its XML string
            /// Nactu z XML A1 obsahy tagu Modulus a Exponent a ulozim je do stejne pojm. VV
            /// </summary>
            public void LoadFromXml(string keyXml)
            {
                this.Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, "Modulus");
                this.Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, "Exponent");
            }

            /// <summary>
            /// Converts this public key to an RSAParameters object
            /// Vrati mi pp Modulus a Exponent v O RSAParameters
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
            /// Vrati mi Tagy PP Modulus a Exponent v Tagu RSAKeyValue
            /// </summary>
            public string ToXml()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nevim zda bych nemel vytvoiit novou instanci SB
                StringBuilder _with2 = sb;
                // Mohl bych to zapsat pomoci T RSAParameters ale nevim jak by se to vyporadalo s verejnym klicem.
                _with2.Append(UtilsNonNetStandard.WriteXmlNode(Asymmetric._ElementParent, false));
                _with2.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementModulus, this.Modulus));
                _with2.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementExponent, this.Exponent));
                _with2.Append(UtilsNonNetStandard.WriteXmlNode(Asymmetric._ElementParent, true));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the Xml representation of this public key to a file
            /// Prepne A1 Tagy PP Modulus a Exponent v Tagu RSAKeyValue
            /// </summary>
            public void ExportToXmlFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToXml());
                sw.Close();
            }

        }
        #endregion

        #region "  " + "PrivateKey class"

        /// <summary>
        /// Represents a private encryption key. Not intended to be shared, as it 
        /// contains all the elements that make up the key.
        /// </summary>
        public class PrivateKey
        {
            #region Prevedou se na base64 a vlozi se do objektu RSAParameters
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
            /// Nactu z XML A1 obsahy tagu Modulus a Exponent a dalsi tagy a ulozim je do stejne pojm. VV
            /// </summary>
            /// <param name="keyXml"></param>
            public PrivateKey(string keyXml)
            {
                LoadFromXml(keyXml);
            }

            /// <summary>
            /// Load private key from App.config or Web.config file
            /// Ulozim do PPs z CM.AS
            /// </summary>
            public void LoadFromConfig()
            {
                this.Modulus = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyModulus, true);
                this.Exponent = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyExponent, true);
                this.PrimeP = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyPrimeP, true);
                this.PrimeQ = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyPrimeQ, true);
                this.PrimeExponentP = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyPrimeExponentP, true);
                this.PrimeExponentQ = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyPrimeExponentQ, true);
                this.Coefficient = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyCoefficient, true);
                this.PrivateExponent = UtilsNonNetStandard.GetConfigString(Asymmetric._KeyPrivateExponent, true);
            }

            /// <summary>
            /// Converts this private key to an RSAParameters object
            /// Prevedu PPs z Base64 a vlozim do O RSAParameter, ktere G 
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
            /// Vratim xx tax Add s argumenty PP Modulus a Exponent
            /// </summary>
            public string ToConfigSection()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nevim zda bych nemel vytvorit novou instanci SB
                StringBuilder _with3 = sb;
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyModulus, this.Modulus));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyExponent, this.Exponent));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyPrimeP, this.PrimeP));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyPrimeQ, this.PrimeQ));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyPrimeExponentP, this.PrimeExponentP));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyPrimeExponentQ, this.PrimeExponentQ));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyCoefficient, this.Coefficient));
                _with3.Append(UtilsNonNetStandard.WriteConfigKey(Asymmetric._KeyPrivateExponent, this.PrivateExponent));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the *.config file representation of this private key to a file
            /// Prepnu A1 2x tagem Add s argumenty PP Modulus a Exponent a dalsi
            /// </summary>
            public void ExportToConfigFile(string strFilePath)
            {
                StreamWriter sw = new StreamWriter(strFilePath, false);
                sw.Write(this.ToConfigSection());
                sw.Close();
            }

            /// <summary>
            /// Loads the private key from its XML string
            /// Nactu z XML A1 obsahy tagu Modulus a Exponent a dalsi tagy a ulozim je do stejne pojm. VV
            /// </summary>
            public void LoadFromXml(string keyXml)
            {
                this.Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, "Modulus");
                this.Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, "Exponent");
                this.PrimeP = UtilsNonNetStandard.GetXmlElement(keyXml, "P");
                this.PrimeQ = UtilsNonNetStandard.GetXmlElement(keyXml, "Q");
                this.PrimeExponentP = UtilsNonNetStandard.GetXmlElement(keyXml, "DP");
                this.PrimeExponentQ = UtilsNonNetStandard.GetXmlElement(keyXml, "DQ");
                this.Coefficient = UtilsNonNetStandard.GetXmlElement(keyXml, "InverseQ");
                this.PrivateExponent = UtilsNonNetStandard.GetXmlElement(keyXml, "D");
            }

            /// <summary>
            /// Converts this private key to its XML string representation
            /// Vrati mi Tagy PP Modulus a Exponent a dalsi v Tagu RSAKeyValue
            /// </summary>
            public string ToXml()
            {
                StringBuilder sb = new StringBuilder();
                // TODO: Nevim zda bych nemel vytvorit novou instanci SB
                StringBuilder _with4 = sb;
                _with4.Append(UtilsNonNetStandard.WriteXmlNode(Asymmetric._ElementParent, false));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementModulus, this.Modulus));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementExponent, this.Exponent));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementPrimeP, this.PrimeP));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementPrimeQ, this.PrimeQ));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementPrimeExponentP, this.PrimeExponentP));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementPrimeExponentQ, this.PrimeExponentQ));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementCoefficient, this.Coefficient));
                _with4.Append(UtilsNonNetStandard.WriteXmlElement(Asymmetric._ElementPrivateExponent, this.PrivateExponent));
                _with4.Append(UtilsNonNetStandard.WriteXmlNode(Asymmetric._ElementParent, true));
                return sb.ToString();
            }

            /// <summary>
            /// Writes the Xml representation of this private key to a file
            /// Prepte A1 Tagy PP Modulus a Exponent a dalsi v Tagu RSAKeyValue
            /// </summary>
            public void ExportToXmlFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToXml());
                sw.Close();
            }


            public static string FromFile(string p)
            {
                throw new Exception("The method or operation is not implemented" + ".");
            }
        }

        #endregion

        /// <summary>
        /// Instantiates a new asymmetric encryption session using the default key size; 
        /// this is usally 1024 bits
        /// Vytvorim obejkt _rsa se kterem budu provadet sifrovaci operace
        /// </summary>
        public Asymmetric()
        {
            _rsa = GetRSAProvider();
        }

        /// <summary>
        /// Instantiates a new asymmetric encryption session using a specific key size
        /// OOP A1 _KeySize a do _rsa vlozim provider M GetRSAProvider. Vytvorim instance pro novou asymetrickou krypt. session s velikostm klice A1
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
        /// Nastavi jmeno kontejneru na klic ussvansho k uchovani tohoto klice na disku.
        /// Toto je vedlejsi efekt nizkourovnove Microsoft CryptoAPI
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
        /// G akt. velikost klice
        /// </summary>
        public int KeySizeBits
        {
            get { return _rsa.KeySize; }
        }

        /// <summary>
        /// Returns the maximum supported key size, in bits
        /// Vratim max. velikost klice v bitech dle _rsa.LegalKeySizes[0]
        /// </summary>
        public int KeySizeMaxBits
        {
            get { return _rsa.LegalKeySizes[0].MaxSize; }
        }

        /// <summary>
        /// Returns the minimum supported key size, in bits
        /// Vratim min. velikost klice v bitech dle _rsa.LegalKeySizes[0]
        /// </summary>
        public int KeySizeMinBits
        {
            get { return _rsa.LegalKeySizes[0].MinSize; }
        }

        /// <summary>
        /// Returns valid key step sizes, in bits
        /// Vratim  velikost kroku v bitech dle _rsa.LegalKeySizes[0]
        /// </summary>
        public int KeySizeStepBits
        {
            get { return _rsa.LegalKeySizes[0].SkipSize; }
        }

        /// <summary>
        /// Returns the default public key as stored in the *.config file
        /// Vratim PublicKey z CM.AS a G
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
        /// Vratim PrivateKey z CM.AS a G
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
        /// VO RSA a vlozim do As verejne a privitni klic, ktere vygeneruji v teto ttide.
        /// Vlozim do typovanych objektu
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
        /// VO RSA a vlozim do As veeejne a privatni klic, ktery vygeneruji v teto tride.
        /// </summary>
        public void GenerateNewKeyset(ref string publicKeyXML, ref string privateKeyXML)
        {
            RSA rsa = RSACryptoServiceProvider.Create();
            publicKeyXML = rsa.ToXmlString(false);
            privateKeyXML = rsa.ToXmlString(true);
        }

        /// <summary>
        /// Encrypts data using the default public key
        /// Zakryptuje A1 klicem v DefaultPublicKey 
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d)
        {
            PublicKey PublicKey = DefaultPublicKey;
            return Encrypt(d, PublicKey);
        }

        /// <summary>
        /// Encrypts data using the provided public key
        /// Prevede A2 na parametr, ktery vlozi do _rsa a zasifruje A2.
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d, PublicKey publicKey)
        {
            _rsa.ImportParameters(publicKey.ToParameters());
            return EncryptPrivate(d);
        }

        /// <summary>
        /// Encrypts data using the provided public key as XML
        /// Nacte z xml A2 klic a zasifruje A1
        /// </summary>
        public DataCrypt Encrypt(DataCrypt d, string publicKeyXML)
        {
            LoadKeyXml(publicKeyXML, false);
            return EncryptPrivate(d);
        }

        /// <summary>
        /// Dekryptuje A1, VV pri nezdaru.
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
                    throw new CryptographicException("Your data is too large; RSA encryption is designed to encrypt relatively small amounts of data. The exact byte limit depends on the key size. To encrypt more data, use symmetric encryption and then encrypt that symmetric key with asymmetric RSA encryption" + ".", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Decrypts data using the default private key
        /// Nacte klic z CM.AS a dekryptuje A1 s timto klicem.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
        {
            PrivateKey PrivateKey = new PrivateKey();
            PrivateKey.LoadFromConfig();
            return Decrypt(encryptedDataCrypt, PrivateKey);
        }

        /// <summary>
        /// Decrypts data using the provided private key
        /// Importuji klic A2 jako parametr do _rsa
        /// Dekryptuje A1.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, PrivateKey PrivateKey)
        {
            _rsa.ImportParameters(PrivateKey.ToParameters());
            return DecryptPrivate(encryptedDataCrypt);
        }

        /// <summary>
        /// Decrypts data using the provided private key as XML
        /// Nacte klic z xml A2 - pouziva interni .net metodu.
        /// Dekryptuje data A1.
        /// </summary>
        public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, string PrivateKeyXML)
        {
            LoadKeyXml(PrivateKeyXML, true);
            return DecryptPrivate(encryptedDataCrypt);
        }

        /// <summary>
        /// Nactu do O _rsa ze XML A1 net metodou. A2 slouzi k tomu aby se vypsalo ve vyjimce jaky klic se nezdariloi nacist. 
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
                throw new System.Security.XmlSyntaxException(SH.Format2("The provided {0} encryption key XML does not appear to be valid.", s), ex);
            }
        }

        /// <summary>
        /// Dekryptuje data v A1 pomoci RSA
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
        /// Inicializuji krypt. ttidu RSA s PP a  _KeySize a _KeyContainerName
        /// Klic se bude uchovavat v ulozisti klice PC, nikoliv v uz. profilu
        /// Pokud se nepodari nacist, VV
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
                // Klic se bude uchovavat v ulozisti klice PC, nikoliv v uc. profilu
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                return rsa;
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                if (ex.Message.ToLower().IndexOf("csp for this implementation could not be acquired") > -1)
                {
                    throw new Exception("Unable to obtain Cryptographic Service Provider" + ". " + "Either the permissions are incorrect on the" + " " + "'C:\\Documents and Settings\\All Users\\Application DataCrypt\\Microsoft\\Crypto\\RSA\\MachineKeys' " + "folder, or the current security context" + " '" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "'" + " " + "does not have access to this folder" + ".", ex);
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
}
