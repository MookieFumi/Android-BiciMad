using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Features.Stations.Models.Services;

namespace bicimad.Features.Stations.Presenters
{
    public abstract class StationsPresenterBase : IStationsPresenter
    {
        protected readonly IStationsView _view;
        protected readonly StationsService _stationsService;

        protected StationsPresenterBase(IStationsView view)
        {
            _view = view;
            _stationsService = new StationsService();
            Stations = Enumerable.Empty<Station>().ToList();
        }

        public event EventHandler<int> StationsLoaded;

        protected virtual void OnStationsLoaded(int numberOfItems)
        {
            StationsLoaded?.Invoke(this, numberOfItems);
        }

        public List<Station> Stations { get; set; }

        public async Task GetStationsAsync()
        {
            _view.Busy(true);

            Stations = await _stationsService.GetStationsAsync();
            RefineStations();

            OnStationsLoaded(Stations.Count);

            _view.Busy(false);
        }

        public abstract void RefineStations();
    }
}