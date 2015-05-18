using MovieWatchr.Models;
using StorageHelper.WindowsStore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatchr.Library.Services
{
    public static class TileService
    {
        public async static Task<Movie> GetLastMovieWithMetadataAsync()
        {
            var movies = await Storage.LoadAsync<ObservableCollection<Movie>>("Movies");
            if (movies == null) return null;

            return movies.Reverse().FirstOrDefault<Movie>(m => m.Id != 0);
        }
    }
}
