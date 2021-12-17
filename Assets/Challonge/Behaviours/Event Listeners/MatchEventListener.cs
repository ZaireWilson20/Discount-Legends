using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Behaviours.EventListeners
{
    public class MatchEventListener : MonoBehaviour
    {
        public MatchEvent Event;
        public MatchUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Models.Match match)
        {
            Response.Invoke(match);
        }
    }
}
