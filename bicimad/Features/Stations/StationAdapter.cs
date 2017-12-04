using System;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations.Models.Entities;

namespace bicimad.Features.Stations
{
    public class StationAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private readonly IStationsPresenter _presenter;

        public event EventHandler<Station> StationClicked;

        protected virtual void OnStationClicked(Station station)
        {
            StationClicked?.Invoke(this, station);
        }

        public StationAdapter(Context context, IStationsPresenter presenter)
        {
            _context = context;
            _presenter = presenter;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var station = _presenter.Stations.ElementAt(position);
            var holder = viewHolder as StationHolder;

            if (station.Activate == Activate.NoActivate)
            {
                holder.ItemView.SetBackgroundColor(Android.Graphics.Color.Salmon);
            }

            holder.Name.Text = station.Name;
            holder.Address.Text = station.Address;

            holder.Total.Text = station.TotalBases.ToString();
            holder.Dock.Text = station.DockBikes.ToString();
            holder.Free.Text = station.FreeBases.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup and inflate your layout here
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.StationRecyclerViewRow, parent, false);
            return new StationHolder(itemView, position =>
            {
                var station = _presenter.Stations.ElementAt(position);
                OnStationClicked(station);
            });
        }

        public override int ItemCount => _presenter.Stations.Count();

        public class StationHolder : RecyclerView.ViewHolder
        {
            public StationHolder(View view, Action<int> onClick) : base(view)
            {
                view.Click += (sender, e) => onClick(base.AdapterPosition);

                Name = view.FindViewById<TextView>(Resource.Id.Name);
                Address = view.FindViewById<TextView>(Resource.Id.Address);

                Total = view.FindViewById<TextView>(Resource.Id.Total);
                Dock = view.FindViewById<TextView>(Resource.Id.Dock);
                Free = view.FindViewById<TextView>(Resource.Id.Free);
            }

            public TextView Name { get; }
            public TextView Address { get; }

            public TextView Total { get; }
            public TextView Dock { get; }
            public TextView Free { get; }
        }
    }
}