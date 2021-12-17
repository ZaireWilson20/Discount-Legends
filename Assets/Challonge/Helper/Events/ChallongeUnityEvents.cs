using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Challonge
{
    [System.Serializable]

    public class TournamentUnityEvent : UnityEvent<Models.Tournament> { }

    [System.Serializable]

    public class TournamentListUnityEvent : UnityEvent<List<Models.Tournament>> { }

    [System.Serializable]

    public class MatchUnityEvent : UnityEvent<Models.Match> { }

    [System.Serializable]

    public class MatchListUnityEvent : UnityEvent<List<Models.Match>> { }

    [System.Serializable]

    public class ParticipantUnityEvent : UnityEvent<Models.Participant> { }

    [System.Serializable]

    public class ParticipantListUnityEvent : UnityEvent<List<Models.Participant>> { }
}
