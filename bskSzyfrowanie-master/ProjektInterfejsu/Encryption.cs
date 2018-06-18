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
using System.Windows.Threading;

namespace ProjektInterfejsu
{
   public class Encryption
    {
        private static int saltLengthLimit = 32;
        public static byte[] GetSalt()
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
                BlockSize = 128,
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
        public static byte[] GenerateSymKey(int keyLength)
        {
            var sec = new SecureRandom();
            sec.SetSeed(DateTime.Now.ToBinary());
            var keyParam = new KeyGenerationParameters(sec, keyLength);
            var keyGenerator = new CipherKeyGenerator();
            keyGenerator.Init(keyParam);
           var symKey = keyGenerator.GenerateKey();
            return symKey;
        }
        

        
    }
}

