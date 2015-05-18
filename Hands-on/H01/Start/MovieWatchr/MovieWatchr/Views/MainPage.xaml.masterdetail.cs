using MovieWatchr.Models;
using MovieWatchr.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieWatchr.Views
{
    public sealed partial class MainPage
    {
        #region Master-Detail behavior
        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // if resizing from >1024 width to <1024 width
            if (e.NewSize.Width < 1024 && e.PreviousSize.Width >= 1024)
            {
                // Show details if a movie is selected
                if (ViewModelLocator.Main.SelectedMovie != null)
                    ShowDetailsGrid();
            }
            // otherwise (<1024 to >1024)
            else if (e.NewSize.Width > 1024 && e.PreviousSize.Width <= 1024)
            {
                // select the movie in the Master ListView and restore the view
                if (ViewModelLocator.Main.SelectedMovie != null)
                {
                    MasterListView.SelectedItem = ViewModelLocator.Main.SelectedMovie;
                    RestoreMasterDetail(true);
                }
            }
        }

        // if selection in the Master ListView changes, set the SelectedMovie property on MainViewModel
        private void MasterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0 && e.AddedItems[0] != null && e.AddedItems[0] is Movie)
                ViewModelLocator.Main.SelectedMovie = e.AddedItems[0] as Movie;
        }

        // if item is clicked in the Master ListView, set SelectedMovie on MainViewModel and show details
        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is Movie)
            {
                ViewModelLocator.Main.SelectedMovie = e.ClickedItem as Movie;
                ShowDetailsGrid();
            }
        }

        // restore the view if back button is clicked on detail view or hardware back button is clicked
        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                if (MasterListViewColumn.Width != new GridLength(1, GridUnitType.Star))
                {
                    RestoreMasterDetail(false);
                    e.Handled = true;
                }
            }
            else
                RestoreMasterDetail(false);
        }

        // show the details grid at full width and hide the Master ListView
        private void ShowDetailsGrid()
        {
            MasterListViewColumn.Width = new GridLength(0, GridUnitType.Star);
            DetailGridColumn.Width = new GridLength(1, GridUnitType.Star);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void RestoreMasterDetail(bool isResized)
        {
            MasterListViewColumn.Width = new GridLength(1, GridUnitType.Star);

            // if triggered by a resize and on a screen <1024, hide details view
            if (isResized && Window.Current.Bounds.Width < 1024)
                DetailGridColumn.Width = new GridLength(0, GridUnitType.Star);
            // otherwise, set appropriate width of details view
            else if (isResized && Window.Current.Bounds.Width < 1366)
                DetailGridColumn.Width = new GridLength(1.5, GridUnitType.Star);
            else if (isResized)
                DetailGridColumn.Width = new GridLength(2, GridUnitType.Star);
            // if not resized (back button), hide details view
            else
                DetailGridColumn.Width = new GridLength(0, GridUnitType.Star);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
        #endregion
    }
}
