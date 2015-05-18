using System.Collections.Generic;
using MovieWatchr.Models;

namespace MovieWatchr.Data
{
    public class StaticDataSource
    {
        private readonly List<Movie> _movies = new List<Movie>();
        public List<Movie> Movies
        {
            get { return _movies; }
        }
    }
}
