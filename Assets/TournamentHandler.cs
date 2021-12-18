using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Models;

public class TournamentHandler : MonoBehaviour
{
    [SerializeField]Tournament _tournament;
    [SerializeField] Challonge.API.Data.ChallongeUser _user;
    [SerializeField] Challonge.API.Data.ChallongeMatch _match; 


    public void CacheTournamet(Tournament t)
    {
        _tournament = t; 

    }

    public Tournament GetTournament()
    {
        return _tournament;
    }


    public void GetMatches(List<Match> matches)
    {
        Debug.Log("Getting Matches: " + matches.Count);
        matches[0].tournamentType = Challonge.Properties.TournamentType.Leaderboard; 
        for(int i  = matches[0].Participants.Count - 1; i >= 0; i--)
        {
            if(matches[0].Participants[i].username != _user.user.username)
            {
                matches[0].Participants.RemoveAt(i); 
            }
        }

        _match.ApplyMatchSettings(matches[0]);
    }
}
