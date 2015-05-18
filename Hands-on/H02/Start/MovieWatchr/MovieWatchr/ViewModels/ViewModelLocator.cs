namespace MovieWatchr.ViewModels
{
    public static class ViewModelLocator
    {
        private static MainViewModel _main;
        public static MainViewModel Main
        {
            get { return _main ?? (_main = new MainViewModel()); }
        }
    }
}
