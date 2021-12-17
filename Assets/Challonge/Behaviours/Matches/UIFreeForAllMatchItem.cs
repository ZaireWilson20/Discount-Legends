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
    public class UIFreeForAllMatchItem : UIMatchItem
    {
        public List<UIFreeforAllMatchSeeding> participants;

        public override void ShowMatch(Match match)
        {
            this.match = match;

            for (int i = 0; i < participants.Count; i++)
            {
                if(i >= match.Participants.Count)
                {
                    participants[i].partipantName.text = "";
                    participants[i].seeding.text = "";
                }
                else
                {
                    participants[i].partipantName.text = match.Participants[i].name;
                    participants[i].seeding.text = match.Participants[i].seed.Value.ToString();
                }
            }
        }
    }
}