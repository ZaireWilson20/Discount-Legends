using System;
using Challonge.Models;

#region Documentation
/// Namespace:  Challonge
///
/// Summary:    .
#endregion
namespace Challonge.API
{
    #region Documentation
    /// Class:  ChallongeParticipantsAPI
    ///
    /// Summary:    A challonge participants api.
    #endregion
    public static class Participants
    {
        #region Documentation
        /// Function:   GetAllTournamentParticipantsAsync
        ///
        /// Summary:    Gets all tournament participants asynchronous.
        ///
        /// Parameters:
        /// GetAllParticipantsRequest -     The get all participants request.
        /// callbackMethod -                The callback method.
        #endregion
        public static void GetAllTournamentParticipants(string accessToken, GetAllParticipantsRequest GetAllParticipantsRequest, Action<GetAllParticipantsResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalParticipant.GetAllParticipants(accessToken, GetAllParticipantsRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   GetTournamentParticipantAsync
        ///
        /// Summary:    Gets tournament participant asynchronous.
        ///
        /// Parameters:
        /// GetParticipantRequest -     The get participant request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void GetTournamentParticipant(string accessToken, GetParticipantRequest GetParticipantRequest, Action<GetParticipantResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalParticipant.GetParticipant(accessToken, GetParticipantRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   AddParticipant
        ///
        /// Summary:    Adds a participant to 'callbackMethod'.
        ///
        /// Parameters:
        /// AddParticipantRequest -     The add participant request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void AddParticipant(string accessToken, CreateParticipantRequest AddParticipantRequest, Action<GetParticipantResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalParticipant.CreateParticipant(accessToken, AddParticipantRequest, callbackMethod);
        }

        #region Documentation
        /// Function:   AddParticipants
        ///
        /// Summary:    Adds the participants to 'callbackMethod'.
        ///
        /// Parameters:
        /// AddParticipantsRequest -    The add participants request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void AddParticipants(string accessToken, CreateParticipantsRequest AddParticipantsRequest, Action<GetAllParticipantsResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalParticipant.CreateParticipants(accessToken, AddParticipantsRequest, callbackMethod);
        }

        #region Documentation
        /// Function:   DeleteParticipantAsync
        ///
        /// Summary:    Deletes the participant asynchronous.
        ///
        /// Parameters:
        /// DeleteParticipantRequest -  The delete participant request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void DeleteParticipant(string accessToken, DeleteParticipantRequest DeleteParticipantRequest, Action<ChallongeBaseResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalParticipant.DeleteParticipant(accessToken, DeleteParticipantRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   ClearParticipantsAsync
        ///
        /// Summary:    Clears the participants asynchronous.
        ///
        /// Parameters:
        /// ClearParticipantsRequest -  The clear participants request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void ClearParticipants(string accessToken, ClearParticipantsRequest ClearParticipantsRequest, Action<ChallongeBaseResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalParticipant.ClearParticipants(accessToken, ClearParticipantsRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   RandomizeParticipants
        ///
        /// Summary:    Randomize participants.
        ///
        /// Parameters:
        /// RandomizeParticipantsRequest -  The randomize participants request.
        /// callbackMethod -                The callback method.
        #endregion
        public static void RandomizeParticipants(string accessToken, RandomizeParticipantsRequest RandomizeParticipantsRequest, Action<GetAllParticipantsResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalParticipant.RandomizeParticipants(accessToken, RandomizeParticipantsRequest, callbackMethod));
        }
    }
}
