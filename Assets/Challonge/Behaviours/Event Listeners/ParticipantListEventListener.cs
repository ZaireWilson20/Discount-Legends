using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Behaviours.EventListeners
{
    public class ParticipantListEventListener : MonoBehaviour
    {
        public ParticipantListEvent Event;
        public ParticipantListUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(List<Models.Participant> participantList)
        {
            Response.Invoke(participantList);
        }
    }
}
