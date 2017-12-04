using Newtonsoft.Json;

namespace bicimad.Features.Stations.Models.Entities
{
    public class Station
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public Light Light { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public Activate Activate { get; set; }
        [JsonProperty("no_available")]
        public NoAvailable NoAvailable { get; set; }
        [JsonProperty("total_bases")]
        public int TotalBases { get; set; }
        [JsonProperty("dock_bikes")]
        public int DockBikes { get; set; }
        [JsonProperty("free_bases")]
        public int FreeBases { get; set; }
        [JsonProperty("reservations_count")]
        public int ReservationsCount { get; set; }
    }
}
