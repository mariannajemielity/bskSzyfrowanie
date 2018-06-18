using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjektInterfejsu
{
    [Serializable]
    public class Recipient
    {
        public string Name { get; set; }

        [XmlIgnore]
        public AsymmetricKeyParameter PubKey { get; set; }

        public byte[] EncryptedSymKey { get; set; }

        public Recipient(string name, AsymmetricKeyParameter key)
        {
            Name = name;
            PubKey = key;
        }

        public Recipient() { }
    }
}
