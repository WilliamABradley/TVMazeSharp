namespace TVMazeAPI.Models
{
    /// <summary>
    /// Information about a particular Network or WebChannel.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Channel ID (Network or WebChannel, check Required for Ambiguity.)
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of the Channel.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Country the Network originates from.
        /// </summary>
        public Country country { get; set; }

        public override string ToString()
        {
            return $"{name}({country.code})";
        }
    }
}
