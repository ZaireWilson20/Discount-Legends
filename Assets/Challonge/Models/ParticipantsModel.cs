using Challonge.Properties;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Documentation
/// Namespace:  Challonge.Models
///
/// Summary:    .
#endregion
namespace Challonge.Models
{

    #region Documentation
    /// Class:  Participant
    ///
    /// Summary:    (Serializable) a participant.
    #endregion
    [Serializable]
    public class Participant
    {
        /// Summary:    The name.
        public string name;

        /// Summary:    The username.
        public string username;

        private string _id;

        /// Summary:    The identifier.
        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
                matchResult.participantId = value;
            }
        }

        /// Summary:    The email.
        public string email;

        /// Summary:    The timestamps.
        public Timestamps timestamps;

        /// Summary:    The icon.
        public string imageUrl;

        /// Summary:    The image texture.
        public Texture imageTexture;

        /// Summary:    The seed.
        public int? seed;

        /// Summary:    The misc.
        public object misc;

        /// Summary:    The final rank.
        public int finalRank;

        /// Summary:    Identifier for the group.
        public string groupId;

        /// Summary:    Identifier for the tournament.
        public int tournamentId;

        /// Summary:    The states.
        public States states;

        /// Summary:    The match result.
        public MatchResult matchResult = new MatchResult();

        #region Documentation
        /// Function:   Participant
        ///
        /// Summary:    Constructor.
        ///
        /// Parameters:
        /// name -  The name.
        #endregion
        public Participant (string name)
        {
            this.name = name;
        }
    }

    #region Documentation
    /// Class:  GetAllParticipantsResponse
    ///
    /// Summary:    (Serializable) a get all participants response.
    #endregion
    [Serializable]
    public class GetAllParticipantsResponse : ChallongeBaseResponse
    {
        /// Summary:    The participants.
        public List<Participant> Participants = new List<Participant>();
    }


    #region Documentation
    /// Class:  GetParticipantResponse
    ///
    /// Summary:    (Serializable) a get participant response.
    #endregion
    [Serializable]
    public class GetParticipantResponse : ChallongeBaseResponse
    {

        /// Summary:    The participant.
        public Participant Participant;
    }

    #region Documentation
    /// Class:  GetAllParticipantsRequest
    ///
    /// Summary:    (Serializable) a get all participants request.
    #endregion
    [Serializable]
    public class GetAllParticipantsRequest : ChallongeBaseRequest
    {

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Number of pages.
        public int pageCount = 1;

        /// Summary:    The total items per page.
        public int totalItemsPerPage = 100;

        public GetAllParticipantsRequest(string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants.json"))
        {
            this.tournamentURL = tournamentURL;
        }
    }

    #region Documentation
    /// Class:  GetParticipantRequest
    ///
    /// Summary:    (Serializable) a get participant request.
    #endregion
    [Serializable]
    public class GetParticipantRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Identifier for the participant.
        public string participantID;

        public GetParticipantRequest(string tournamentURL, string participantID, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants/" + participantID +".json"))
        {
            this.tournamentURL = tournamentURL;
            this.participantID = participantID;
        }
    }

    #region Documentation
    /// Class:  CreateParticipantRequest
    ///
    /// Summary:    (Serializable) a create participant request.
    #endregion
    [Serializable]
    public class CreateParticipantRequest : ChallongeBaseRequest
    {

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    The participant.
        public Participant participant;

        public CreateParticipantRequest(string tournamentURL, Participant participant, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants.json"))
        {
            this.tournamentURL = tournamentURL;
            this.participant = participant;
        }
    }

    #region Documentation
    /// Class:  CreateParticipantsRequest
    ///
    /// Summary:    (Serializable) a create participants request.
    #endregion
    [Serializable]
    public class CreateParticipantsRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    The participants.
        public List<Participant> participants = new List<Participant>();

        public CreateParticipantsRequest(string tournamentURL, List<Participant> participants, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants/bulk_add.json"))
        {
            this.tournamentURL = tournamentURL;
            this.participants = participants;
        }
    }

    [Serializable]
    public class DeleteParticipantRequest : ChallongeBaseRequest
    {

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Identifier for the participant.
        public string participantID;

        public DeleteParticipantRequest(string tournamentURL, string participantID, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants/" + participantID + ".json"))
        {
            this.tournamentURL = tournamentURL;
            this.participantID = participantID;
        }
    }

    [Serializable]
    public class ClearParticipantsRequest : ChallongeBaseRequest
    {

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        public ClearParticipantsRequest(string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants/clear.json"))
        {
            this.tournamentURL = tournamentURL;
        }
    }

    [Serializable]
    public class RandomizeParticipantsRequest : ChallongeBaseRequest
    {
        /// Summary:    URL of the tournament.
        public string tournamentURL;

        public RandomizeParticipantsRequest(string tournamentURL, Scope scope) : base(URL.Generate(scope, "tournaments/" + tournamentURL + "/participants/randomize.json"))
        {
            this.tournamentURL = tournamentURL;
        }
    }
}
