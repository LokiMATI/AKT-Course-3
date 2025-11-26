using System.Windows;
using WindowApp.Services;

namespace WindowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void TicketSaveButton_Click(object sender, RoutedEventArgs e)
        {
            TicketService service = new(new Contexts.TicketDbContext());

            try
            {
                var ticket = await service.GetTicketInfoAsync(int.Parse(TicketIdTextBox.Text));

                var isSaved = await service.SaveTicketAsync(ticket);

                MessageBox.Show(isSaved ? "Файл сохранён" : "Файл НЕ сохранён");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}