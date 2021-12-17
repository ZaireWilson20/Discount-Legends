using System;
using System.Collections.Generic;
using UnityEngine;
using Challonge.Behaviours;
using Challonge.Behaviours.EventListeners;

namespace Challonge
{
    [CreateAssetMenu(menuName = "Challonge/Events/Participant List Event")]
    public class ParticipantListEvent : ScriptableObject
    {
        public List<ParticipantListEventListener> listeners = new List<ParticipantListEventListener>();

        public void Raise(List<Models.Participant> participantList)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(participantList);
        }

        public void RegisterListener(ParticipantListEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ParticipantListEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
