namespace bicimad.Features.Stations
{
    public class StationsTab
    {
        public StationsTab(Android.Support.V4.App.Fragment fragment, string title)
        {
            Fragment = fragment;
            Title = title;
        }

        public Android.Support.V4.App.Fragment Fragment { get; }
        public string Title { get; }
    }
}