using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Behaviours.EventListeners
{
    public class MatchListEventListener : MonoBehaviour
    {
        public MatchListEvent Event;
        public MatchListUnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(List<Models.Match> matchList)
        {
            Response.Invoke(matchList);
        }
    }
}
