using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Behaviours.EventListeners
{
    public class ParticipantEventListener : MonoBehaviour
    {
        public ParticipantEvent Event;
        public ParticipantUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Models.Participant participant)
        {
            Response.Invoke(participant);
        }
    }
}
