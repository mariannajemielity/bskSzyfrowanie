using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjektInterfejsu
{
   public class XmlMetadata
    {
        public string Algorithm { get; set; }

        public string KeySize { get; set; }

        public int BlockSize { get; set; }

        public string CipherMode { get; set; }

        public string IV { get; set; }

        public List<Recipient> Recipients { get; set; }
    }
}
