using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Challonge;
using Challonge.API;
using Challonge.Models;
using Challonge.Properties;
using System;
using Challonge.Behaviours;

namespace Challonge.Behaviours.UI
{

    /// Class:  TournamentItem
    ///
    /// Summary:    A tournament item.
    ///
    /// Author: Ahmed
    public class UITournamentItem : MonoBehaviour
    {
        public Challonge.TournamentEvent OnTournamentSelectEvent;

        /// Summary:    Name of the tournament.
        public TextMeshProUGUI tournamentName;

        /// Summary:    Type of the tournament.
        public TextMeshProUGUI tournamentType;

        /// Summary:    The tournament status.
        public TextMeshProUGUI tournamentStatus;

        /// Summary:    The tournament participant total.
        public TextMeshProUGUI tournamentParticipantTotal;

        /// Summary:    The image.
        public RawImage image;

        /// Summary:    The tournament.
        public Models.Tournament tournament;

        /// Summary:    URL of the tournament.
        private string tournamentURL;

        /// Summary:    List of i tournaments.
        [HideInInspector]
        public UITournamentList uITournamentList;

        /// Summary:    The scope.
        private Scope scope;

        public void ShowTournament(Models.Tournament tournament)
        {
            if (tournamentName != null)
                tournamentName.text = tournament.name;
            if (tournamentType != null)
                tournamentType.text = tournament.tournamentType.ToString();
            if (tournamentStatus != null)
                tournamentStatus.text = tournament.tournamentState.ToString();
            if (tournamentParticipantTotal != null)
                tournamentParticipantTotal.text = tournament.participantCount.ToString();
            this.tournamentURL = tournament.url;
            this.tournament = tournament;

            if (image != null)
            {
                Challonge.URL.GetTexture(tournament.hostImageURL, (texture) =>
                {
                    image.texture = texture;
                });
            }
        }

        public void ClickButtonActions()
        {
            uITournamentList.onUiTournamentItemClickAction.Invoke(tournament);
            OnTournamentSelectEvent.Raise(tournament);
        }
    }
}