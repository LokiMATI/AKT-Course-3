using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Contexts;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для FilmWindow.xaml
    /// </summary>
    public partial class FilmWindow : Window
    {
        private FilmService _filmService;
        private List<string> _ageRatings = new()
        {
            "0+",
            "4+"
        };
        private Film _film = new();

        public FilmWindow()
        {
            InitializeComponent();

            var context = new AppDbContext();
            _filmService = new FilmService(context);

            DataContext = _film;
            LoadAgeRatingsAsync();

            foreach (var rating in _ageRatings)
                AgeRatingComboBox.Items.Add(rating);
        }

        private async Task LoadAgeRatingsAsync() 
            => _ageRatings.AddRange(await _filmService.GetAgeRatingsAsync());
    }
}
