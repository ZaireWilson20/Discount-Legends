using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Match List Event")]
    public class MatchListEvent : ScriptableObject
    {
        public List<MatchListEventListener> listeners = new List<MatchListEventListener>();

        public void Raise(List<Models.Match> MatchList)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(MatchList);
        }

        public void RegisterListener(MatchListEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(MatchListEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
