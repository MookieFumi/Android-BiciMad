﻿using Android.App;
using Android.OS;
using bicimad.Models;
using Android.Widget;
using Android.Views;
using Android.Graphics;
using bicimad.Infrastructure;

namespace bicimad
{
    public class StationDialogFragment : Android.Support.V4.App.DialogFragment
    {
        public Station Station { get; private set; }

        internal static StationDialogFragment NewInstance(Station station)
        {
            return new StationDialogFragment
            {
                Station = station
            };
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
        }
    }
}