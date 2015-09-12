using System.Collections.Generic;
using System.Threading.Tasks;

namespace TVMazeAPI.Models
{
    /// <summary>
    /// Information about a particular Actor.
    /// </summary>
    public class CastMember
    {
        /// <summary>
        /// Actor's information.
        /// </summary>
        public Human person { get; set; }
        /// <summary>
        /// Actors' Character information.
        /// </summary>
        public Human character { get; set; }
        /// <summary>
        /// Collection of all Shows the Actor has played a part in.
        /// </summary>
        public IReadOnlyCollection<CastCredit> castCredit { get; set; }
        /// <summary>
        /// Collection of all shows the Actor has done some behind the scenes work in.
        /// </summary>
        public IReadOnlyCollection<CrewCredit> crewCredit { get; set; }

        /// <summary>
        /// Fetches CastCredit information if null.
        /// </summary>
        public async Task getCastCredit()
        {
            castCredit = await TVMaze.GetCast_CastCredits(person.id);
        }

        /// <summary>
        /// Fetches CrewCredit information if null.
        /// </summary>
        public async Task getCrewCredit()
        {
            crewCredit = await TVMaze.GetCast_CrewCredits(person.id);
        }

        public override string ToString()
        {
            return $"Actor: {person.name}, Character: {character.name}";
        }
    }
}
