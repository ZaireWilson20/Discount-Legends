using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Tournament List Event")]
    public class TournamentListEvent : ScriptableObject
    {
        public List<TournamentListEventListener> listeners = new List<TournamentListEventListener>();

        public void Raise(List<Models.Tournament> tournamentList)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(tournamentList);
        }

        public void RegisterListener(TournamentListEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(TournamentListEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
