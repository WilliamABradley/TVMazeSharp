namespace TVMazeAPI.Models
{
    /// <summary>
    /// Ratings that are given to Shows.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Average Rating Value for the Show.
        /// </summary>
        public double? average { get; set; }

        public override string ToString()
        {
            if (average.HasValue) return $"Average: {average}";
            else return "No Rating";
        }
    }
}
