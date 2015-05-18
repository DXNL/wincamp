using MovieWatchr.Library.Helpers;
using MovieWatchr.Library.Services;
using MovieWatchr.Models;
using Newtonsoft.Json;
using StorageHelper.WindowsStore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;

namespace MovieWatchr.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppServiceConnection connection = null;

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
            set { SetProperty(ref _movies, value); }
        }

        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set { SetProperty(ref _selectedMovie, value); }
        }

        public async void LoadMovies()
        {
        }

        public async Task AddMovie(string title)
        {
        }
    }
}
