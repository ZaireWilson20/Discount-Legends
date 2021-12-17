using Challonge.Properties;
using UnityEngine;
using System.Collections.Generic;
using System;
using Challonge.API.Data;
using Challonge.Models;
using Challonge.Behaviours.UI;
using UnityEngine.Events;

namespace Challonge.Behaviours.Matches
{
    public class GetAllMatches : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Name of the tournament.
        public string tournamentName;

        /// Summary:    Type of the tournament.
        public TournamentType tournamentType;

        /// Summary:    State of the match.
        public GetAllMatchesRequest.MatchState matchState = GetAllMatchesRequest.MatchState.Any;

        /// Summary:    Identifier for the participant.
        public string participantID = "";

        /// Summary:    Number of pages.
        public int pageCount = 1;

        /// Summary:    The total items per page.
        public int totalItemsPerPage = 25;

        public MatchListEvent onGetMatchListSuccess;

        public UnityEvent onError;

        public void Send(UITournamentItem uITournamentItem)
        {
            tournamentURL = uITournamentItem.tournament.url;
            tournamentName = uITournamentItem.tournament.name;
            tournamentType = uITournamentItem.tournament.tournamentType;
            Send();
        }

        public void Send(Models.Tournament tournament)
        {
            tournamentURL = tournament.url;
            tournamentName = tournament.name;
            tournamentType = tournament.tournamentType;
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

            GetAllMatchesRequest request = new GetAllMatchesRequest(tournamentURL, scope);
            request.matchState = this.matchState;
            request.pageCount = this.pageCount;
            request.participantID = this.participantID;
            request.totalItemsPerPage = this.totalItemsPerPage;

            API.Matches.GetAllMatches(accessToken, new GetAllMatchesRequest(tournamentURL, scope), (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    for(int i = 0; i < response.Matches.Count; i++)
                    {
                        response.Matches[i].tournamentName = tournamentName;
                        response.Matches[i].tournamentType = tournamentType;
                    }
                    onGetMatchListSuccess.Raise(response.Matches);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
