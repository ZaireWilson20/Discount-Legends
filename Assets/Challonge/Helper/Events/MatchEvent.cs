using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Match Event")]
    public class MatchEvent : ScriptableObject
    {
        public List<MatchEventListener> listeners = new List<MatchEventListener>();

        public void Raise(Models.Match match)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(match);
        }

        public void RegisterListener(MatchEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(MatchEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
