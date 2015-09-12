namespace TVMazeAPI.Models
{
    /// <summary>
    /// Another role the Actor has been in.
    /// </summary>
    public class CrewCredit
    {
        /// <summary>
        /// Type of Role.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Accessible links for relevant Cast information.
        /// </summary>
        public CrewLinks _links { get; set; }

        /// <summary>
        /// Accessible links for relevant Cast information.
        /// </summary>
        public class CrewLinks
        {
            /// <summary>
            /// Link to the show in which this role was performed.
            /// </summary>
            public Link show { get; set; }
        }
    }
}
