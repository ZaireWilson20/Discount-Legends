using Challonge.Properties;
using UnityEngine;
using System.Collections.Generic;
using System;
using Challonge.API.Data;
using Challonge.Models;
using UnityEngine.Events;

namespace Challonge.Behaviours.Participants
{
    public class GetAllParticipants : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public string tournamentURL;

        public int maxParticipants;

        public ParticipantListEvent onGetParticipantListSuccess;

        public UnityEvent onError;

        public void Send(Challonge.Behaviours.UI.UITournamentItem uITournamentItem)
        {
            this.tournamentURL = uITournamentItem.tournament.url;
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

            GetAllParticipantsRequest request = new GetAllParticipantsRequest(tournamentURL, scope);
            request.pageCount = maxParticipants;

            API.Participants.GetAllTournamentParticipants(accessToken, request, (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    onGetParticipantListSuccess.Raise(response.Participants);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
