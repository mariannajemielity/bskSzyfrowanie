using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInterfejsu
{
    static class PGPKey
    {
        public static PgpPublicKey ImportPublicKey(this Stream publicIn)
        {
            var pubRings =
                new PgpPublicKeyRingBundle(PgpUtilities.GetDecoderStream(publicIn)).GetKeyRings().OfType<PgpPublicKeyRing>();
            var pubKeys = pubRings.SelectMany(x => x.GetPublicKeys().OfType<PgpPublicKey>());
            var pubKey = pubKeys.FirstOrDefault();
            return pubKey;
        }

        public static PgpSecretKey ImportSecretKey(this Stream secretIn)
        {
            var secRings =
                new PgpSecretKeyRingBundle(PgpUtilities.GetDecoderStream(secretIn)).GetKeyRings().OfType<PgpSecretKeyRing>();
            var secKeys = secRings.SelectMany(x => x.GetSecretKeys().OfType<PgpSecretKey>());
            var secKey = secKeys.FirstOrDefault();
            return secKey;
        }
    }
}
