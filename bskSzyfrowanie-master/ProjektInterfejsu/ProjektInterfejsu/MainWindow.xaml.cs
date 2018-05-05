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

        private void EncryptionButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new EncryptionPage();
            EncryptionButton.Background = Brushes.Gray;
            DecryptionButton.Background = Brushes.LightGray;
            KeyButton.Background = Brushes.LightGray;
        }

        private void Decryption_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new DecryptionPage();
            DecryptionButton.Background = Brushes.Gray;
            EncryptionButton.Background = Brushes.LightGray;
            KeyButton.Background = Brushes.LightGray;
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new KeyPage();
            KeyButton.Background = Brushes.Gray;
            EncryptionButton.Background = Brushes.LightGray;
            DecryptionButton.Background = Brushes.LightGray;
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
