using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Tournament Event")]
    public class TournamentEvent : ScriptableObject
    {
        public List<TournamentEventListener> listeners = new List<TournamentEventListener>();

        public void Raise(Models.Tournament tournament)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(tournament);
        }

        public void RegisterListener(TournamentEventListener listener)
        {
            listeners.Add(listener);
        }
        
        public void UnregisterListener(TournamentEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
