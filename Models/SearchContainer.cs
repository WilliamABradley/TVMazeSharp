namespace TVMazeAPI.Models
{
    /// <summary>
    /// A DeSerializable container to display the Show result and score of how close to query it is.
    /// </summary>
    public class SearchContainerShow
    {
        public MazeSeries show { get; set; }
        /// <summary>
        /// Score rank of how close result is to query.
        /// </summary>
        public double score { get; set; }

        public override string ToString()
        {
            return $"{show.name} Score: {score}";
        }
    }
    /// <summary>
    /// A DeSerializable container to display the Person result and score of how close to query it is.
    /// </summary>
    public class SearchContainerPerson
    {
        public Human person { get; set; }
        /// <summary>
        /// Score rank of how close result is to query.
        /// </summary>
        public double score { get; set; }

        public override string ToString()
        {
            return $"{person.name} Score: {score}";
        }
    }
}
