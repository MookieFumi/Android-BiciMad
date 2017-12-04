using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Infrastructure.Transformers;
using Java.Lang;
using bicimad.Features.Stations.Presenters;

namespace bicimad
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, IStationsView
    {
        private RelativeLayout _progressBarLayout;
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private ViewPager _viewPager;
        private StationsPresenter _presenter;
        private NoFreeStationsPresenter _lowLightStationsPresenter;
        private TopAvailableStationsPresenter _topAvailableStationsPresenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            SetupToolbar();

            SetupPresenters();

            SetupTabs();
        }

        private void SetupPresenters()
        {
            _presenter = new StationsPresenter(this);
            _lowLightStationsPresenter = new NoFreeStationsPresenter(this);
            _topAvailableStationsPresenter = new TopAvailableStationsPresenter(this);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.StationsMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Update:
                    RunOnUiThread(async () =>
                    {
                        await _presenter.GetStationsAsync();
                    });
                    break;
                case Resource.Id.About:
                    Toast.MakeText(this, "About clicked", ToastLength.Short).Show();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void SetupTabs()
        {
            var tabs = new List<MainPageTab>
            {
                new MainPageTab(StationsFragment.NewInstance(this, _presenter), "Todas") ,
                new MainPageTab(StationsFragment.NewInstance(this, _lowLightStationsPresenter), "Poca disponibilidad") ,
                new MainPageTab(StationsFragment.NewInstance(this, _topAvailableStationsPresenter), "Top 5")
            };

            var mainPageAdapter = new MainPageAdapter(SupportFragmentManager, tabs);
            _viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            _viewPager.Adapter = mainPageAdapter;
            _viewPager.SetPageTransformer(true, new ScaleTransformer());
        }

        private void SetupToolbar()
        {
            _progressBarLayout = FindViewById<RelativeLayout>(Resource.Id.ProgressBarLayout);
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Toolbar);
            SetSupportActionBar(_toolbar);
            SupportActionBar.Title = GetString(Resource.String.app_name);
        }

        public void OnStationClick(Station station)
        {
            var fragmentManager = SupportFragmentManager.BeginTransaction();
            var dialog = StationDialogFragment.NewInstance(station);
            dialog.Show(fragmentManager, nameof(StationDialogFragment));
        }

        public void Busy(bool busy)
        {
            _viewPager.Visibility = busy ? ViewStates.Gone : ViewStates.Visible;
            _progressBarLayout.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
        }
    }

    public class MainPageAdapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        private readonly List<MainPageTab> _tabs;

        public MainPageAdapter(Android.Support.V4.App.FragmentManager fragmentManager, List<MainPageTab> tabs) : base(fragmentManager)
        {
            _tabs = tabs;
        }

        public override int Count => _tabs.Count;

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_tabs.ElementAt(position).Title.ToUpper());
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _tabs.ElementAt(position).Fragment;
        }
    }

    public class MainPageTab
    {
        public MainPageTab(Android.Support.V4.App.Fragment fragment, string title)
        {
            Fragment = fragment;
            Title = title;
        }

        public Android.Support.V4.App.Fragment Fragment { get; }
        public string Title { get; }
    }
}