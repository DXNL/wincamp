using MovieWatchr.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieWatchr.Views
{
    public sealed partial class MainPage : BasePage
    {
        public MainViewModel ViewModel { get { return ViewModelLocator.Main; } } 

        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;
            SizeChanged += MainPage_SizeChanged;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Main.LoadMovies();
        }

        private void SelectAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MasterListView.SelectionMode != ListViewSelectionMode.Multiple)
            {
                MasterListView.SelectionMode = ListViewSelectionMode.Multiple;
                DeleteMultiAppBarButton.Visibility = Visibility.Visible;
                AddAppBarButton.Visibility = Visibility.Collapsed;
                MasterListView.IsItemClickEnabled = false;
            }
            else
            {
                if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    MasterListView.SelectionMode = ListViewSelectionMode.None;
                else
                    MasterListView.SelectionMode = ListViewSelectionMode.Single;
                DeleteMultiAppBarButton.Visibility = Visibility.Collapsed;
                AddAppBarButton.Visibility = Visibility.Visible;
                MasterListView.IsItemClickEnabled = true;
            }
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewMovieTextBox.Text.Trim()))
            {
                ViewModelLocator.Main.AddMovie(NewMovieTextBox.Text);
                AddMovieFlyout.Hide();
            }
        }
    }
}
