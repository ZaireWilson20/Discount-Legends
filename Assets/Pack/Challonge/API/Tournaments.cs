using System;
using Challonge.Models;
using Challonge.Properties;

#region Documentation
/// Namespace:  Challonge
///
/// Summary:    .
#endregion
namespace Challonge.API
{
    #region Documentation
    /// Class:  ChallongeTournamentAPI
    ///
    /// Summary:    A challonge tournament API.
    #endregion
    public static class Tournaments
    { 
        /// Function:   CreateTournament
        ///
        /// Summary:    Creates a tournament.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// accessToken -               The access token.
        /// CreateTournamentRequest -   The create tournament request.
        /// callbackMethod -            The callback method.
        public static void CreateTournament(string accessToken, CreateTournamentRequest CreateTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalTournament.CreateTournament(accessToken, CreateTournamentRequest, callbackMethod);
        }

        /// Function:   GetTournament
        ///
        /// Summary:    Gets a tournament.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// GetTournamentRequest -  The get tournament request.
        /// callbackMethod -        The callback method.
        public static void GetTournament(string accessToken, GetTournamentRequest GetTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.GetTournament(accessToken, GetTournamentRequest, callbackMethod));
        }

        /// Function:   GetAllTournamentsAsync
        ///
        /// Summary:    Gets all tournaments asynchronous.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// accessToken -               The access token.
        /// GetAllTournamentsRequest -  The get all tournaments request.
        /// callbackMethod -            The callback method.
        public static void GetAllTournaments(string accessToken, GetAllTournamentsRequest GetAllTournamentsRequest, Action<GetAllTournamentsResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.GetAllTournaments(accessToken, GetAllTournamentsRequest, callbackMethod));
        }
 
        /// Function:   ChangeTournamentStateAsync
        ///
        /// Summary:    Change tournament state asynchronous.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// ChangeTournamentStateRequest -  The change tournament state request.
        /// callbackMethod -                The callback method.
        public static void ChangeTournamentState(string accessToken, ChangeTournamentStateRequest ChangeTournamentStateRequest, Action<GetTournamentResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.ChangeTournamentState(accessToken, ChangeTournamentStateRequest, callbackMethod));
        }

        /// Function:   ChangeTournamentStateAsync
        ///
        /// Summary:    Change tournament state asynchronous.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// tournamentStateAction -     The tournament state action.
        /// tournament -                The tournament.
        /// scope -                     The scope.
        /// callbackMethod -            The callback method.
        public static void ChangeTournamentState(string accessToken, TournamentStateAction tournamentStateAction, Tournament tournament, Scope scope, Action<GetTournamentResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.ChangeTournamentState(accessToken, new ChangeTournamentStateRequest(tournamentStateAction, tournament.url, scope), callbackMethod));
        }

        /// Function:   UpdateTournamentAsync
        ///
        /// Summary:    Updates the tournament asynchronous.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// tournament -        The tournament.
        /// scope -             The scope.
        /// callbackMethod -    The callback method.
        public static void UpdateTournament(string accessToken, Tournament tournament, Scope scope, Action<GetTournamentResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.UpdateTournament(accessToken, new UpdateTournamentRequest(tournament, scope), callbackMethod));
        }

        /// Function:   UpdateTournamentAsync
        ///
        /// Summary:    Updates the tournament asynchronous.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// updateTournamentRequest -   The update tournament request.
        /// callbackMethod -            The callback method.
        public static void UpdateTournament(string accessToken, UpdateTournamentRequest updateTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.UpdateTournament(accessToken, updateTournamentRequest, callbackMethod));
        }

        /// Function:   DeleteTournament
        ///
        /// Summary:    Deletes the tournament.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// tournamentURL -     URL of the tournament.
        /// callbackMethod -    The callback method.
        public static void DeleteTournament(string accessToken, string tournamentURL, Scope scope, Action<ChallongeBaseResponse> callbackMethod)
        {
            Internal.ChallongeMaster.Instance.CoroutineWrapper(Internal.API.ChallongeInternalTournament.DeleteTournament(accessToken, new DeleteTournamentRequest(tournamentURL, scope), callbackMethod));
        }
    }
}
