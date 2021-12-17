using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Participant Event")]
    public class ParticipantEvent : ScriptableObject
    {
        public List<ParticipantEventListener> listeners = new List<ParticipantEventListener>();

        public void Raise(Models.Participant participant)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(participant);
        }

        public void RegisterListener(ParticipantEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ParticipantEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
