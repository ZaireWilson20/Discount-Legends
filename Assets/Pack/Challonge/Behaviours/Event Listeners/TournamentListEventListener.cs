using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Behaviours.EventListeners
{
    public class TournamentListEventListener : MonoBehaviour
    {
        public TournamentListEvent Event;
        public TournamentListUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(List<Models.Tournament> tournamentList)
        {
            Response.Invoke(tournamentList);
        }
    }
}
