using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Challonge.Models;
using Challonge.Properties;
using System;
using Challonge.Behaviours;

namespace Challonge.Behaviours.UI
{
    public class UIOneVersusOneMatchItem : UIMatchItem
    {
        public TextMeshProUGUI seedA;
        public TextMeshProUGUI seedB;
        public TextMeshProUGUI participantA;
        public TextMeshProUGUI participantB;

        /// Function:   ShowMatch
        ///
        /// Summary:    Shows the match.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// match -     Specifies the match.
        public override void ShowMatch(Match match)
        {
            this.match = match;
            if (seedA != null)
                seedA.text = "";
            if (seedB != null)
                seedB.text = "";
            if (participantA != null)
                participantA.text = "";
            if (participantB != null)
                participantB.text = "";

            for (int i = 0; i < match.Participants.Count; i++)
            {
                if (i == 0 || i % 2 == 0)
                {
                    if (match.Participants[i].seed.HasValue && seedA != null)
                        seedA.text = match.Participants[i].seed.Value.ToString();
                    if (participantA != null)
                        this.participantA.text = match.Participants[i].name;
                }

                else if (i == 1 || i % 2 != 0)
                {
                    if (match.Participants[i].seed.HasValue && seedB != null)
                        seedB.text = match.Participants[i].seed.Value.ToString();
                    if (participantB != null)
                        this.participantB.text = match.Participants[i].name;
                }
            }
        }
    }
}