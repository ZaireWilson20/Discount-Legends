using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Behaviours.EventListeners
{
    public class TournamentEventListener : MonoBehaviour
    {
        public TournamentEvent Event;
        public TournamentUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Models.Tournament tournament)
        {
            Response.Invoke(tournament);
        }
    }
}
