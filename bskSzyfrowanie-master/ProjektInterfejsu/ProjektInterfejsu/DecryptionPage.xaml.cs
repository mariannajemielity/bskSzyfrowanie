using System;
using System.Collections.Generic;
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
    /// Interaction logic for DecryptionPage.xaml
    /// </summary>
    public partial class DecryptionPage : Page
    {
        public DecryptionPage()
        {
            InitializeComponent();
        }

        //wybór pliku do odszyfrowania
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseFileToDecryptFromDialog = new OpenFileDialog();
            chooseFileToDecryptFromDialog.Title = "Wybór pliku do odszyfrowania";
            chooseFileToDecryptFromDialog.InitialDirectory = @"C:\";
            chooseFileToDecryptFromDialog.CheckFileExists = true;
            chooseFileToDecryptFromDialog.CheckPathExists = true;

            if (chooseFileToDecryptFromDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDecFromFile.Text = chooseFileToDecryptFromDialog.FileName;
            }
        }

        //wybór pliku do którego zostaną odszyfrowane dane
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseFileToDecryptToDialog = new OpenFileDialog();
            chooseFileToDecryptToDialog.Title = "Wybór pliku docelowego";
            chooseFileToDecryptToDialog.InitialDirectory = @"C:\";
            chooseFileToDecryptToDialog.CheckFileExists = true;
            chooseFileToDecryptToDialog.CheckPathExists = true;

            if (chooseFileToDecryptToDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDecToFile.Text = chooseFileToDecryptToDialog.FileName;
            }
        }
    }
}
