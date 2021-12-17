using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challonge.Internal.Properties
{
    public static class ChallongeInternalProperties
    {
        // Base
        public const string Authorization = "Authorization";
        public const string AuthorizationType = "Authorization-Type";
        public const string AuthorizationTypeValue = "v2";
        public const string ContentType = "Content-Type";
        public const string ContentTypeValue = "application/vnd.api+json";
        public const string Accept = "Accept";
        public const string AcceptValue = "application/json";

        // Shared
        public const string URL = "url";
        public const string Page = "page";
        public const string PerPage = "per_page";
        public const string CreatedAfter = "created_after";
        public const string CreatedBefore = "created_before";
        public const string Data = "data";

        // Tournament
        public const string TournamentType = "type";
        public const string TournamentState = "state";
        public const string SingleElimination = "single elimination";
        public const string DoubleElimination = "double elimination";
        public const string Leaderboard = "leaderboard";
        public const string FreeForAll = "free for all";
        public const string Swiss = "swiss";
        public const string RoundRobin = "round robin";
        public const string TimeTrail = "time trail";
        public const string GrandPrix = "grand prix";
        public const string TournamentStateAsType = "TournamentState";
        public const string TournamentStatePending = "pending";
        public const string TournamentStateUnderway = "underway";

        // User
        public const string User = "user";

        // Ranking
        public const string MatchWins = "match wins";
        public const string GameWins = "game wins";
        public const string GameWinPercentage = "game win percentage";
        public const string PointsScored = "points scored";
        public const string PointsDifference = "points difference";
        public const string Custom = "custom";

        // Grand Finals Modifier
        public const string Skip = "skip";
        public const string SingleMatch = "single match";

        // Match
        public const string MatchAsAType = "Match";
        public const string MatchStateAsAType = "MatchState";
        public const string MatchID = "match_id";
        public const string MatchType = "match";
        public const string matchStatePending = "pending";
        public const string matchStateOpen = "open";
        public const string matchStateReopen = "reopen";
        public const string matchStateMarkAsUnderway = "mark_as_underway";
        public const string matchStateUnmarkAsUnderway = "unmark_as_underway";
        public const string matchStateComplete = "complete";
        public const string matchState = "state";

        // Participant
        public const string ParticipantsType = "Participants";
        public const string ParticipantID = "participant_id";
        public const string ParticipantType = "participant";

        public enum HTTPRequestMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }

    [Serializable]
    public class Source
    {
        public string pointer;
    }

    public class DetailObject
    {
        public List<string> MyArray { get; set; }
    }
}
