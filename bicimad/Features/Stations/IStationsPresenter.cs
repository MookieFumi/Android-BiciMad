using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bicimad.Features.Stations.Models.Entities;

namespace bicimad.Features.Stations
{
    public interface IStationsPresenter
    {
        event EventHandler<int> StationsLoaded;
        Task GetStationsAsync();
        void RefineStations();
        List<Station> Stations { get; set; }
    }
}