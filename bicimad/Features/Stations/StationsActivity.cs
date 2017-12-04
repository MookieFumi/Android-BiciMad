using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations.Models.Entities;

namespace bicimad.Features.Stations
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class StationsActivity : AppCompatActivity, IStationsView
    {
        private RelativeLayout _toolbar;
        private StationAdapter _stationAdapter;
        private StationsPresenter _presenter;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Stations);

            _toolbar = FindViewById<RelativeLayout>(Resource.Id.progressBar);

            SetupToolbar();

            _presenter = new StationsPresenter(this);
            _presenter.StationsLoaded += (sender, e) =>
            {
                Toast.MakeText(this, $"Total stations: {e}", ToastLength.Short).Show();
                _stationAdapter.NotifyDataSetChanged();
            };

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.StationRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            _stationAdapter = new StationAdapter(this, _presenter);
            _stationAdapter.StationClicked += (sender, station) =>
            {
                OnStationClick(station);
            };
            recyclerView.SetAdapter(_stationAdapter);

            await _presenter.GetStationsAsync();
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
                //case Android.Resource.Id.Home:
                //    drawerLayout.OpenDrawer(GravityCompat.Start);
                //    return true;

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

        public void OnStationClick(Station station)
        {
            var fragmentManager = SupportFragmentManager.BeginTransaction();
            var dialog = StationDialogFragment.NewInstance(station);
            dialog.Show(fragmentManager, nameof(StationDialogFragment));
        }

        public void Busy(bool busy)
        {
            _toolbar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
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