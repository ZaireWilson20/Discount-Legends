using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Challonge Event")]
    public class ChallongeEvent : ScriptableObject
    {
        public List<ChallongeEventListener> listeners = new List<ChallongeEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(ChallongeEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ChallongeEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
