using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimetableWindowApp.Services;
using TimetableWindowApp.Contexts;
using System.Diagnostics.Eventing.Reader;
using TimetableWindowApp.Dtos;
using System.Threading.Tasks;

namespace TimetableWindowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SessionService _sessionService = new(new TimetableDbContext());

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionDatePicker.SelectedDate is not null && CinemaTextBox.Text.Length > 0)
            {
                var sessions = await _sessionService.GetSessionDtosAsync(SessionDatePicker.SelectedDate.Value, CinemaTextBox.Text);

                var isSaved = await _sessionService.SaveTimetableAsync(sessions);
                MessageBox.Show(isSaved ? "Сохранение прошло удачно" : "Сохранение прошло НЕ удачно");
                return;
            }
            MessageBox.Show("Выбери дату");
        }
    }
}