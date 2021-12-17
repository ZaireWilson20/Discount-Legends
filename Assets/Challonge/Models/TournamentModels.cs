using Challonge.Properties;
using Challonge.API.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/// Namespace:  Challonge.Models
///
/// Summary:    .
#endregion
namespace Challonge.Models
{
    #region Documentation
    /// Class:  Tournament
    ///
    /// Summary:    (Serializable) a tournament.
    #endregion
    [Serializable]
    public class Tournament
    {
        /// Summary:    The name.
        public string name; 

        /// Summary:    The identifier.
        public string id;

        /// Summary:    The host username.
        public string host;

        /// Summary:    URL of the host image.
        public string hostImageURL;

        /// Summary:    The timestamps.
        public Timestamps timestamps = new Timestamps();

        /// Summary:    URL of the resource.
        public string url;

        /// Summary:    The description.
        public string description;

        /// Summary:    URL of the full challonge.
        public string fullChallongeUrl;

        /// Summary:    URL of the live image.
        public string liveImageUrl;

        /// Summary:    State of the tournament.
        public TournamentState tournamentState;

        /// Summary:    Type of the tournament.
        public TournamentType tournamentType;

        /// Summary:    True if is private, false if not.
        public bool isPrivate;

        /// Summary:    True to notify upon tournament ends.
        public bool notifyUponTournamentEnds;

        /// Summary:    True to notify upon matches open.
        public bool notifyUponMatchesOpen;

        /// Summary:    True if has third place match, false if not.
        public bool hasThirdPlaceMatch;

        /// Summary:    True to accept attachments.
        public bool acceptAttachments;

        /// Summary:    The signup capability.
        public int signupCap;

        /// Summary:    True to open signup.
        public bool openSignup;

        /// Summary:    Duration of the check in.
        public int checkInDuration;

        /// Summary:    True to hide, false to show the seeds.
        public bool hideSeeds;

        /// Summary:    True to sequential pairings.
        public bool sequentialPairings;

        /// Summary:    A match specifying the maximum participants per.
        public int maxParticipantsPerMatch;

        /// Summary:    Number of participants.
        public int participantCount;

        /// Summary:    Information describing the recent request.
        public ChallongeBaseRequest RecentRequestInfo;

        /// Summary:    The participants.
        public List<Participant> Participants = new List<Participant>();

        /// Summary:    The matches.
        public List<Match> Matches = new List<Match>();

        /// Summary:    Options for controlling the double elimination.
        public DoubleEliminationOptions DoubleEliminationOptions = new DoubleEliminationOptions();

        /// Summary:    Options for controlling the round robin.
        public RoundRobinOptions RoundRobinOptions = new RoundRobinOptions();

        /// Summary:    Options for controlling the swiss.
        public SwissOptions SwissOptions = new SwissOptions();

        /// Summary:    Options for controlling the free for all.
        public FreeForAllOptions FreeForAllOptions = new FreeForAllOptions();
    }


    #region Documentation
    /// Class:  DoubleEliminationOptions
    ///
    /// Summary:    (Serializable) a double elimination options.
    #endregion
    [Serializable]
    public class DoubleEliminationOptions
    {
        /// Summary:    True to split participants.
        public bool splitParticipants = false;

        /// Summary:    The grand finals modifier.
        public GrandFinalsModifier grandFinalsModifier;

        #region Documentation
        /// Enum:   GrandFinalsModifier
        ///
        /// Summary:    Values that represent grand finals modifiers.
        #endregion
        public enum GrandFinalsModifier
        {
            Default,
            Skip,
            SingleMatch
        }
    }

    #region Documentation
    /// Class:  RoundRobinOptions
    ///
    /// Summary:    (Serializable) a round robin options.
    #endregion
    [Serializable]
    public class RoundRobinOptions
    {

        /// Summary:    The iterations.
        public int iterations = 2;

        /// Summary:    The ranking.
        public Ranking ranking;

        /// Summary:    The points for game window.
        public int pointsForGameWin = 1;

        /// Summary:    The points for game tie.
        public int pointsForGameTie = 0;

        /// Summary:    The points for match window.
        public int pointsForMatchWin = 1;

        /// Summary:    The points for match tie.
        public int pointsForMatchTie = 1;


        #region Documentation
        /// Enum:   Ranking
        ///
        /// Summary:    Values that represent rankings.
        #endregion
        public enum Ranking
        {
            Default,
            MatchWins,
            GameWins,
            GameWinPercentage,
            PointsScored,
            PointsDifference,
            Custom
        }
    }

    #region Documentation
    /// Class:  SwissOptions
    ///
    /// Summary:    (Serializable) the swiss options.
    #endregion
    [Serializable]
    public class SwissOptions
    {
        /// Summary:    The rounds.
        public int rounds = 2;

        /// Summary:    The points for game window.
        public int pointsForGameWin = 1;

        /// Summary:    The points for game tie.
        public int pointsForGameTie = 0;

        /// Summary:    The points for match window.
        public int pointsForMatchWin = 1;

        /// Summary:    The points for match tie.
        public int pointsForMatchTie = 1;
    }

    #region Documentation
    /// Class:  FreeForAllOptions
    ///
    /// Summary:    (Serializable) a free for all options.
    #endregion
    [Serializable]
    public class FreeForAllOptions
    {
        /// Summary:    The maximum participants.
        public int maxParticipants = 4;
    }

    #region Documentation
    /// Class:  GetAllTournamentsResponse
    ///
    /// Summary:    (Serializable) a get all tournaments response.
    #endregion
    [Serializable]
    public class GetAllTournamentsResponse : ChallongeBaseResponse
    {
        /// Summary:    The tournaments.
        public List<Tournament> Tournaments = new List<Tournament>();
    }

    #region Documentation
    /// Class:  GetTournamentResponse
    ///
    /// Summary:    (Serializable) a get tournament response.
    #endregion
    [Serializable]
    public class GetTournamentResponse : ChallongeBaseResponse
    {
        /// Summary:    The tournament.
        public Tournament Tournament;
    }



    #region Documentation
    /// Class:  GetAllTournamentsRequest
    ///
    /// Summary:    (Serializable) a get all tournaments request.
    #endregion
    [Serializable]
    public class GetAllTournamentsRequest : ChallongeBaseRequest
    {
        public GetAllTournamentsRequest(Scope scope) : base(URL.Generate(scope, "tournaments.json"))
        {

        }

        public GetAllTournamentsRequest(GetTournamentsParams getTournamentsParams, Scope scope) : base(URL.Generate(scope, "tournaments.json"))
        {
            this.tournamentState = getTournamentsParams.tournamentState;
            this.tournamentType = getTournamentsParams.tournamentType;
            this.createdAfterDate = getTournamentsParams.createdAfterDate;
            this.createdBeforeDate = getTournamentsParams.createdBeforeDate;
            this.pageCount = getTournamentsParams.pageCount;
            this.totalItemsPerPage = getTournamentsParams.totalItemsPerPage;
        }

        #region Documentation
        /// Enum:   TournamentState
        ///
        /// Summary:    Values that represent tournament states.
        #endregion
        public enum TournamentState
        {
            any,
            in_progress,
            pending,
            ended
        }

        /// Summary:    State of the tournament.
        public TournamentState tournamentState;

        /// Summary:    Type of the tournament.
        public TournamentTypeFilter tournamentType;

        /// Summary:    The created before date.
        public string createdBeforeDate = ""; 

        /// Summary:    The created after date.
        public string createdAfterDate = ""; 

        /// Summary:    Number of pages.
        public int pageCount = 1;

        /// Summary:    The total items per page.
        public int totalItemsPerPage = 25;
    }

    #region Documentation
    /// Class:  GetTournamentRequest
    ///
    /// Summary:    (Serializable) a get tournament request.
    #endregion
    [Serializable]
    public class GetTournamentRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        public GetTournamentRequest(string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + ".json"))
        {
            this.tournamentURL = tournamentURL;
        }
    }

    #region Documentation
    /// Class:  ChangeTournamentStateRequest
    ///
    /// Summary:    (Serializable) a change tournament state request.
    #endregion
    [Serializable]
    public class ChangeTournamentStateRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    The state action.
        public TournamentStateAction stateAction;

        public ChangeTournamentStateRequest(TournamentStateAction stateAction, string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/change_state.json"))
        {
            this.tournamentURL = tournamentURL;
            this.stateAction = stateAction;
        }
    }

    #region Documentation
    /// Class:  UpdateTournamentRequest
    ///
    /// Summary:    (Serializable) an update tournament request.
    #endregion
    [Serializable]
    public class UpdateTournamentRequest : ChallongeBaseRequest
    {
        /// Summary:    The tournament.
        public Tournament Tournament;

        public UpdateTournamentRequest(Tournament tournament, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournament.url + ".json"))
        {
            this.Tournament = tournament;
        }
    }

    #region Documentation
    /// Class:  CreateTournamentRequest
    ///
    /// Summary:    (Serializable) a create tournament request.
    #endregion
    [Serializable]
    public class CreateTournamentRequest : ChallongeBaseRequest
    {
        /// Summary:    The tournament.
        public Tournament Tournament = new Tournament();

        /// Function:   CreateTournamentRequest
        ///
        /// Summary:    Constructor.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// tournamentName -    Name of the tournament.
        /// tournamentURL -     URL of the tournament.
        /// tournamentType -    Type of the tournament.
        /// scope -             The scope.
        public CreateTournamentRequest(string tournamentName, string tournamentURL, TournamentType tournamentType, Scope scope) : base (URL.Generate(scope, "tournaments.json"))
        {
            Tournament.name = tournamentName;
            Tournament.url = tournamentURL;
            Tournament.tournamentType = tournamentType;
        }

        /// Function:   CreateTournamentRequest
        ///
        /// Summary:    Constructor.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// tournament -    The tournament.
        /// scope -         The scope.
        public CreateTournamentRequest(CreateTournamentParams createTournamentParams, Scope scope) : base(URL.Generate(scope, "tournaments.json"))
        {
            ParamsToTournament(createTournamentParams);
        }

        private void ParamsToTournament(CreateTournamentParams createTournamentParams)
        {
            this.Tournament.name = createTournamentParams.tournamentName;
            this.Tournament.description = createTournamentParams.tournamentDescription;
            this.Tournament.acceptAttachments = createTournamentParams.acceptAttachments;
            this.Tournament.checkInDuration = createTournamentParams.checkInDuration;
            this.Tournament.url = createTournamentParams.tournamentURL;
            this.Tournament.hasThirdPlaceMatch = createTournamentParams.hasThirdPlaceMatch;
            this.Tournament.hideSeeds = createTournamentParams.hideSeeds;
            this.Tournament.isPrivate = createTournamentParams.isPrivate;
            this.Tournament.notifyUponMatchesOpen = createTournamentParams.notifyUponMatchesOpen;
            this.Tournament.notifyUponTournamentEnds = createTournamentParams.notifyUponTournamentEnds;
            this.Tournament.openSignup = createTournamentParams.openSignup;
            this.Tournament.sequentialPairings = createTournamentParams.sequentialPairings;
            this.Tournament.signupCap = createTournamentParams.signupCap;
            this.Tournament.tournamentType = createTournamentParams.tournamentType;

            if (createTournamentParams.GetType() == typeof(DoubleEliminationParams))
                this.Tournament.DoubleEliminationOptions = ((DoubleEliminationParams)createTournamentParams).DoubleEliminationOptions;

            if (createTournamentParams.GetType() == typeof(RoundRobinParams))
                this.Tournament.RoundRobinOptions = ((RoundRobinParams)createTournamentParams).RoundRobinOptions;

            if (createTournamentParams.GetType() == typeof(SwissParams))
                this.Tournament.SwissOptions = ((SwissParams)createTournamentParams).SwissOptions;

            if (createTournamentParams.GetType() == typeof(FreeForAllParams))
                this.Tournament.FreeForAllOptions = ((FreeForAllParams)createTournamentParams).FreeForAllOptions;

            if (createTournamentParams.GetType() == typeof(SingleEliminationParams))
            {

            }
        }
    }

    #region Documentation
    /// Class:  DeleteTournamentRequest
    ///
    /// Summary:    (Serializable) a delete tournament request.
    #endregion
    [Serializable]
    public class DeleteTournamentRequest : ChallongeBaseRequest
    {
        public DeleteTournamentRequest(string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + ".json"))
        {

        }
    }
}
