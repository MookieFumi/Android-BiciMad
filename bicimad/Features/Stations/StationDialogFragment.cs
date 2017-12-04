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
        private Station _station;
        private GoogleMap _map;
        private SupportMapFragment _mapFragment;

        internal static StationDialogFragment NewInstance( Station station)
        {
            return new StationDialogFragment
            {
                _station = station,
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
            number.Text = _station.Number;

            var name = view.FindViewById<TextView>(Resource.Id.Name);
            name.Text = _station.Name;
            var address = view.FindViewById<TextView>(Resource.Id.Address);
            address.Text = _station.Address;

            var total = view.FindViewById<TextView>(Resource.Id.Total);
            total.Text = _station.TotalBases.ToString();

            var dock = view.FindViewById<TextView>(Resource.Id.Dock);
            dock.Text = _station.DockBikes.ToString();

            var free = view.FindViewById<TextView>(Resource.Id.Free);
            free.Text = _station.FreeBases.ToString();

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

            var latLng = new LatLng(_station.Latitude, _station.Longitude);
            var zoom = CameraUpdateFactory.NewLatLngZoom(latLng, 17);
            _map.MoveCamera(zoom);

            var options = new MarkerOptions();
            options.SetPosition(latLng);
            options.SetTitle(_station.Number);
            _map.AddMarker(options);
        }
    }
}