using bicimad.Models;
using bicimad.Models.Services;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace bicimad
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
            Stations = await _stationsService.GetStationsAsync();
            OnStationsLoaded(Stations.Count());
        }
    }
}