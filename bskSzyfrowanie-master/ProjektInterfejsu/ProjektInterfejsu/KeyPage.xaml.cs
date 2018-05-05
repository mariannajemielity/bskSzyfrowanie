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
       

        //sprawdzenie czy podane przez użytkownika hasło spełnia wymagania
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //przynajmniej 8 znaków
            if (passwordBoxPassword1.Password.Length <8)
                statusText.Text = "Hasło musi składać się z przynajmniej 8 znaków";
            //przynajmniej 1 litera          
            else if (!Regex.IsMatch(passwordBoxPassword1.Password, "[a-zA-Z]"))
                statusText.Text = "Hasło musi zawierać przynajmiej jedeną literę";
            //przynajmniej 1 cyfra
            else if(!Regex.IsMatch(passwordBoxPassword1.Password, "[0-9]"))
                statusText.Text = "Hasło musi zawierać przynajmiej jedną cyfrę";
            //przynajmniej 1 znak specjalny
            else if (Regex.IsMatch(passwordBoxPassword1.Password, "^[a-zA-Z0-9 ]*$"))
                statusText.Text = "Hasło musi zawierać przynajmiej jeden znak specjalny";
            else
                statusText.Text = string.Empty;
        }

        //kliknięcie "Wygeneruj parę kluczy"
        private void GenerateKeysButton_Click(object sender, RoutedEventArgs e)
        {
            //sprawdzenie czy podane hasła są identyczne
            if (!passwordBoxPassword1.Password.Equals(passwordBoxPassword2.Password)) 
                statusText.Text = "Podane hasła różnią się";

        }
    }
}
