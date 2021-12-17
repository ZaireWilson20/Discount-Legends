using Challonge.Models;
using System;

#region Documentation
/// Namespace:  Challonge
///
/// Summary:    .
#endregion
namespace Challonge.API
{
    #region Documentation
    /// Class:  ChallongeMatchesAPI
    ///
    /// Summary:    A challonge matches api.
    #endregion
    public static class Matches
    {
        #region Documentation
        /// Function:   GetAllMatchesAsync
        ///
        /// Summary:    Gets all matches asynchronous.
        ///
        /// Parameters:
        /// GetAllMatchesRequest -  The get all matches request.
        /// callbackMethod -        The callback method.
        #endregion
        public static void GetAllMatches(string accessToken, GetAllMatchesRequest GetAllMatchesRequest, Action<GetAllMatchesResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalMatch.GetAllMatches(accessToken, GetAllMatchesRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   GetMatchAsync
        ///
        /// Summary:    Gets match asynchronous.
        ///
        /// Parameters:
        /// GetMatchRequest -   The get match request.
        /// callbackMethod -    The callback method.
        #endregion
        public static void GetMatch(string accessToken, GetMatchRequest GetMatchRequest, Action<GetMatchResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalMatch.GetMatch(accessToken, GetMatchRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   ChangeMatchStateAsync
        ///
        /// Summary:    Change match state asynchronous.
        ///
        /// Parameters:
        /// ChangeMatchStateRequest -   The change match state request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void ChangeMatchState(string accessToken, ChangeMatchStateRequest ChangeMatchStateRequest, Action<GetMatchResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalMatch.ChangeMatchState(accessToken, ChangeMatchStateRequest, callbackMethod));
        }

        #region Documentation
        /// Function:   UpdateMatchScoresAsync
        ///
        /// Summary:    Updates the match scores asynchronous.
        ///
        /// Parameters:
        /// UpdateMatchScoresRequest -  The update match scores request.
        /// callbackMethod -            The callback method.
        #endregion
        public static void UpdateMatchScores(string accessToken, UpdateMatchScoresRequest UpdateMatchScoresRequest, Action<GetMatchResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalMatch.UpdateMatchScores(accessToken, UpdateMatchScoresRequest, callbackMethod));
        }
    }
}
