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
    public class UpdateScoresForMatch : MonoBehaviour
    {
        public Scope scope;

        public ChallongeApplication applicationData;

        public API.Data.ChallongeUser challongeUser;

        public API.Data.ChallongeMatch liveMatchData;

        public MatchEvent onMatchScoreUpdated;

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

            UpdateMatchScoresRequest updateMatchScoresRequest = new UpdateMatchScoresRequest(liveMatchData.tournamentURL, liveMatchData.matchId, liveMatchData.GetMatchResults(), scope);

            API.Matches.UpdateMatchScores(accessToken, updateMatchScoresRequest, (response) =>
            {
                if (response.Result == Result.SUCCESS)
                {
                    onMatchScoreUpdated.Raise(response.match);
                }
                else
                {
                    onError.Invoke();
                }
            });
        }
    }
}
