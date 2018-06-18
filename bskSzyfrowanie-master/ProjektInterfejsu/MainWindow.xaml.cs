using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Paddings;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace ProjektInterfejsu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new EncryptionPage();

        }

        private void EncryptionTab_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new EncryptionPage();
            EncryptionTab.Background = Brushes.Gray;
            DecryptionTab.Background = Brushes.LightGray;
            KeyButton.Background = Brushes.LightGray;
        }

        private void DecryptionTab_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new DecryptionPage();
            DecryptionTab.Background = Brushes.Gray;
            EncryptionTab.Background = Brushes.LightGray;
            KeyButton.Background = Brushes.LightGray;
        }
       
        private void Key_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new KeyPage();
            KeyButton.Background = Brushes.Gray;
            EncryptionTab.Background = Brushes.LightGray;
            DecryptionTab.Background = Brushes.LightGray;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

  
       
        //wybrano opcję "O programie"
        //wyświetlenie informacji o programie TO DO
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
