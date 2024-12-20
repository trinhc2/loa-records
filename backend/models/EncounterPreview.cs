namespace database.Models
{
    public class EncounterPreview
    {
        public int id { get; set; }
        public string? current_boss { get; set; }
        public int? duration { get; set; }
        public string? local_player { get; set; }
        public int? my_dps { get; set; }
        public bool? cleared { get; set; }
    }
}