using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Models;
using TMPro;

public class CreateTournamentMenu : MonoBehaviour
{
    public Challonge.Behaviours.Tournament.CreateTournament createTournament;

    public TMP_InputField tournamentName;

    public GameObject parentParticipants;

    public void CreateTournamentWrapper()
    {
        Debug.Log("button pressed");

        List<Participant> participants = new List<Participant>();

        List<TMP_InputField> participantInputs = new List<TMP_InputField>(parentParticipants.GetComponentsInChildren<TMP_InputField>(true));

        for(int i = 0; i < participantInputs.Count; i++)
        {
            if(participantInputs[i].text != "")
            {
                Participant p = new Participant(participantInputs[i].text);
               // p.username = p.name;
                p.seed = i + 1;
                participants.Add(p);
            }
        }

        createTournament.createTournamentParams.tournamentName = tournamentName.text;
        createTournament.Send(participants);
    }
}
