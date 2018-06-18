using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Bcpg;

namespace ProjektInterfejsu
{
    /// <summary>
    /// Interaction logic for KeyPage.xaml
    /// </summary>
    public partial class KeyPage : Page
    {
        private const int RsaKeySize = 2048;
        public KeyPage()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            passwordBoxPassword1.Password = String.Empty;
            passwordBoxPassword2.Password = String.Empty;
            textBoxUserName.Text = String.Empty;
        }

    
        public static AsymmetricCipherKeyPair GetKeyPair()
        {
            CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
            SecureRandom secureRandom = new SecureRandom(randomGenerator);
            var keyGenerationParameters = new KeyGenerationParameters(secureRandom, RsaKeySize);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            return keyPairGenerator.GenerateKeyPair();

        }

        private static void ExportKeyPair(
            Stream secretOut,
            Stream publicOut,
            AsymmetricKeyParameter publicKey,
            AsymmetricKeyParameter privateKey,
            string identity,
            char[] passPhrase,
            bool armor)
        {


            PgpSecretKey secretKey = new PgpSecretKey(
                PgpSignature.DefaultCertification,
                PublicKeyAlgorithmTag.RsaGeneral,
                publicKey,
                privateKey,
                DateTime.UtcNow,
                identity,
                SymmetricKeyAlgorithmTag.Aes128,
                passPhrase,
                null,
                null,
                new SecureRandom()
                );

            secretKey.Encode(secretOut);

            if (armor)
            {
                secretOut.Close();
                publicOut = new ArmoredOutputStream(publicOut);
            }

            PgpPublicKey key = secretKey.PublicKey;

            key.Encode(publicOut);

            if (armor)
            {
                publicOut.Close();
            }
        }


        //sprawdzenie czy podane przez użytkownika hasło spełnia wymagania
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            generateKeysButton.IsEnabled = false;
            //przynajmniej 8 znaków
            if (passwordBoxPassword1.Password.Length < 8)
                statusText.Text = "Hasło musi składać się z przynajmniej 8 znaków";
            //przynajmniej 1 litera          
            else if (!Regex.IsMatch(passwordBoxPassword1.Password, "[a-zA-Z]"))
                statusText.Text = "Hasło musi zawierać przynajmiej jedeną literę";
            //przynajmniej 1 cyfra
            else if (!Regex.IsMatch(passwordBoxPassword1.Password, "[0-9]"))
                statusText.Text = "Hasło musi zawierać przynajmiej jedną cyfrę";
            //przynajmniej 1 znak specjalny
            else if (Regex.IsMatch(passwordBoxPassword1.Password, "^[a-zA-Z0-9 ]*$"))
                statusText.Text = "Hasło musi zawierać przynajmiej jeden znak specjalny";
            else
            {
                statusText.Text = String.Empty;
                generateKeysButton.IsEnabled = true;
            }
        }

        //kliknięcie "Wygeneruj parę kluczy"
        private void GenerateKeysButton_Click(object sender, RoutedEventArgs e)
        {
            //sprawdzenie czy podane hasła są identyczne
            if (!passwordBoxPassword1.Password.Equals(passwordBoxPassword2.Password)) 
                statusText.Text = "Podane hasła różnią się";
            else
            {
                AsymmetricCipherKeyPair keyPair = GetKeyPair();
                var pass = passwordBoxPassword1.Password;
                var recip = textBoxUserName.Text;
                Directory.CreateDirectory(@".\prywatne\");
                Directory.CreateDirectory(@".\publiczne\");
                var out1 = File.Create(@".\prywatne\" + recip);
                var out2 = File.Create(@".\publiczne\" + recip);

                ExportKeyPair(out1, out2, keyPair.Public, keyPair.Private, recip, pass.ToCharArray(), true);

                out1.Close();
                out2.Close();

                Clear();
            }
                

        }
    }
}
