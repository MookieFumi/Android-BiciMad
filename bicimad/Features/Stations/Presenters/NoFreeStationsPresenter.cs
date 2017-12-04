using System.Linq;

namespace bicimad.Features.Stations.Presenters
{
    public class NoFreeStationsPresenter : StationsPresenterBase
    {
        public NoFreeStationsPresenter(IStationsView view) : base(view)
        {
        }

        public override void RefineStations()
        {
            Stations = Stations
                .Where(p => p.FreeBases == 0)
                .OrderByDescending(p => p.DockBikes)
                .ToList();
        }
    }
}