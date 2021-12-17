using System;
using System.Collections.Generic;
using System.Linq;
using Challonge;
using Challonge.Models;
using Challonge.Properties;
using UnityEngine;

/// Namespace:  Challonge.API.Data
///
/// Summary:    .
namespace Challonge.API.Data
{
    /// Class:  MatchData
    ///
    /// Summary:    A match data.
    ///
    /// Author: Ahmed
    [CreateAssetMenu(menuName = "Challonge/API/Challonge Match")]
    public class ChallongeMatch : ScriptableObject
    {
        /// Summary:    Identifier for the match.
        public string matchId;

        /// Summary:    Type of the tournament.
        public TournamentType tournamentType;

        /// Summary:    Name of the tournament.
        public string tournamentName;

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    The high score.
        public int highScore;

        /// Summary:    The round.
        public int round;

        /// Summary:    The total participants.
        public int totalParticipants;

        /// Summary:    List of participants.
        public List<Participant> participantList;

        public void ApplyMatchSettings(Models.Match match)
        {
            this.matchId = match.id;
            this.round = match.round;
            this.tournamentType = match.tournamentType;
            this.tournamentURL = match.tournamentURL;
            this.tournamentName = match.tournamentName;
            this.totalParticipants = match.Participants.Count;
            this.participantList = match.Participants;
        }

        public List<MatchResult> GetMatchResults()
        {
            SortParticipantsByRank();

            List<MatchResult> matchResults = new List<MatchResult>();

            for (int i = 0; i < participantList.Count; i++)
                matchResults.Add(participantList[i].matchResult);

            return matchResults;
        }

        public void SortParticipantsByRank()
        {
            this.participantList = participantList.OrderByDescending(o => o.matchResult.score).ToList();
            for (int i = 0; i < this.participantList.Count; i++)
                participantList[i].matchResult.rank = i + 1;
        }
    }
}
