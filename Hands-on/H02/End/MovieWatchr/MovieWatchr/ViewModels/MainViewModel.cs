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
            Movies = await Storage.LoadAsync<ObservableCollection<Movie>>("Movies");
            if (Movies == null)
                Movies = new ObservableCollection<Movie>();
        }

        public async Task AddMovie(string title)
        {
            await MovieWatchrAppService.EnsureConnectionToAppServiceAsync();

            var response = await MovieWatchrAppService.SendMessageAsync(title);
            if (response.Status == AppServiceResponseStatus.Success)
            {
                if ((bool)response.Message["Error"])
                    Movies.Add(new Movie { Title = title });
                else
                {
                    var movieJson = (string)response.Message["MovieJson"];
                    var movie = JsonConvert.DeserializeObject<Movie>(movieJson);
                    Movies.Add(movie);
                }
            }

            Storage.SaveAsync("Movies", Movies);
        }
    }
}
