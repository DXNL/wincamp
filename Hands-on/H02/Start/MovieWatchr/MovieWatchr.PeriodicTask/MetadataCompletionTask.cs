using MovieWatchr.Library.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;

namespace MovieWatchr.Tasks
{
    public sealed class MetadataCompletionTask : IBackgroundTask
    {
        private const string TaskName = "MetadataCompletionTask";

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();



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
