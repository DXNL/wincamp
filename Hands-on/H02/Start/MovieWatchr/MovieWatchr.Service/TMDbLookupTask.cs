using Newtonsoft.Json.Linq;
using System;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Web.Http;

namespace MovieWatchr.Service
{
    public sealed class TMDbLookupTask : IBackgroundTask
    {
        private static BackgroundTaskDeferral _serviceDeferral;
        private static HttpClient _httpClient;

        private static string _tmdbApiKey = "TMDB_API_KEY"; // Swap out with your TMDb API key
        private static string _tmdbSearchUri = "http://api.themoviedb.org/3/search/movie?api_key={0}&query={1}";
        private static string _tmdbMovieUri = "http://api.themoviedb.org/3/movie/{0}?api_key={1}";

        public void Run(IBackgroundTaskInstance taskInstance)
        {
        }
    }
}
