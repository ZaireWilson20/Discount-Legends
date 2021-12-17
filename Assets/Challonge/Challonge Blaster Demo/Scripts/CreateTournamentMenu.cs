using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Models;
using TMPro;

public class CreateTournamentMenu : MonoBehaviour
{
    public Challonge.Behaviours.Tournament.CreateTournament createTournament;

    public TMP_Text tournamentName;

    public GameObject parentParticipants;

    public void CreateTournamentWrapper()
    {
        Debug.Log("button pressed");

        List<Participant> participants = new List<Participant>();

        //List<TMP_InputField> participantInputs = parentParticipants.game//new List<TMP_InputField>(parentParticipants.GetComponentsInChildren<TMP_InputField>(true));


        foreach(Transform g in parentParticipants.transform)
        {
            PlayerListObject playa = g.gameObject.GetComponent<PlayerListObject>(); 
            if(playa != null & playa._playerName.text != "")
            {
                Participant p = new Participant(playa._playerName.text);
                participants.Add(p);

            }
        }

        /*
        for(int i = 0; i < participantInputs.Count; i++)
        {
            if(participantInputs[i].text != "")
            {
                Participant p = new Participant(participantInputs[i].text);
               // p.username = p.name;
                p.seed = i + 1;
                participants.Add(p);
            }
        }*/

        createTournament.createTournamentParams.tournamentName = tournamentName.text;
        Debug.Log(participants.Count);
        createTournament.Send(participants);
    }
}
