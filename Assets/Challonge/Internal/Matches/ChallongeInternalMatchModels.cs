using System;
using System.Collections.Generic;


namespace Challonge.Internal.Models
{
    public class GetAllMatchesResponse
    {
        public List<ResponseData> data = new List<ResponseData>();
        public List<ResponseData> included = new List<ResponseData>();
        public Meta meta { get; set; }
        public Links links { get; set; }

        public Challonge.Models.GetAllMatchesResponse Convert(string tournamentURL)
        {
            Challonge.Models.GetAllMatchesResponse getMatchesResponse = new Challonge.Models.GetAllMatchesResponse();

            // Iterate through each data reponse and convert to Match class
            for (int i = 0; i < this.data.Count; i++)
            {
                Challonge.Models.Match newMatch = ChallongeParse.ParseMatchData(this.data[i], null, included);
                newMatch.tournamentURL = tournamentURL;
                getMatchesResponse.Matches.Add(newMatch);
            }

            return getMatchesResponse;
        }
    }

    public class GetMatchResponse
    {
        public ResponseData data { get; set; }
        public List<ResponseData> included = new List<ResponseData>();

        public Challonge.Models.GetMatchResponse Convert(string tournamentURL)
        {
            Challonge.Models.GetMatchResponse getMatchResponse = new Challonge.Models.GetMatchResponse();
            getMatchResponse.match = ChallongeParse.ParseMatchData(this.data, null, included);
            getMatchResponse.match.tournamentURL = tournamentURL;
            return getMatchResponse;
        }
    }

    public class GetMatchAttachmentResponse
    {
        public int id { get; set; }
        public string type { get; set; }
        public Attributes attributes { get; set; }
    }

    public class UpdateMatchResponse
    {
        public string type { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Matches
    {
        public List<ResponseData> data = new List<ResponseData>();
        public Links links { get; set; }
    }
}
