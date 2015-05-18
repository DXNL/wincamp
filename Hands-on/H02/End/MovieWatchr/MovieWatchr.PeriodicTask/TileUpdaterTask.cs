using MovieWatchr.Library.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Notifications;

namespace MovieWatchr.Tasks
{
    public sealed class TileUpdaterTask : IBackgroundTask
    {
        private const string TaskName = "TileUpdaterTask";
        
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var movie = await TileService.GetLastMovieWithMetadataAsync();

            var tileXmlString = string.Format(@"<tile>
  <visual>

    <binding template=""TileSmall"" branding=""logo"" />

    <binding template=""TileMedium"" displayName=""MovieWatchr"" branding=""name"">
      <image placement=""background"" src=""http://image.tmdb.org/t/p/w185{0}"" />
      <text hint-style=""caption"">{1}</text>
      <text hint-style=""captionsubtle"">{2}</text>
      <text hint-style=""caption"">{3} min</text>
    </binding>

    <binding template=""TileWide"" displayName=""Xbox"">
      <group>
        <subgroup hint-weight=""33"">
          <image placement=""inline"" src=""http://image.tmdb.org/t/p/w92{0}"" />
        </subgroup>
        <subgroup>
          <text hint-style=""body"">{1}</text>
          <text hint-style=""captionsubtle"">{2}</text>
          <text hint-style=""caption"">{3} min</text>
        </subgroup>
      </group>
    </binding>

  </visual>
</tile>", movie.PosterPath, movie.Title, movie.ReleaseDate, movie.Runtime);

            XmlDocument tileDOM = new XmlDocument();
            tileDOM.LoadXml(tileXmlString);

            TileNotification tile = new TileNotification(tileDOM);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tile);

            deferral.Complete();
        }

        public static IAsyncOperation<bool> Register()
        {
            return RegisterInternal().AsAsyncOperation();
        }

        private async static Task<bool> RegisterInternal()
        {
            if (!IsTaskRegistered())
            {
                await BackgroundExecutionManager.RequestAccessAsync();

                var builder = new BackgroundTaskBuilder { Name = TaskName, TaskEntryPoint = typeof(TileUpdaterTask).FullName };
                builder.SetTrigger(new TimeTrigger(30, false));

                builder.Register();
                return true;
            }
            return false;
        }

        public static void Unregister()
        {
            var entry = BackgroundTaskRegistration.AllTasks.FirstOrDefault(t => t.Value.Name == TaskName);

            if (entry.Value != null)
                entry.Value.Unregister(true);
        }

        public static bool IsTaskRegistered()
        {
            return BackgroundTaskRegistration.AllTasks.Any(t => t.Value.Name == TaskName);
        }
    }
}
