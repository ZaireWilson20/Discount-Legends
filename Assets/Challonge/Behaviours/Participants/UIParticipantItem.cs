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
    public class UIParticipantItem : MonoBehaviour
    {
        /// Summary:    Name of the participant.
        public TextMeshProUGUI participantName;

        /// Summary:    The image.
        public RawImage image;

        /// Summary:    List of i tournaments.
        [HideInInspector]
        public UIParticipantList uIParticipantList;

        /// Summary:    The scope.
        private Scope scope;

        public void ShowParticipant(Models.Participant participant)
        {
            if (participantName != null)
                participantName.text = participant.name;

            //if (image != null)
            //{
            //    Challonge.URL.GetTexture(participant.icon, (texture) =>
            //    {
            //        image.texture = texture;
            //    });
            //}
        }
    }
}