using System.Collections.Generic;
using System.Threading.Tasks;
using bicimad.Features.Stations.Models.Entities;

namespace bicimad.Features.Stations
{
    public interface IStationsPresenter
    {
        Task GetStationsAsync();
        List<Station> Stations { get; set; }
    }
}