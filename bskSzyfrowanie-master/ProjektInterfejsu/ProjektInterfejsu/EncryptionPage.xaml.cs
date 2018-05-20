using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EncryptionPage.xaml
    /// </summary>
    public partial class EncryptionPage : Page
    {
        private string inputFilePath;
        private string outputFilePath;
        private PaddedBufferedBlockCipher cipher = null;
        private Stream inStr = null;
        private Stream outStr = null;
        private List<Recipient> recipients = new List<Recipient>();
        private List<Recipient> recipientsToSend = new List<Recipient>();
        private byte[] symKey;
        private byte[] iv;
        public EncryptionPage()
        {
            InitializeComponent();
            ComboBoxCipherMode.ItemsSource = Enum.GetValues(typeof(CipherMode)).Cast<CipherMode>().Take(4);
            ComboBoxKeyLength.ItemsSource = new List<int>{ 128,192,256};
        }
        private void LoadRecip(Recipient recip)
        {
            if (ListBoxRecipient.Items.Count == 0)
            {
                recipients.Add(recip);
                ListBoxRecipient.Items.Add(recip.Name);
            }
            else
            {
                var toAdd = true;
                foreach (var item in ListBoxRecipient.Items)
                {
                    if (recip.Name == item.ToString())
                        toAdd = false;
                }
                if (toAdd)
                {
                    recipients.Add(recip);
                    ListBoxRecipient.Items.Add(recip.Name);
                }
            }
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            var dir = new DirectoryInfo(@".\publiczne\");

            foreach (var file in dir.GetFiles())
            {
                var recip = new Recipient(System.IO.Path.GetFileNameWithoutExtension(file.FullName), readKey(file.FullName));

                LoadRecip(recip);
            }
        }

        private AsymmetricKeyParameter readKey(string file)
        {
            AsymmetricKeyParameter key;
            PgpPublicKey pgpPub;
            using (Stream sr = File.OpenRead(file))
            {
                pgpPub = PGPKey.ImportPublicKey(sr);
            }

            key = pgpPub.GetKey();

            return key;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseFileToEncryptDialog = new OpenFileDialog();
            chooseFileToEncryptDialog.Title = "Wybór pliku do zaszyfrowania";
            chooseFileToEncryptDialog.InitialDirectory = @"C:\";
            chooseFileToEncryptDialog.CheckFileExists = true;
            chooseFileToEncryptDialog.CheckPathExists = true;

            if (chooseFileToEncryptDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxChosenFileToEncFrom.Text = chooseFileToEncryptDialog.FileName;
            }

        }

        //
        private void Button_Output_Click(object sender, RoutedEventArgs e)
        {
            var chooseFileToEncryptToDialog = new SaveFileDialog();
            chooseFileToEncryptToDialog.Title = "Wybór pliku docelowego";
            chooseFileToEncryptToDialog.InitialDirectory = @"C:\";
             chooseFileToEncryptToDialog.CheckFileExists = false;
             chooseFileToEncryptToDialog.CheckPathExists = false;

            if (chooseFileToEncryptToDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxChosenFileToEncTo.Text = chooseFileToEncryptToDialog.FileName;
            }
        }

        private void EncryptBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = (ComboBoxItem)ComboBoxKeyLength.SelectedItem;
            int keyLength = int.Parse(item.Content.ToString());
            GenerateSymKey(keyLength);

            Stream inStr = File.OpenRead(inputFilePath);
            Stream outStr = File.Create(outputFilePath);

            RecipantsToEncrypt();


            var mode = (CipherMode)ComboBoxCipherMode.SelectedValue;

            byte[] header = Encryption.GenerateXmlHeader(recipientsToSend,keyLength.ToString(), 128, mode, Convert.ToBase64String(iv));
            outStr.Write(header, 0, header.Length);
            outStr.WriteByte((byte)3);

            //Encryption.AESEncrypt(textBoxChosenFileToEncFrom.Text, textBoxChosenFileToEncFrom.Text + 1, mode, new byte[] { 1, 2, 3 });
        }

       
        private void RecipantsToEncrypt()
        {
            recipientsToSend.Clear();

            foreach (var item in ListBoxRecipient.SelectedItems)
            {
                foreach (var recip in recipients)
                {
                    if (item.ToString() == recip.Name)
                    {
                        recip.EncryptedSymKey = RSACode(recip);
                        recipientsToSend.Add(recip);
                    }
                }
            }
        }
        private byte[] RSACode(Recipient recip)
        {
            var bytesToEncrypt = symKey;

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());

            encryptEngine.Init(true, recip.PubKey);

            var encrypted = encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);

            return encrypted;
        }

        private void btnAddRecipient_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                foreach (var file in openFileDialog.FileNames)
                {
                    var recip = new Recipient(System.IO.Path.GetFileNameWithoutExtension(file), readKey(file));

                    LoadRecip(recip);
                }
        }

    }
}
