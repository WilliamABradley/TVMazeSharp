using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;
using TVMazeAPI.Models;
using Newtonsoft.Json;

namespace TVMazeAPI
{
    /// <summary>
    /// TVRage API class
    /// </summary>
    public class TVMaze
    {
        #region Constants
        /// <summary>
        /// Search the scraper by Name.
        /// </summary>
        private const string SID_LOOKUP = @"http://api.tvmaze.com/search/shows?q=";
        /// <summary>
        /// Search the scraper by Name and return 1 Result, with a harsher acceptance chance.
        /// </summary>
        private const string SID_LOOKUP_SINGLE = @"http://api.tvmaze.com/singlesearch/shows?q=";
        /// <summary>
        /// Search the scaper by TVMaze Series ID.
        /// </summary>
        private const string SERIES_LOOKUP = @"http://api.tvmaze.com/shows/";
        /// <summary>
        /// Search the scaper by TheTVDB Series ID.
        /// </summary>
        private const string SERIES_LOOKUP_TVDB = @"http://api.tvmaze.com/lookup/shows?thetvdb=";
        /// <summary>
        /// Search the scaper by TVRage Series ID.
        /// </summary>
        private const string SERIES_LOOKUP_TVRAGE = @"http://api.tvmaze.com/lookup/shows?tvrage=";
        /// <summary>
        /// Path to Episodes information in the Series Path.
        /// </summary>
        private const string SERIES_EPISODES = "/episodes";
        /// <summary>
        /// Path to Cast information in the Series Path.
        /// </summary>
        private const string SERIES_CAST = "/cast";
        /// <summary>
        /// Search the Scraper for Actors by Name.
        /// </summary>
        private const string PEOPLE_LOOKUP = @"http://api.tvmaze.com/search/people?q=";
        /// <summary>
        /// Search the Scraper for Actors by TVMaze Person ID.
        /// </summary>
        private const string CAST_LOOKUP = @"http://api.tvmaze.com/people/";
        /// <summary>
        /// Path to Cast Credits in the Cast Path.
        /// </summary>
        private const string CAST_CASTCRED = "/castcredits";
        /// <summary>
        /// Path to Cast Credits in the Cast Path.
        /// </summary>
        private const string CAST_CREWCRED = "/crewcredits";
        /// <summary>
        /// Fetch all Series from a page in a paginated collection of all series in the scraper.
        /// </summary>
        private const string ALL_SERIES_LOOKUP = @"http://api.tvmaze.com/shows?page=";
        /// <summary>
        /// Fetch Episodes for the current Date or Date Specified.
        /// </summary>
        private const string SCHEDLUE_LOOKUP = @"http://api.tvmaze.com/schedule";
        /// <summary>
        /// Json DeSerializable to TVMaze Classes, primarily MazeSeries.
        /// </summary>
        private const string _404Stub = "{\"name\":\"Not Found\",\"message\":\"\",\"code\":0,\"status\":404}";
        #endregion

        /// <summary>
        /// Base Server Fetching Method
        /// </summary>
        /// <typeparam name="T">Type of Class for the incoming Json to be DeSerialized to.</typeparam>
        /// <param name="lookup">Constant Path for data collection, with identifiers for collecting specific data.</param>
        /// <returns></returns>
        private static async Task<T> _Get<T>(Uri lookup)
        {
            string results = "";
            string responseBodyAsText = "";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(lookup).AsTask();
                responseBodyAsText = await response.Content.ReadAsStringAsync().AsTask();
                
                if (response.ReasonPhrase == "OK")
                {
                    results = responseBodyAsText.Replace("<br>", Environment.NewLine);
                    results = results.Replace("<p>", "");
                    results = results.Replace("</p>", "");
                    results = results.Replace("<i>", "");
                    results = results.Replace("</i>", "");
                    results = results.Replace("<b>", "");
                    results = results.Replace("</b>", "");
                    results = results.Replace("<li>", "");
                    results = results.Replace("</li>", "");
                    results = results.Replace("<ul>", "");
                    results = results.Replace("</ul>", "");
                    results = results.Replace("<div>", "");
                    results = results.Replace("<br />", "");
                    results = results.Replace("<br/>", "");
                    results = results.Replace("<em>", "");
                    results = results.Replace("<em/>", "");

                    try { return JsonConvert.DeserializeObject<T>(results); }
                    catch (JsonSerializationException) { return default(T); }
                }
                try { return JsonConvert.DeserializeObject<T>(_404Stub); }
                catch (JsonSerializationException) { return default(T); }
            }
        }

        /// <summary>
        /// Search the Scraper for Shows with names similar to this Query.
        /// </summary>
        /// <param name="Query">Show Name</param>
        /// <returns>A collection of results, filled with Show Information</returns>
        public static async Task<IReadOnlyCollection<SearchContainerShow>> FindSeries(string Query)
        {
            Query = Uri.UnescapeDataString(Query);
            if (string.IsNullOrWhiteSpace(Query)) return new SearchContainerShow[0];
            return await _Get<SearchContainerShow[]>(new Uri(SID_LOOKUP + Query));
        }

        /// <summary>
        /// Search the Scraper for People with names similar to this Query.
        /// </summary>
        /// <param name="Query">Actor Name</param>
        /// <returns>A collection of results, filled with Actor Information</returns>
        public static async Task<IReadOnlyCollection<SearchContainerPerson>> FindPerson(string Query)
        {
            Query = Uri.UnescapeDataString(Query);
            if (string.IsNullOrWhiteSpace(Query)) return new SearchContainerPerson[0];
            return await _Get<SearchContainerPerson[]>(new Uri(PEOPLE_LOOKUP + Query));
        }

        /// <summary>
        /// Fetches an Actor from the Scraper with a specified TVMaze ID.
        /// </summary>
        /// <param name="PersonID">Actor's TVMaze ID Number</param>
        /// <param name="FetchCastCrewCredits">Fetches information about actor, such as other shows they have been in, and what they are executive producer in, etc.</param>
        public static async Task<CastMember> GetCastMember(uint PersonID, bool FetchCastCrewCredits = true)
        {
            var member = await _Get<CastMember>(new Uri(CAST_LOOKUP + PersonID));
            if (FetchCastCrewCredits)
            {
                await member.getCastCredit();
                await member.getCrewCredit();
            }
            return member;
        }

        /// <summary>
        /// Fetches a collection of other shows the Actor has been in.
        /// </summary>
        /// <param name="PersonID">Actor's TVMaze ID Number</param>
        public static async Task<IReadOnlyCollection<CastCredit>> GetCast_CastCredits(uint PersonID)
        {
            return await _Get<CastCredit[]>(new Uri(CAST_LOOKUP + PersonID + CAST_CASTCRED));
        }

        /// <summary>
        /// Fetches a collection of behind the scenes roles the Actor has been in.
        /// </summary>
        /// <param name="PersonID">Actor's TVMaze ID Number</param>
        public static async Task<IReadOnlyCollection<CrewCredit>> GetCast_CrewCredits(uint PersonID)
        {
            return await _Get<CrewCredit[]>(new Uri(CAST_LOOKUP + PersonID + CAST_CREWCRED));
        }

        /// <summary>
        /// 0 Based Array of Series by the Scraper in amounts of up to 250 per Page, see: http://www.tvmaze.com/api for details.
        /// </summary>
        /// <param name="pageNumber">Index number of page to recieve data from</param>
        /// <returns></returns>
        public static async Task<IReadOnlyCollection<MazeSeries>> GetSeriesAtPage(int pageNumber)
        {
            return await _Get<MazeSeries[]>(new Uri(ALL_SERIES_LOOKUP + pageNumber));
        }

        /// <summary>
        /// Base Get Series Method, allows fetching of Episodes and Cast information.
        /// </summary>
        /// <param name="uri">Link to fetch Show Data from.</param>
        /// <param name="FetchEpisodes">Fetch episodes in the show, or leave null.</param>
        /// <param name="FetchCast">Fetch Cast in the show, or leave null.</param>
        /// <returns></returns>
        private static async Task<MazeSeries> _GetSeries(Uri uri, bool FetchEpisodes = false, bool FetchCast = false)
        {
            var series = await _Get<MazeSeries>(uri);
            if (series.status != "404")
            {
                if (FetchEpisodes) await series.getEpisodes();
                if (FetchCast) await series.getCast();
            }
            return series;
        }

        /// <summary>
        /// Search the scraper by Name and return 1 Result, with a harsher acceptance chance.
        /// </summary>
        /// <param name="Query">Show Name</param>
        /// <param name="FetchEpisodes">Fetch episodes in the show, or leave null.</param>
        /// <param name="FetchCast">Fetch Cast in the show, or leave null.</param>
        public static async Task<MazeSeries> FindSingleSeries(string Query, bool FetchEpisodes = false, bool FetchCast = false) { Query = Uri.EscapeDataString(Query); return await _GetSeries(new Uri(SID_LOOKUP_SINGLE + Query), FetchEpisodes, FetchCast); }
        /// <summary>
        /// Search the scaper by TVMaze Series ID.
        /// </summary>
        /// <param name="TVMazeID">TVMaze Series ID</param>
        /// <param name="FetchEpisodes">Fetch episodes in the show, or leave null.</param>
        /// <param name="FetchCast">Fetch Cast in the show, or leave null.</param>
        public static async Task<MazeSeries> GetSeries(uint TVMazeID, bool FetchEpisodes = false, bool FetchCast = false) { return await _GetSeries(new Uri(SERIES_LOOKUP + TVMazeID), FetchEpisodes, FetchCast); }
        /// <summary>
        /// Search the scaper by TheTVDB Series ID.
        /// </summary>
        /// <param name="TVDBID">TheTVDB Series ID</param>
        /// <param name="FetchEpisodes">Fetch episodes in the show, or leave null.</param>
        /// <param name="FetchCast">Fetch Cast in the show, or leave null.</param>
        public static async Task<MazeSeries> GetSeriesfromTVDBID(uint TVDBID, bool FetchEpisodes = false, bool FetchCast = false) { return await _GetSeries(new Uri(SERIES_LOOKUP_TVDB + TVDBID), FetchEpisodes, FetchCast); }
        /// <summary>
        /// Search the scaper by TVRage Series ID.
        /// </summary>
        /// <param name="TVRageID">TVRage Series ID</param>
        /// <param name="FetchEpisodes">Fetch episodes in the show, or leave null.</param>
        /// <param name="FetchCast">Fetch Cast in the show, or leave null.</param>
        public static async Task<MazeSeries> GetSeriesfromTVRageID(uint TVRageID, bool FetchEpisodes = false, bool FetchCast = false) { return await _GetSeries(new Uri(SERIES_LOOKUP_TVRAGE + TVRageID), FetchEpisodes, FetchCast); }

        /// <summary>
        /// Get Episodes for a Series.
        /// </summary>
        /// <param name="TVMazeID">TVMaze Series ID.</param>
        public static async Task<IReadOnlyCollection<MazeEpisode>> GetSeries_Episodes(uint TVMazeID)
        {
            return await _Get<MazeEpisode[]>(new Uri(SERIES_LOOKUP + TVMazeID + SERIES_EPISODES));
        }

        /// <summary>
        /// Get the Cast of a Series.
        /// </summary>
        /// <param name="TVMazeID">TVMaze Series ID.</param>
        /// <param name="FetchCastCrewCredits">Fetches information about every other show and role the Actor has been in.</param>
        public static async Task<IReadOnlyCollection<CastMember>> GetSeries_Cast(uint TVMazeID, bool FetchCastCrewCredits = true)
        {
            var members = await _Get<CastMember[]>(new Uri(SERIES_LOOKUP + TVMazeID + SERIES_CAST));
            if (FetchCastCrewCredits)
            {
                foreach (var member in members)
                {
                    await member.getCastCredit();
                    await member.getCrewCredit();
                }
            }
            return members;
        }

        /// <summary>
        /// Fetch a collection of all episodes from all Series, that are being aired Today.
        /// </summary>
        public static async Task<IReadOnlyCollection<MazeEpisode>> FetchAllTodaysEpisodes()
        {
            return await _Get<MazeEpisode[]>(new Uri(SCHEDLUE_LOOKUP));
        }
        /// <summary>
        /// Fetches all of the episodes from every series on TVMaze for the specified date.
        /// </summary>
        /// <param name="date">Date to Fetch Episodes from</param>
        /// <param name="Country">ISO 2 Letter Country Code: https://en.wikipedia.org/wiki/ISO_3166-1</param>
        /// <returns></returns>
        public static async Task<IReadOnlyCollection<MazeEpisode>> FetchAllEpisodesOnDate(DateTime date, string Country = "US")
        {
            return await _Get<MazeEpisode[]>(new Uri($"{SCHEDLUE_LOOKUP}?{Country}&date={date.ToString("yyyy-MM-dd")}"));
        }
    }
}
