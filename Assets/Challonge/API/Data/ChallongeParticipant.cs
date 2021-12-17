using System;
using System.Collections.Generic;
using Challonge;
using Challonge.Properties;
using UnityEngine;

/// Namespace:  Challonge.API.Data
///
/// Summary:    .
namespace Challonge.API.Data
{
    /// Class:  ParticipantData
    ///
    /// Summary:    (Serializable) a participant data.
    ///
    /// Author: Ahmed
    [CreateAssetMenu(menuName = "Challonge/API/Participant Data")]
    [Serializable]
    public class ParticipantData 
    {
        /// Summary:    Identifier for the participant.
        public string participantId;

        /// Summary:    The name.
        public string name;

        /// Summary:    The username.
        public string username;

        /// Summary:    The icon.
        public Texture2D icon;

        /// Summary:    The seeding.
        public int? seeding;

        /// Summary:    The score.
        public int score;
    }
}
