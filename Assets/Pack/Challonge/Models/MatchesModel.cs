using System;
using System.Collections.Generic;
using Challonge.Properties;

#region Documentation
/// Namespace:  Challonge.Models
///
/// Summary:    .
#endregion
namespace Challonge.Models
{

    #region Documentation
    /// Class:  Match
    ///
    /// Summary:    (Serializable) a match.
    #endregion
    [Serializable]
    public class Match
    {
        /// Summary:    The identifier.
        public string identifier;

        /// Summary:    The identifier.
        public string id;

        /// Summary:    The timestamps.
        public Timestamps timestamps;

        /// Summary:    Name of the tournament.
        public string tournamentName;

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Type of the tournament.
        public TournamentType tournamentType;

        /// Summary:    The round.
        public int round;

        /// Summary:    The scores.
        public string scores;

        /// Summary:    State of the match.
        public MatchState matchState;

        /// Summary:    Sets the score in belongs to.
        public List<object> scoreInSets;

        /// Summary:    The participants.
        public List<Participant> Participants = new List<Participant>();

        /// Summary:    The participants links.
        public Links participantsLinks;
    }

    [Serializable]
    public class MatchResult
    {
        public string participantId;
        public int score = 0;
        public int rank;
        public bool isAdvancing;
    }

    #region Documentation
    /// Class:  GetAllMatchesResponse
    ///
    /// Summary:    (Serializable) a get all matches response.
    #endregion
    [Serializable]
    public class GetAllMatchesResponse : ChallongeBaseResponse
    {
        /// Summary:    The matches.
        public List<Match> Matches = new List<Match>();
    }

    #region Documentation
    /// Class:  GetMatchResponse
    ///
    /// Summary:    (Serializable) a get match response.
    #endregion
    [Serializable]
    public class GetMatchResponse : ChallongeBaseResponse
    {
        /// Summary:    Specifies the match.
        public Match match;
    }

    #region Documentation
    /// Class:  GetAllMatchesRequest
    ///
    /// Summary:    (Serializable) a get all matches request.
    #endregion
    [Serializable]
    public class GetAllMatchesRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    State of the match.
        public MatchState matchState;

        /// Summary:    Identifier for the participant.
        public string participantID = "";

        /// Summary:    Number of pages.
        public int pageCount = 1;

        /// Summary:    The total items per page.
        public int totalItemsPerPage = 25;

        public GetAllMatchesRequest (string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/matches.json"))
        {
            this.tournamentURL = tournamentURL;
        }

        #region Documentation
        /// Enum:   MatchState
        ///
        /// Summary:    Values that represent match states.
        #endregion
        public enum MatchState
        {

            /// Summary:    An enum constant representing any option.
            Any,

            /// Summary:    An enum constant representing the pending option.
            Pending,

            /// Summary:    An enum constant representing the open option.
            Open,

            /// Summary:    An enum constant representing the complete option.
            Complete
        }
    }

    #region Documentation
    /// Class:  GetMatchRequest
    ///
    /// Summary:    (Serializable) a get match request.
    #endregion
    [Serializable]
    public class GetMatchRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Identifier for the match.
        public string matchID;

        public GetMatchRequest(string tournamentURL, string matchID, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/matches/" + matchID + ".json"))
        {
            this.tournamentURL = tournamentURL;
            this.matchID = matchID;
        }
    }

    [Serializable]
    public class UpdateMatchScoresRequest : ChallongeBaseRequest
    {
        /// Summary:    The matches.
        private List<Internal.Models.MatchResult> MatchResults = new List<Internal.Models.MatchResult>();

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Identifier for the match.
        public string matchID;

        public UpdateMatchScoresRequest(string tournamentURL, string matchID, bool isLeaderboard, List<MatchResult> matchResults, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/matches/" + matchID + ".json"))
        {
            this.tournamentURL = tournamentURL;
            this.matchID = matchID;
            for (int i = 0; i < matchResults.Count; i++)
                AddValuesFromMatchResult(matchResults[i], isLeaderboard);
        }

        public void AddValuesFromMatchResult(MatchResult matchResult, bool isLeaderboard)
        {
            if (isLeaderboard)
                AddValues(matchResult.participantId, matchResult.score);
            else
                AddValues(matchResult.participantId, matchResult.score, matchResult.rank, matchResult.isAdvancing);
        }

        public void AddValues(string participantID, int score)
        {
            Internal.Models.MatchResult match = new Internal.Models.MatchResult();
            match.participant_id = participantID;
            match.score_set = score.ToString();

            bool found = false;
            for (int i = 0; i < MatchResults.Count; i++)
            {
                if (MatchResults[i].participant_id == participantID)
                {
                    match.score_set = MatchResults[i].score_set + "," + match.score_set;
                    found = true;
                    MatchResults[i] = match;
                    break;
                }
            }

            if (!found)
                MatchResults.Add(match);
        }

        #region Documentation
        /// Function:   AddValues
        ///
        /// Summary:    Adds the values.
        ///
        /// Parameters:
        /// participantID -     Identifier for the participant.
        /// score -             The score.
        /// rank -              The rank.
        /// advancing -         True to advancing.
        #endregion
        public void AddValues(string participantID, int score, int rank, bool advancing)
        {
            Internal.Models.MatchResult match = new Internal.Models.MatchResult();
            match.participant_id = participantID;
            match.score_set = score.ToString();
            match.rank = rank;
            match.advancing = advancing;

            bool found = false;
            for (int i = 0; i < MatchResults.Count; i++)
            {
                if (MatchResults[i].participant_id == participantID)
                {
                    match.score_set = MatchResults[i].score_set + "," + match.score_set;
                    found = true;
                    MatchResults[i] = match;
                    break;
                }
            }

            if (!found)
                MatchResults.Add(match);
        }

        #region Documentation
        /// Function:   GetMatchUpdateValues
        ///
        /// Summary:    Gets match update values.
        ///
        /// Returns:    The match update values.
        #endregion
        public List<Internal.Models.MatchResult> GetMatchUpdateValues ()
        {
            return MatchResults;
        }
    }

    #region Documentation
    /// Class:  ChangeMatchStateRequest
    ///
    /// Summary:    (Serializable) a change match state request.
    #endregion
    [Serializable]
    public class ChangeMatchStateRequest : ChallongeBaseRequest
    {

        #region Documentation
        /// Enum:   MatchStateAction
        ///
        /// Summary:    Values that represent match state actions.
        #endregion
        public enum MatchStateAction
        {
            /// Summary:    An enum constant representing the reopen option.
            Reopen,

            /// Summary:    An enum constant representing the mark as underway option.
            MarkAsUnderway,

            /// Summary:    An enum constant representing the unmark as underway option.
            UnmarkAsUnderway
        }

        /// Summary:    The match state action.
        public MatchStateAction matchStateAction;

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Identifier for the match.
        public string matchID;

        public ChangeMatchStateRequest(string tournamentURL, string matchID, MatchStateAction matchStateAction, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/matches/" + matchID + ".json"))
        {
            this.matchStateAction = matchStateAction;
            this.tournamentURL = tournamentURL;
            this.matchID = matchID;
        }
    }
}
