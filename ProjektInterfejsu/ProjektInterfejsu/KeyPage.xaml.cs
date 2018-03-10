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


namespace ProjektInterfejsu
{
    /// <summary>
    /// Interaction logic for KeyPage.xaml
    /// </summary>
    public partial class KeyPage : Page
    {
        public KeyPage()
        {
            InitializeComponent();
        }

        private void generateKeysButton_Click(object sender, RoutedEventArgs e)

        {
            if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("Nie podano nazwy uæytkownika", "Wszystkie pola są obowiązkowe",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(passwor1Box.Password != password2Box.Password)
            {
                System.Windows.Forms.MessageBox.Show("Podane hała różnią się", "Różne hasła ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //TO DO
            //dłuższe niż 8 znaków, zawiera cyfrę, znak specjalny
        }
    }
}
