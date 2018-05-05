using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace ProjektInterfejsu
{
    /// <summary>
    /// Interaction logic for EncryptionPage.xaml
    /// </summary>
    public partial class EncryptionPage : Page
    {
       
        public EncryptionPage()
        {
            InitializeComponent();
        }

        //wybor pliku wejsciowego do szyfrowania
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseFileToEncryptDialog = new OpenFileDialog();
            chooseFileToEncryptDialog.Title = "Wybór pliku do zaszyfrowania";
            chooseFileToEncryptDialog.InitialDirectory= @"C:\";
            chooseFileToEncryptDialog.CheckFileExists = true;
            chooseFileToEncryptDialog.CheckPathExists = true;

            if (chooseFileToEncryptDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxChosenFileToEncFrom.Text = chooseFileToEncryptDialog.FileName;
            }
        }

        //
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseFileToEncryptToDialog = new OpenFileDialog();
            chooseFileToEncryptToDialog.Title = "Wybór pliku docelowego";
            chooseFileToEncryptToDialog.InitialDirectory = @"C:\";
            chooseFileToEncryptToDialog.CheckFileExists = true;
            chooseFileToEncryptToDialog.CheckPathExists = true;

            if (chooseFileToEncryptToDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxChosenFileToEncTo.Text = chooseFileToEncryptToDialog.FileName;
            }
        }
    }
}
