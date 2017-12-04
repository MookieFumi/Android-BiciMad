using bicimad.Features.Stations.Models.Entities;

namespace bicimad.Features.Stations
{
    public interface IStationsView
    {
        void OnStationClick(Station station);
    }
}