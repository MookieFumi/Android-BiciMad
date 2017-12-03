using Newtonsoft.Json;

namespace bicimad.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
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
