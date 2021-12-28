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
    /// Class:  CreateTournament
    ///
    /// Summary:    A create tournament.
    ///
    /// Author: Ahmed
    public class ChangeTournamentState : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public TournamentStateAction stateAction;

        public string tournamentURL;

        public TournamentEvent onChangeTournamentStateSuccess;

        public UnityEvent onError;

        public void Send(Behaviours.UI.UITournamentItem uITournamentItem)
        {
            this.tournamentURL = uITournamentItem.tournament.url;
            Send();
        }

        public void Send(Models.Tournament tournament)
        {
            this.tournamentURL = tournament.url;
            Send();
        }

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

            Challonge.API.Tournaments.ChangeTournamentState(accessToken, new ChangeTournamentStateRequest(stateAction, tournamentURL, scope), (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    onChangeTournamentStateSuccess.Raise(response.Tournament);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
