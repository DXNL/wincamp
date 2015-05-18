using MovieWatchr.Models;
using Newtonsoft.Json;
using StorageHelper.WindowsStore;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace MovieWatchr.Library.Services
{
    public static class MovieWatchrAppService
    {
        public static AppServiceConnection AppServiceConnection = null;

        public static async Task EnsureConnectionToAppServiceAsync()
        {
            if (AppServiceConnection == null)
            {
                AppServiceConnection = new AppServiceConnection();
                AppServiceConnection.AppServiceName = "moviewatchr-tmdblookup";
                // Use the Windows.ApplicationModel.Package.Current.Id.FamilyName API to get this value
                AppServiceConnection.PackageFamilyName = "c6c83e34-53e3-40c8-9e5c-784ef51eef93_xv54q8mwq0yk0";

                var connectionStatus = await AppServiceConnection.OpenAsync();

                if (connectionStatus == AppServiceConnectionStatus.Success)
                {
                    // Subscribe to the ServiceClosed event to handle service being terminated
                    //appServiceConnection.ServiceClosed += OnServiceClosed;
                }
                else
                {
                    // Handle any errors appropriately
                }
            }
        }

        public static async Task UpdateMoviesMetadataAsync()
        {
            var movies = await Storage.LoadAsync<ObservableCollection<Movie>>("Movies");
            if (movies == null) return;

            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].Id == 0)
                {
                    await EnsureConnectionToAppServiceAsync();

                    var response = await SendMessageAsync(movies[i].Title);
                    if (response.Status == AppServiceResponseStatus.Success)
                    {
                        if (!(bool)response.Message["Error"])
                        {
                            var movieJson = (string)response.Message["MovieJson"];
                            movies[i] = JsonConvert.DeserializeObject<Movie>(movieJson);
                        }
                    }

                    Storage.SaveAsync("Movies", movies);
                }
            }
        }

        public static async Task<AppServiceResponse> SendMessageAsync(string message)
        {
            // Data to send to the app service
            var messageValueSet = new ValueSet();
            messageValueSet.Add("Title", message);

            // Send the message and handle the response
            return await AppServiceConnection.SendMessageAsync(messageValueSet);
        }
    }
}
