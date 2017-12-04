using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Features.Stations.Models.Services;

namespace bicimad.Features.Stations
{
    public class StationsPresenter : IStationsPresenter
    {
        private readonly IStationsView _view;
        private readonly StationsService _stationsService;

        public event EventHandler<int> StationsLoaded;

        protected virtual void OnStationsLoaded(int numberOfItems)
        {
            StationsLoaded?.Invoke(this, numberOfItems);
        }

        public StationsPresenter(IStationsView view)
        {
            _view = view;
            _stationsService = new StationsService();
            Stations = Enumerable.Empty<Station>().ToList();
        }

        public List<Station> Stations { get; set; }

        public async Task GetStationsAsync()
        {
            _view.Busy(true);

            Stations = await _stationsService.GetStationsAsync();
            OnStationsLoaded(Stations.Count);

            _view.Busy(false);
        }
    }
}