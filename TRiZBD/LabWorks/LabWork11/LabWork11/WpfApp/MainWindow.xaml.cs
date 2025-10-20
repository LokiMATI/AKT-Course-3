using System.Windows;
using WpfApp.Contexts;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilmService _filmService;
        private List<Film> _films;
        public MainWindow()
        {
            InitializeComponent();

            var context = new AppDbContext();
            _filmService = new FilmService(context);

            LoadFilmsAsync();
        }

        private async Task LoadFilmsAsync()
        {
            _films = await _filmService.GetFilmsAsync();
            FilmsDataGrid.ItemsSource = _films;
        }

        private async Task RemoveFilmAsync()
        {
            var films = FilmsDataGrid.SelectedItems;
            foreach (Film film in films)
                await _filmService.RemoveFilmAsync(film.FilmId);
        }

        private async void RemoveFilmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FilmsDataGrid.SelectedItems.Count < 1)
                    throw new Exception("Необходимо выбрать фильм для удаления.");

                if (MessageBox.Show(
                    $"Вы уверены, что хотите удалить {FilmsDataGrid.SelectedItems.Count} записей?",
                    "Удаление",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;

                await RemoveFilmAsync();
                MessageBox.Show("Данные успешно удалены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                await LoadFilmsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось удалить записи. Причина: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddFilmButton_Click(object sender, RoutedEventArgs e)
        {
            FilmWindow window = new();
            Hide();
            window.ShowDialog();
            Show();
        }
    }
}