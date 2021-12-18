using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Models;

public class TournamentHandler : MonoBehaviour
{
    [SerializeField]Tournament _tournament;


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
    }
}
