using bicimad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bicimad
{
    public interface IStationsPresenter
    {
        Task GetStationsAsync();
        List<Station> Stations { get; set; }
    }
}