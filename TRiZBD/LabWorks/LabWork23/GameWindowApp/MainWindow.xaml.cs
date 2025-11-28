using GameWindowApp.Serivces;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameWindowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameService _gameService = new(new Contexts.GameDbContext());
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectLogoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "PNG (*.png)|*.png";

            if (dialog.ShowDialog() == true)
                LogoNameLabel.Content = dialog.FileName;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LogoNameLabel.Content.ToString()) && !string.IsNullOrWhiteSpace(GameNameTextBox.Text))
            {
                try
                {
                    await _gameService.AddLogoAsync(GameNameTextBox.Text, LogoNameLabel.Content.ToString());
                    MessageBox.Show("Лого добавлено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void SaveScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _gameService.SaveScreenshotAsync(GameNameTextBox.Text);
                MessageBox.Show("Скриншот сохранён.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportUsersButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}