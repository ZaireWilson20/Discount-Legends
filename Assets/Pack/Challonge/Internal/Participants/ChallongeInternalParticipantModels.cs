using System;
using System.Collections.Generic;

namespace Challonge.Internal.Models
{
    public class GetAllParticipantsResponse
    {
        public List<ResponseData> data { get; set; }
        public Meta meta { get; set; }
        public Links links { get; set; }

        public Challonge.Models.GetAllParticipantsResponse Convert()
        {
            Challonge.Models.GetAllParticipantsResponse getParticipantsResponse = new Challonge.Models.GetAllParticipantsResponse();

            // Iterate through each data reponse and convert to Match class
            for (int i = 0; i < this.data.Count; i++)
                getParticipantsResponse.Participants.Add(ChallongeParse.ParseParticipantData(this.data[i]));

            return getParticipantsResponse;
        }
    }

    public class GetParticipantResponse
    {
        public ResponseData data { get; set; }
        public List<object> included { get; set; }

        public Challonge.Models.GetParticipantResponse Convert()
        {
            Challonge.Models.GetParticipantResponse getParticipantResponse = new Challonge.Models.GetParticipantResponse();
            getParticipantResponse.Participant = ChallongeParse.ParseParticipantData(this.data);
            return getParticipantResponse;
        }
    }

    public class Participants
    {
        public List<ResponseData> data = new List<ResponseData>();
        public Links links { get; set; }
    }

    public class States
    {
        public bool active { get; set; }
    }
}
