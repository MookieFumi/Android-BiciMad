namespace bicimad.Models
{
    public class BiciMadResult
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string WhoAmI { get; set; }
        public string Version { get; set; }
        public string Time { get; set; }

        public BiciMadData Data { get; set; }
    }
}
