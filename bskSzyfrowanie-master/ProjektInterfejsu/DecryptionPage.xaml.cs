using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using ProjektInterfejsu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ProjektInterfejsu
{
    /// <summary>
    /// Interaction logic for DecryptionPage.xaml
    /// </summary>
    public partial class DecryptionPage : Page
    {
        private Stream inStr = null;
        private Stream outStr = null;
        private string inputKeyFile;
        private string inputFileDecryptPath;
        private string outputFileDecryptPath;
        private byte[] symKey;
        private XmlMetadata metadataDecrypt;
        Stopwatch watch;
        public DecryptionPage()
        {
            InitializeComponent();
           // DecryptBtn.IsEnabled = false;
        }

        private void btnInputFileDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog chooseFileToDecryptFromDialog = new OpenFileDialog();
                chooseFileToDecryptFromDialog.Title = "Wybór pliku do odszyfrowania";
              //  chooseFileToDecryptFromDialog.InitialDirectory = @"C:\";
                chooseFileToDecryptFromDialog.CheckFileExists = true;
                chooseFileToDecryptFromDialog.CheckPathExists = true;

                if (chooseFileToDecryptFromDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxDecryptInputFile.Text = chooseFileToDecryptFromDialog.FileName;
                    inputFileDecryptPath = textBoxDecryptInputFile.Text;
                }

                if (inputFileDecryptPath != null)
                {
                    inStr = File.OpenRead(inputFileDecryptPath);

                    //odczytanie naglowka
                    using (var ms = new MemoryStream())
                    {
                        byte[] buff = new byte[1];
                        while (true)
                        {
                            inStr.Read(buff, 0, 1);

                            if (buff[0] == (byte)3)
                                break;

                            ms.Write(buff, 0, 1);
                        }

                        ms.Position = 0;
                        XmlSerializer ser = new XmlSerializer(typeof(XmlMetadata));
                        metadataDecrypt = (XmlMetadata)ser.Deserialize(ms);
                    }

                    ComboBoxRecipient.ItemsSource = metadataDecrypt.Recipients;
                }
            }
            catch
            {
                textBoxDecryptInputFile.Text = "";
                // errorMsg = "niepoprawny plik";
            }
        }
        public void AESDecrypt(Stream fsCrypt, Stream fsOut, CipherMode cipherMode, int keySize, byte[] iv, byte[] passwordBytes)
        {

            // byte[] saltBytes = GetSalt();
            // FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged AES = new RijndaelManaged();

            AES.KeySize = keySize;
            AES.BlockSize = 128;

            var key = new Rfc2898DeriveBytes(passwordBytes, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 1000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = iv;
            AES.Padding = PaddingMode.Zeros;

            AES.Mode = cipherMode;

            CryptoStream cs = new CryptoStream(fsCrypt,
                AES.CreateDecryptor(),
                CryptoStreamMode.Read);

            //  FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            byte[] inblock = new byte[8];
            byte[] outblock = null;
            int counter = 0;
            while (cs.Read(inblock,0,inblock.Length) > 0)
            {
                counter += 1;
                fsOut.Write(inblock,0,inblock.Length);
                if (counter % 200 == 0)
                {
                    var prog = fsCrypt.Position * 100 / fsCrypt.Length;
                    Dispatcher.Invoke((Action)(() => ProgBarDecrypt.Value = prog));
                }
            }
            Dispatcher.Invoke((Action)(() => ProgBarDecrypt.Value = 100));
            fsOut.Close();
            cs.Close();
            fsCrypt.Close();
            watch.Stop();

        }
        private void BtnOutputFileDecrypt_Click(object sender, RoutedEventArgs e)
        {

         

        }
       async private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            watch = Stopwatch.StartNew();
            var who = ComboBoxRecipient.SelectedItem as Recipient;

            if (who != null)
            {
                inputKeyFile = @".\prywatne\" + who.Name;
            }
            outStr = File.Create(outputFileDecryptPath);

            var receiver = ComboBoxRecipient.SelectedItem as Recipient;
            symKey = RSADecrypt(receiver.EncryptedSymKey);
            var iv = new byte[(metadataDecrypt.BlockSize / 8)];
            iv = Convert.FromBase64String(metadataDecrypt.IV);
            var enumValue = Enum.Parse(typeof(CipherMode), metadataDecrypt.CipherMode);
            await Task.Run(() => AESDecrypt(inStr, outStr, (CipherMode)enumValue, Int32.Parse(metadataDecrypt.KeySize), iv, symKey));
            var elapsedMs = watch.ElapsedMilliseconds;
            


            // Clear();
        }
        //private void BtnDecrypt_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (String.IsNullOrEmpty(textBoxDecryptInputFile.Text) || String.IsNullOrEmpty(textBoxDecryptOutputFile.Text)
        //        || String.IsNullOrEmpty(PasswordDecrypt.Password) || ComboBoxRecipient.ItemsSource == null)
        //        DecryptBtn.IsEnabled = false;
        //    else
        //        DecryptBtn.IsEnabled = true;
        //}
        //private void BtnDecrypt_Loaded(object sender, SelectionChangedEventArgs e)
        //{
        //    var who = ComboBoxRecipient.SelectedItem as Recipient;

        //    if (who != null)
        //    {
        //        inputKeyFile = @".\prywatne\" + who.Name;
        //    }
           
        //}

        private byte[] RSADecrypt(byte[] toDecrypt)
        {
            AsymmetricKeyParameter keyPriv;
            var bytesToDecrypt = toDecrypt;

            var sec = new SecureRandom();

            PgpSecretKey pgpPriv;
            using (Stream sr = File.OpenRead(inputKeyFile))
            {
                pgpPriv = PGPKey.ImportSecretKey(sr);
            }

            var pass = PasswordDecrypt.Password.ToCharArray();

            try
            {
                keyPriv = pgpPriv.ExtractPrivateKey(pass).Key;
            }
            catch
            {
                keyPriv = null;
            }

            if (keyPriv == null)
            {
                byte[] seed = Encoding.UTF8.GetBytes(pass);
                var sec2 = new SecureRandom(seed);

                var keyParam = new KeyGenerationParameters(sec2, Int32.Parse(metadataDecrypt.KeySize));
                var keyGenerator = new CipherKeyGenerator();
                keyGenerator.Init(keyParam);
                return keyGenerator.GenerateKey();
            }

            var decryptEngine = new Pkcs1Encoding(new RsaEngine());
            decryptEngine.Init(false, keyPriv);

            byte[] decrypted;

            try
            {
                decrypted = decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length);
            }
            catch
            {
                var keyParam = new KeyGenerationParameters(sec, Int32.Parse(metadataDecrypt.KeySize));
                var keyGenerator = new CipherKeyGenerator();
                keyGenerator.Init(keyParam);
                return keyGenerator.GenerateKey();
            }

            return decrypted;
        }

        private void textBoxDecryptOutputFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var chooseFileToDecryptToDialog = new SaveFileDialog();
            chooseFileToDecryptToDialog.Title = "Wybór pliku docelowego";
          //  chooseFileToDecryptToDialog.InitialDirectory = @"C:\";
            chooseFileToDecryptToDialog.CheckFileExists = false;
            chooseFileToDecryptToDialog.CheckPathExists = false;

            if (chooseFileToDecryptToDialog.ShowDialog() == DialogResult.OK)
            {
                outputFileDecryptPath = chooseFileToDecryptToDialog.FileName;
                textBoxDecryptOutputFile.Text = outputFileDecryptPath;
            }
            //  errorMsg = "";
        }
    }
}
