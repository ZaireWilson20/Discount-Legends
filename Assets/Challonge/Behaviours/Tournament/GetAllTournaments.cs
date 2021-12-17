using Challonge.Properties;
using UnityEngine;
using System.Collections.Generic;
using System;
using Challonge.API.Data;
using Challonge.Models;
using UnityEngine.Events;

/// Namespace:  Challonge.Behaviours.Tournament
///
/// Summary:    .
namespace Challonge.Behaviours.Tournament
{
    /// Class:  GetTournamentList
    ///
    /// Summary:    List of get tournaments.
    ///
    /// Author: Ahmed
    public class GetAllTournaments : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public GetTournamentsParams getTournamentsParams;

        public TournamentListEvent onGetTournamentListSuccess;

        public UnityEvent onError;

        public void Send()
        {
            string accessToken = "";

            switch (scope)
            {
                case Scope.User:
                    accessToken = challongeUser.user.accessToken;
                    break;
                case Scope.Application:
                    accessToken = applicationData.accessToken;
                    break;
            }

            API.Tournaments.GetAllTournaments(accessToken, new GetAllTournamentsRequest(scope), (response) =>
            {
                if(response.Result == Result.SUCCESS)
                {
                    onGetTournamentListSuccess.Raise(response.Tournaments);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
