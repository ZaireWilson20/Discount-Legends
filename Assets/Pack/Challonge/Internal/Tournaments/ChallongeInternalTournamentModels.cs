using System;
using System.Collections.Generic;



namespace Challonge.Internal.Models
{
    public class GetAllTournamentsResponse
    {
        public List<ResponseData> data = new List<ResponseData>();
        public Meta meta;
        public Links links;
      
        public Challonge.Models.GetAllTournamentsResponse Convert()
        {
            Challonge.Models.GetAllTournamentsResponse getTournamentsResponse = new Challonge.Models.GetAllTournamentsResponse();

            // Iterate through each data reponse and convert to Tournament class
            for (int i = 0; i < this.data.Count; i++)
                getTournamentsResponse.Tournaments.Add(ChallongeParse.ParseTournamentData(this.data[i]));

            return getTournamentsResponse;
        }
    }

    public class GetTournamentResponse
    {
        public ResponseData data;
        public List<ResponseData> included = new List<ResponseData>();

        public Challonge.Models.GetTournamentResponse Convert()
        {
            Challonge.Models.GetTournamentResponse getTournamentResponse = new Challonge.Models.GetTournamentResponse();
            getTournamentResponse.Tournament = ChallongeParse.ParseTournamentData(this.data, this.included);
            return getTournamentResponse;
        }
    }

    public class Notifications
    {
        public bool upon_matches_open { get; set; }
        public bool upon_tournament_ends { get; set; }
    }

    public class MatchOptions
    {
        public bool third_place_match { get; set; }
        public bool accept_attachments { get; set; }
    }

    public class MatchResult
    {
        public string participant_id { get; set; }
        public string score_set { get; set; }
        public int? rank { get; set; }
        public bool? advancing { get; set; }
    }

    public class RegistrationOptions
    {
        public bool open_signup { get; set; }
        public int signup_cap { get; set; }
        public int check_in_duration { get; set; }
    }

    public class SeedingOptions
    {
        public bool hide_seeds { get; set; }
        public bool sequential_pairings { get; set; }
    }

    public class DoubleEliminationOptions
    {
        public bool split_participants { get; set; }
        public string grand_finals_modifier { get; set; }
    }

    public class RoundRobinOptions
    {
        public int iterations { get; set; }
        public string ranking { get; set; }
        public int pts_for_game_win { get; set; }
        public int pts_for_game_tie { get; set; }
        public int pts_for_match_win { get; set; }
        public int pts_for_match_tie { get; set; }
    }

    public class SwissOptions
    {
        public int rounds { get; set; }
        public int pts_for_game_win { get; set; }
        public int pts_for_game_tie { get; set; }
        public int pts_for_match_win { get; set; }
        public int pts_for_match_tie { get; set; }
    }

    public class FreeForAllOptions
    {
        public int max_participants { get; set; }
    }
}
