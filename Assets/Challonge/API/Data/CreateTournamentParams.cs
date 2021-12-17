using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    public abstract class CreateTournamentParams : ScriptableObject
    {
        /// Summary:    Name of the tournament.
        public string tournamentName;

        /// Summary:    URL of the tournament.
        public string tournamentURL;

        /// Summary:    Type of the tournament.
        public TournamentType tournamentType;

        /// Summary:    Information describing the tournament.
        public string tournamentDescription;

        /// Summary:    True if is private, false if not.
        public bool isPrivate;

        /// Summary:    True if has third place match, false if not.
        public bool hasThirdPlaceMatch;

        /// Summary:    The signup capability.
        public int signupCap;

        /// Summary:    True to open signup.
        public bool openSignup;

        /// Summary:    Duration of the check in.
        public int checkInDuration;

        /// Summary:    True to hide, false to show the seeds.
        public bool hideSeeds;

        /// Summary:    True to sequential pairings.
        public bool sequentialPairings;

        /// Summary:    True to accept attachments.
        public bool acceptAttachments;

        /// Summary:    True to notify upon tournament ends.
        public bool notifyUponTournamentEnds;

        /// Summary:    True to notify upon matches open.
        public bool notifyUponMatchesOpen;
    }
}
