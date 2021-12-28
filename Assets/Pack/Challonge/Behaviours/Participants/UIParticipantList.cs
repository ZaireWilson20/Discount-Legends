using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Challonge.Models;
using Challonge.Properties;

namespace Challonge.Behaviours.UI
{
    public class UIParticipantList : MonoBehaviour
    {
        public GameObject parentGameObject;

        public GameObject uiParticipantItemPrefab;

        public int maxParticipants = 10;

        public void ShowParticipants(List<Models.Participant> participants)
        {
            List<UIParticipantItem> items = new List<UIParticipantItem>(parentGameObject.transform.GetComponentsInChildren<UIParticipantItem>(true));

            List<Challonge.Models.Participant> participantsFinal = new List<Challonge.Models.Participant>();

            for (int i = 0; i < participants.Count; i++)
            {
                participantsFinal.Add(participants[i]);
            }

            for (int i = 0; i < items.Count; i++)
                DestroyImmediate(items[i].gameObject);

            for (int i = 0; i < participantsFinal.Count; i++)
            {
                if (i == maxParticipants)
                    break;
                UIParticipantItem item = GameObject.Instantiate<GameObject>(uiParticipantItemPrefab).GetComponent<UIParticipantItem>();
                item.transform.parent = transform;
                item.ShowParticipant(participantsFinal[i]);
                item.uIParticipantList = this;
            }
        }
    }
}
