using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Features.Stations.Presenters;
using bicimad.Infrastructure.Transformers;

namespace bicimad.Features.Stations
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class StationsActivity : AppCompatActivity, IStationsView
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
            var tabs = new List<StationsTab>
            {
                new StationsTab(StationsFragment.NewInstance(this, _presenter), "Todas") ,
                new StationsTab(StationsFragment.NewInstance(this, _lowLightStationsPresenter), "Poca disponibilidad") ,
                new StationsTab(StationsFragment.NewInstance(this, _topAvailableStationsPresenter), "Top 5")
            };

            var mainPageAdapter = new StationsTabAdapter(SupportFragmentManager, tabs);
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
}