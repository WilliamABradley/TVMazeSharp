using System;

namespace TVMazeAPI.Models
{
    public class Human
    {
        /// <summary>
        /// Unique TVMaze Human Identifier.
        /// </summary>
        public uint id { get; set; }
        /// <summary>
        /// Url to this Human's Page on the Website.
        /// </summary>
        public Uri url { get; set; }
        /// <summary>
        /// Name of the Person/Character.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Images of the Human.
        /// </summary>
        public Image image { get; set; }
        /// <summary>
        /// Link to itself on the Website.
        /// </summary>
        public Links _links { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
