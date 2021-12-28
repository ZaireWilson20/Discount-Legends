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
    public class CreateTournament : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public CreateTournamentParams createTournamentParams;

        public bool startTournamentAfterCreated = true;

        public TournamentEvent onCreateTournamentSuccess;

        public UnityEvent onError;

        /// Function:   CreateTournament
        ///
        /// Summary:    Creates a tournament.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// accessToken -       The access token.
        /// tournament -        The tournament.
        /// scope -             The scope.
        /// callbackMethod -    The callback method.
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

            Challonge.API.Tournaments.CreateTournament(accessToken, new CreateTournamentRequest(createTournamentParams, scope), (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    onCreateTournamentSuccess.Raise(response.Tournament);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }

        public void Send(List<Participant> participants)
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

            Challonge.API.Tournaments.CreateTournament(accessToken, new CreateTournamentRequest(createTournamentParams, scope), (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    Challonge.API.Participants.AddParticipants(accessToken, new CreateParticipantsRequest(response.Tournament.url, participants, scope), (participantsResponse) => 
                    {
                        if(participantsResponse.Result == Result.SUCCESS)
                        {
                            if (startTournamentAfterCreated)
                            {
                                Challonge.API.Tournaments.ChangeTournamentState(accessToken, new ChangeTournamentStateRequest(TournamentStateAction.start, response.Tournament.url, scope), (tournamentResponse) =>
                                {
                                    if (tournamentResponse.Result == Result.SUCCESS)
                                        onCreateTournamentSuccess.Raise(tournamentResponse.Tournament);
                                    else
                                    {
                                        onError.Invoke();
                                        //onCreateTournamentSuccess.Raise(response.Tournament);                                   
                                    }
                                });
                            }
                            else
                            {
                                API.Tournaments.GetTournament(accessToken, new GetTournamentRequest(response.Tournament.url, scope), (tournamentResponse) =>
                                {
                                    if (tournamentResponse.Result == Result.SUCCESS)
                                        onCreateTournamentSuccess.Raise(tournamentResponse.Tournament);
                                    else
                                    {
                                        onError.Invoke();
                                        //onCreateTournamentSuccess.Raise(response.Tournament);                                   
                                    }
                                });
                            }
                        }
                        else
                        {
                            //onCreateTournamentSuccess.Raise(response.Tournament);
                            onError.Invoke();
                        }
                    });

                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
