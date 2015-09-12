namespace TVMazeAPI.Models
{
    /// <summary>
    /// Another show the Actor has been in.
    /// </summary>
    public class CastCredit
    {
        /// <summary>
        /// Accessible links for relevant Cast information.
        /// </summary>
        public CastLink _links { get; set; }
        /// <summary>
        /// Accessible links for relevant Cast information.
        /// </summary>
        public class CastLink
        {
            /// <summary>
            /// Link to the Character Page that this actor plays in a particular show.
            /// </summary>
            public Link character { get; set; }
            /// <summary>
            /// Link to the show that this actor stars in.
            /// </summary>
            public Link show { get; set; }
        }
    }
}
