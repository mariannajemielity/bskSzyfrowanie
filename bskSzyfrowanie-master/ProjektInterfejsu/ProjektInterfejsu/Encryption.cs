using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using System.Xml.Serialization;

namespace ProjektInterfejsu
{
   public class Encryption
    {
        private static int saltLengthLimit = 32;
        private static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static byte[] GenerateXmlHeader(List<Recipient> recipients,string keySize, string cipherMode, string iv)
        {
            XmlMetadata metadata = new XmlMetadata()
            {
                Algorithm = "AES",
                CipherMode = cipherMode,
                IV = iv,
                KeySize = keySize,
                Recipients = recipients
            };

            byte[] result;
            XmlSerializer serializer = new XmlSerializer(typeof(XmlMetadata));
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, metadata);
                result = ms.ToArray();
            }
            return result;
        }
        public static void GenerateSymKey(int keyLength)
        {
            var sec = new SecureRandom();
            sec.SetSeed(DateTime.Now.ToBinary());
            var keyParam = new KeyGenerationParameters(sec, keyLength);
            var keyGenerator = new CipherKeyGenerator();
            keyGenerator.Init(keyParam);
            symKey = keyGenerator.GenerateKey();
        }
        public static void AESEncrypt(string inputFile, string outputFile, CipherMode cipherMode, int keySize, byte[] passwordBytes)
        {
            byte[] saltBytes = GetSalt();
            string cryptFile = outputFile;
            FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

            RijndaelManaged AES = new RijndaelManaged();

            AES.KeySize = keySize;
            AES.BlockSize = 128;


            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.Zeros;

            AES.Mode = cipherMode;

            CryptoStream cs = new CryptoStream(fsCrypt,
                 AES.CreateEncryptor(),
                CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            int data;
            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);


            fsIn.Close();
            cs.Close();
            fsCrypt.Close();

        }

        public static void AESDecrypt(string inputFile, string outputFile, CipherMode cipherMode,int keySize, byte[] passwordBytes)
        {

            byte[] saltBytes = GetSalt();
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged AES = new RijndaelManaged();

            AES.KeySize = keySize;
            AES.BlockSize = 128;


            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.Zeros;

            AES.Mode = cipherMode;

            CryptoStream cs = new CryptoStream(fsCrypt,
                AES.CreateDecryptor(),
                CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();

        }
    }
}

