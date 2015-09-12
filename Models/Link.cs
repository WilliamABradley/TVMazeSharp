using System;

namespace TVMazeAPI.Models
{
    /// <summary>
    /// Link for Accessing Various pages on the Scaper's website.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// A Link to a Page on the Site.
        /// </summary>
        public Uri href { get; set; }
        public override string ToString()
        {
            return href.ToString();
        }
    }
}
