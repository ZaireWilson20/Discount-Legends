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
    /// Class:  GetTournament
    ///
    /// Summary:    List of get tournaments.
    ///
    /// Author: Ahmed
    public class GetTournament : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public string tournamentUrl;

        public UnityEvent onError;

        public TournamentEvent onGetTournamentSuccess;

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

            API.Tournaments.GetTournament(accessToken, new GetTournamentRequest(tournamentUrl, scope), (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    onGetTournamentSuccess.Raise(response.Tournament);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
