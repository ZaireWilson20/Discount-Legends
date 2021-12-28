using System;
using System.Collections.Generic;


namespace Challonge.Internal.Models
{
    public class ResponseData
    {
        public string id { get; set; }
        public string type { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }
        public Links links { get; set; }

        public Errors errors { get; set; }
    }

    public class ResponseDataWrapper
    {
        public ResponseData data;
        public List<ResponseData> included { get; set; }
        public Links links { get; set; }
    }

    public class ResponseDataListWrapper
    {
        public List<ResponseData> data;
    }

    [Serializable]
    public class Errors
    {
        public string detail;
        public int status;
    }

    public class Attributes
    {
        // tournament only
        public string url { get; set; }
        public string name { get; set; } // tournament, participant
        public string state { get; set; } // tournament, match
        public bool? @private { get; set; }
        public string description { get; set; }
        public string tournamentType { get; set; }
        public string tournament_type { get; set; } // create tournament only
        public bool? notifyUponTournamentEnds { get; set; }
        public bool? notifyUponMatchesOpen { get; set; }
        public bool? thirdPlaceMatch { get; set; }
        public bool? acceptAttachments { get; set; }
        public int? signupCap { get; set; }
        public bool? openSignup { get; set; }
        public int? checkInDuration { get; set; }
        public bool? hideSeeds { get; set; }
        public bool? sequentialPairings { get; set; }
        public Timestamps timestamps { get; set; } // tournament, match, participant
        public string maxParticipants { get; set; }
        public string fullChallongeUrl { get; set; }
        public string liveImageUrl { get; set; }
        public string permalink { get; set; }
        public string subdomain { get; set; }

        // Create Tournament only     
        public string starts_at { get; set; }
        public string start_time { get; set; }
        public Notifications notifications;
        public MatchOptions match_options { get; set; }
        public RegistrationOptions registration_options { get; set; }
        public SeedingOptions seeding_options { get; set; }
        public DoubleEliminationOptions double_elimination_options { get; set; }
        public RoundRobinOptions round_robin_options { get; set; }
        public SwissOptions swiss_options { get; set; }
        public FreeForAllOptions free_for_all_options { get; set; }

        // match only
        public int? round { get; set; }
        public string identifier { get; set; }
        public string scores { get; set; }
        public List<object> scoreInSets { get; set; }
        public object suggestedPlayOrder { get; set; }

        // participant only
        public object icon { get; set; }
        public int? seed { get; set; }
        public object misc { get; set; }
        public string username { get; set; }

        public string imageUrl { get; set; }
        public object finalRank { get; set; }
        public object groupId { get; set; }
        public int? tournamentId { get; set; }
        public States states { get; set; }

        public string email;
        public List<Attributes> participants { get; set; }

        // user only
        public string uid { get; set; }

        // update match
        public List<MatchResult> match { get; set; }
    }

    public class ParticipantAttributes
    {
        public string name { get; set; }
        public int seed { get; set; }
        public string misc { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class Timestamps
    {
        public DateTime? startsAt { get; set; } //
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? completedAt { get; set; }
        public DateTime? startedAt { get; set; } //
        public object underwayAt { get; set; } //
    }

    public class Meta
    {
        public object count { get; set; }
    }

    public class Relationships
    {
        public Matches matches { get; set; }
        public Participants participants { get; set; }
        public Attachments attachments { get; set; }
        public Invitation invitation { get; set; }
        public ResponseDataWrapper player1 { get; set; }
        public ResponseDataWrapper player2 { get; set; }
        public Tournament tournament { get; set; }
        public User user { get; set; }
    }

    public class Tournament
    {
    }

    public class User
    {
    }

    public class Links
    {
        public string related { get; set; }
        public Meta meta { get; set; }
        public string self { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
    }

    public class Attachments
    {
        public List<object> data { get; set; }
        public Links links { get; set; }
    }

    public class Invitation
    {
        public object data { get; set; }
    }
}
