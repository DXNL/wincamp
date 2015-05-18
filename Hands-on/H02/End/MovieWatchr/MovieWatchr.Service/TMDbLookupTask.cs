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
            // Associate a cancellation handler with the background task.
            taskInstance.Canceled += TaskInstance_Canceled;

            // Get the deferral object from the task instance 
            _serviceDeferral = taskInstance.GetDeferral();

            _httpClient = new HttpClient();

            var appService = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if (appService != null && appService.Name == "moviewatchr-tmdblookup")
            {
                //ValidateCaller(appService.CallerPackageFamilyName);
                appService.AppServiceConnection.RequestReceived += AppServiceConnection_RequestReceived;
            }
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // Maybe do something...
        }

        private async void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var message = args.Request.Message;
            var title = message["Title"] as string;

            if (!string.IsNullOrWhiteSpace(title))
            {
                var messageDeferral = args.GetDeferral();
                var returnMessage = new ValueSet();

                try
                {
                    var searchResult = await _httpClient.GetStringAsync(new Uri(string.Format(_tmdbSearchUri, _tmdbApiKey, title)));
                    var movieId = JObject.Parse(searchResult)["results"][0]["id"];
                    var movieResult = await _httpClient.GetStringAsync(new Uri(string.Format(_tmdbMovieUri, movieId, _tmdbApiKey)));

                    // Build the return message with the movie JSON
                    returnMessage.Add("Error", false);
                    returnMessage.Add("MovieJson", movieResult);
                }
                catch (Exception ex)
                {
                    returnMessage.Add("Error", true);
                }
                finally
                {
                    await args.Request.SendResponseAsync(returnMessage);
                    messageDeferral.Complete();
                }
            }
        }
    }
}
