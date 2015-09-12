namespace TVMazeAPI.Models
{
    /// <summary>
    /// Base Links Class points to itself as its only link.
    /// </summary>
    public class Links
    {
        /// <summary>
        /// Link to the Website's Page
        /// </summary>
        public Link self { get; set; }
    }
}