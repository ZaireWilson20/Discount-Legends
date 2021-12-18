using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.API.Data;
using Challonge.Models;
public class DLAddParticipant : MonoBehaviour
{

    [SerializeField] TournamentHandler _tournamentHandler;
    [SerializeField] Challonge.API.Data.ChallongeUser _user; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPartic()
    {
        bool inList = false; 
        foreach(Participant p in _tournamentHandler.GetTournament().Participants)
        {
            if(p.username == _user.user.username)
            {
                inList = true;
                break; 
            }
        }

        if (!inList)
        {
            Participant p = new Participant(_user.user.username);
            List<Participant> participants = new List<Participant>();
            participants.Add(p);
            //p.email = _user.user.email;
           // p.username = _user.user.username;

            Challonge.API.Participants.AddParticipants(_user.user.accessToken, new CreateParticipantsRequest(_tournamentHandler.GetTournament().url, participants, Challonge.Properties.Scope.Application), (response)=> 
            {
                if(response.Result == Challonge.Properties.Result.SUCCESS) { Debug.Log("We lit my boys"); }
                else
                {
                    Debug.Log("We not lit guys");
                }
             });

           
            //Add user to Participant to list 
        }
    }



}
