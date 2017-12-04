using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Infrastructure;

namespace bicimad.Features.Stations
{
    public class StationDialogFragment : Android.Support.V4.App.DialogFragment, IOnMapReadyCallback
    {
        public Station Station { get; private set; }
        private GoogleMap _map;
        private SupportMapFragment _mapFragment;

        internal static StationDialogFragment NewInstance(Station station)
        {
            return new StationDialogFragment
            {
                Station = station
            };
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            var transaction = FragmentManager.BeginTransaction();
            transaction.Remove(_mapFragment);
            transaction.Commit();

            base.OnDismiss(dialog);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Station, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            this.AdjustSize();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var number = view.FindViewById<TextView>(Resource.Id.Number);
            number.Text = Station.Number;

            var name = view.FindViewById<TextView>(Resource.Id.Name);
            name.Text = Station.Name;
            var address = view.FindViewById<TextView>(Resource.Id.Address);
            address.Text = Station.Address;

            var total = view.FindViewById<TextView>(Resource.Id.Total);
            total.Text = Station.TotalBases.ToString();

            var dock = view.FindViewById<TextView>(Resource.Id.Dock);
            dock.Text = Station.DockBikes.ToString();

            var free = view.FindViewById<TextView>(Resource.Id.Free);
            free.Text = Station.FreeBases.ToString();

            SetupMap();
        }

        private void SetupMap()
        {
            if (_map != null) return;

            _mapFragment = FragmentManager.FindFragmentById(Resource.Id.map) as SupportMapFragment;
            _mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            var latLng = new LatLng(Station.Latitude, Station.Longitude);
            var zoom = CameraUpdateFactory.NewLatLngZoom(latLng, 17);
            _map.MoveCamera(zoom);

            var options = new MarkerOptions();
            options.SetPosition(latLng);
            options.SetTitle(Station.Number);
            _map.AddMarker(options);
        }
    }
}