using System.Linq;

namespace bicimad.Features.Stations.Presenters
{
    public class TopAvailableStationsPresenter : StationsPresenterBase
    {
        public TopAvailableStationsPresenter(IStationsView view) : base(view)
        {
        }

        public override void RefineStations()
        {
            Stations = Stations
                .OrderByDescending(p => p.TotalBases)
                .Take(10)
                .ToList();
        }
    }
}