using MovieWatchr.Data;
using MovieWatchr.Library.Helpers;
using MovieWatchr.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.Storage;

namespace MovieWatchr.ViewModels
{
    public class MainViewModel : BindableBase
    {
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
            var staticDataFolder = await Package.Current.InstalledLocation.GetFolderAsync("Data");
            var staticData = await staticDataFolder.GetFileAsync("StaticData.json");
            var staticDataContent = await FileIO.ReadTextAsync(staticData);

            var staticDataSource = JsonConvert.DeserializeObject<StaticDataSource>(staticDataContent);
            Movies = new ObservableCollection<Movie>(staticDataSource.Movies);
        }

        public void AddMovie(string title)
        {
            Movies.Add(new Movie { Title = title, ReleaseDate = DateTime.Now.ToString("yyyy-MM-dd") });
        }
    }
}
