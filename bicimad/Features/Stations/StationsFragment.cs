using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;

namespace bicimad.Features.Stations
{
    public class StationsFragment : Android.Support.V4.App.Fragment
    {
        private IStationsView _view;
        private StationAdapter _stationAdapter;
        private IStationsPresenter _presenter;

        public static StationsFragment NewInstance(IStationsView view, IStationsPresenter presenter)
        {
            var fragment = new StationsFragment
            {
                _view = view,
                _presenter = presenter
            };
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _presenter.StationsLoaded += (sender, e) =>
            {
                _stationAdapter.NotifyDataSetChanged();
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Stations, container, false);
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.StationRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity, LinearLayoutManager.Vertical, false));

            _stationAdapter = new StationAdapter(Activity, _presenter);
            _stationAdapter.StationClicked += (sender, station) =>
            {
                _view.OnStationClick(station);
            };
            recyclerView.SetAdapter(_stationAdapter);

            await _presenter.GetStationsAsync();
        }
    }
}