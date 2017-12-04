using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using bicimad.Features.Stations.Models.Entities;

namespace bicimad.Features.Stations
{
    [Activity(MainLauncher = true)]
    public class StationsActivity : AppCompatActivity, IStationsView
    {
        private StationAdapter _stationAdapter;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.Stations);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            SetupToolbar();

            var presenter = new StationsPresenter(this);
            presenter.StationsLoaded += (sender, e) =>
            {
                Toast.MakeText(this, $"Total stations: {e}", ToastLength.Short).Show();
                _stationAdapter.NotifyDataSetChanged();
            };

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.StationRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            _stationAdapter = new StationAdapter(this, presenter);
            _stationAdapter.StationClicked += (sender, station) =>
            {
                OnStationClick(station);
            };
            recyclerView.SetAdapter(_stationAdapter);

            await presenter.GetStationsAsync();
        }

        public void OnStationClick(Station station)
        {
            var fragmentManager = SupportFragmentManager.BeginTransaction();
            var dialog = StationDialogFragment.NewInstance(station);
            dialog.Show(fragmentManager, nameof(StationDialogFragment));
        }

        private void SetupToolbar()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = GetString(Resource.String.app_name);

            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_menu);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
        }
    }
}