namespace TVMazeAPI.Models
{
    /// <summary>
    /// Information about a particular Country and Timezone information.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Full Name of Country.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// ISO 2 Letter Country Code.
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Timezone this particular show is on.
        /// </summary>
        public string timezone { get; set; }

        public override string ToString()
        {
            return $"{name} ({timezone})";
        }
    }
}
