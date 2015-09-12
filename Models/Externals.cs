namespace TVMazeAPI.Models
{
    /// <summary>
    /// Identifiers for the series on other scrapers.
    /// </summary>
    public class Externals
    {
        /// <summary>
        /// ID for The TVRage Scraper.
        /// </summary>
        public uint? tvrage { get; set; }
        /// <summary>
        /// ID for the TheTVDB Scraper.
        /// </summary>
        public uint? thetvdb { get; set; }

        public override string ToString()
        {
            if (tvrage.HasValue && thetvdb.HasValue) return $"TVRage: {tvrage.Value}, TheTVDB: {thetvdb.Value}";
            else if (tvrage.HasValue) return $"TVRage: {tvrage.Value}";
            else return $"TheTVDB: {thetvdb.Value}";
        }
    }
}
