using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Infrastructure;
using Newtonsoft.Json;

namespace bicimad.Features.Stations
{
    public class StationsFragment : Android.Support.V4.App.Fragment, IStationsView
    {
        //private RelativeLayout _toolbar;
        private StationAdapter _stationAdapter;
        private StationsPresenter _presenter;
        //public IEnumerable<Player> Players { get; private set; }

        public static StationsFragment NewInstance(StationsPresenter presenter)
        {
            var fragment = new StationsFragment();
            fragment._presenter = presenter;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetupToolbar();

            _presenter = new StationsPresenter(this);
            _presenter.StationsLoaded += (sender, e) =>
            {
                Toast.MakeText(Activity, $"Total stations: {e}", ToastLength.Short).Show();
                _stationAdapter.NotifyDataSetChanged();
            };


            //Recuperamos las variables guardadas en OnSaveInstanceState
            //var value = savedInstanceState?.GetString(Constants.TagStations, string.Empty);
            //if (!string.IsNullOrEmpty(value))
            //    Players = JsonConvert.DeserializeObject<IEnumerable<Player>>(value);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Stations, container, false);
        }

        /// <summary>
        ///     Any view setup should occur here.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="savedInstanceState"></param>
        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.StationRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity, LinearLayoutManager.Vertical, false));

            _stationAdapter = new StationAdapter(Activity, _presenter);
            _stationAdapter.StationClicked += (sender, station) =>
            {
                OnStationClick(station);
            };
            recyclerView.SetAdapter(_stationAdapter);

            await _presenter.GetStationsAsync();

            //var playerAdapter = new PlayerAdapter(Activity, Players.ToArray());
            //playerAdapter.PlayerClicked += (sender, player) =>
            //{
            //    Snackbar
            //        .Make(view, "Message sent", Snackbar.LengthShort)
            //        //.SetAction("Undo", (view) => { /*Undo message sending here.*/ })
            //        .Show(); // Don’t forget to show!
            //};
            //playerAdapter.DetailClicked += (sender, player) =>
            //{
            //    Toast.MakeText(Activity, $"Detail clicked: {player.Name}", ToastLength.Short).Show();
            //};
            //playerAdapter.SelectionClicked += (sender, player) =>
            //{
            //    Toast.MakeText(Activity, $"Selection clicked: {player.Name}", ToastLength.Short).Show();
            //};

            //var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            //recyclerView.SetLayoutManager(GetLayoutManager());
            //recyclerView.SetAdapter(playerAdapter);
        }

        private LinearLayoutManager GetLayoutManager()
        {
            return new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
        }


        public override void OnSaveInstanceState(Bundle outState)
        {
            //Todas las variables locales las guardamos en el Bundle para posteriormente recuperarla en el OnCreate
            //outState.PutString(Constants.TagStations, JsonConvert.SerializeObject(Players));

            //base.OnSaveInstanceState(outState);
        }

        public void OnStationClick(Station station)
        {
            //var fragmentManager = SupportFragmentManager.BeginTransaction();
            //var dialog = StationDialogFragment.NewInstance(station);
            //dialog.Show(fragmentManager, nameof(StationDialogFragment));
        }

        public void Busy(bool busy)
        {
            //_toolbar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
        }

        private void SetupToolbar()
        {
            //var toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Toolbar);
            //Activity.Set(toolbar);
            //SupportActionBar.Title = GetString(Resource.String.app_name);

            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_menu);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
        }
    }
}