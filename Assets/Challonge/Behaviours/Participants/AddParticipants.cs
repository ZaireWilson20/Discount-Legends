using Challonge.Properties;
using UnityEngine;
using System.Collections.Generic;
using System;
using Challonge.API.Data;
using Challonge.Models;
using UnityEngine.Events;

namespace Challonge.Behaviours.Participants
{
    public class AddParticipants : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public string tournamentURL;

        public ParticipantListEvent onGetParticipantListSuccess;

        public UnityEvent onError;

        public void Send(Models.Tournament tournament, List<Participant> participants)
        {
            this.tournamentURL = tournament.url;
            Send(participants);
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

            API.Participants.AddParticipants(accessToken, new CreateParticipantsRequest(tournamentURL, participants, scope), (response) =>
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
