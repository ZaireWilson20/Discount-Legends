using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challonge.Internal
{
    public static class ChallongeParse
    {
        public static Challonge.Models.Tournament ParseTournamentData(Models.ResponseData responseData, List<Models.ResponseData> Included = null)
        {
            // Init Tournament
            Challonge.Models.Tournament tournament = new Challonge.Models.Tournament();

            // Set One-To-One Properties
            tournament.url = responseData.attributes.url;
            tournament.acceptAttachments = responseData.attributes.acceptAttachments.Value;
            tournament.description = responseData.attributes.description;
            if (responseData.attributes.thirdPlaceMatch.HasValue)
                tournament.hasThirdPlaceMatch = responseData.attributes.thirdPlaceMatch.Value;
            tournament.liveImageUrl = responseData.attributes.liveImageUrl;
            tournament.fullChallongeUrl = responseData.attributes.fullChallongeUrl;
            tournament.hideSeeds = responseData.attributes.hideSeeds.Value;
            tournament.id = responseData.id;
            tournament.isPrivate = responseData.attributes.@private.Value;
            tournament.name = responseData.attributes.name;
            tournament.notifyUponMatchesOpen = responseData.attributes.notifyUponMatchesOpen.Value;
            tournament.notifyUponTournamentEnds = responseData.attributes.notifyUponTournamentEnds.Value;
            tournament.openSignup = responseData.attributes.openSignup.Value;
            tournament.sequentialPairings = responseData.attributes.sequentialPairings.Value;

            // Set Nullable Properties
            if (responseData.attributes.checkInDuration.HasValue)
                tournament.checkInDuration = (int)responseData.attributes.checkInDuration;
            if (responseData.attributes.signupCap.HasValue)
                tournament.signupCap = (int)responseData.attributes.signupCap;
            int maxParticipantsOutValue = 0;
            int.TryParse(responseData.attributes.maxParticipants, out maxParticipantsOutValue);
            tournament.maxParticipantsPerMatch = maxParticipantsOutValue;

            // Set Tournament State
            switch (responseData.attributes.state)
            {
                case Properties.ChallongeInternalProperties.TournamentStateUnderway:
                    tournament.tournamentState = Challonge.Properties.TournamentState.Underway;
                    break;
                case Properties.ChallongeInternalProperties.TournamentStatePending:
                    tournament.tournamentState = Challonge.Properties.TournamentState.Pending;
                    break;
                default:
                    break;
            }

            // Set Tournament Type
            switch (responseData.attributes.tournamentType)
            {
                case Properties.ChallongeInternalProperties.SingleElimination:
                    tournament.tournamentType = Challonge.Properties.TournamentType.SingleElimination;
                    break;
                case Properties.ChallongeInternalProperties.DoubleElimination:
                    tournament.tournamentType = Challonge.Properties.TournamentType.DoubleElimination;
                    break;
                case Properties.ChallongeInternalProperties.RoundRobin:
                    tournament.tournamentType = Challonge.Properties.TournamentType.RoundRobin;
                    break;
                case Properties.ChallongeInternalProperties.Swiss:
                    tournament.tournamentType = Challonge.Properties.TournamentType.Swiss;
                    break;
                case Properties.ChallongeInternalProperties.FreeForAll:
                    tournament.tournamentType = Challonge.Properties.TournamentType.FreeForAll;
                    break;
                case Properties.ChallongeInternalProperties.TimeTrail:
                    tournament.tournamentType = Challonge.Properties.TournamentType.TimeTrail;
                    break;
                case Properties.ChallongeInternalProperties.GrandPrix:
                    tournament.tournamentType = Challonge.Properties.TournamentType.GrandPrix;
                    break;
            }

            // Full Match and Participant Details
            if (Included != null)
            {
                for (int i = 0; i < Included.Count; i++)
                    if (Included[i].type == Properties.ChallongeInternalProperties.ParticipantType)
                        tournament.Participants.Add(ChallongeParse.ParseParticipantData(Included[i]));

                tournament.participantCount = tournament.Participants.Count;

                for (int i = 0; i < Included.Count; i++)
                    if (Included[i].type == Properties.ChallongeInternalProperties.MatchType)
                        tournament.Matches.Add(ChallongeParse.ParseMatchData(Included[i], tournament.Participants));

                for (int i = 0; i < Included.Count; i++)
                    if (Included[i].type == Properties.ChallongeInternalProperties.User)
                    {
                        tournament.host = Included[i].attributes.username;
                        tournament.hostImageURL = Included[i].attributes.imageUrl;
                    }
            }
            else
            {
                // Set Matches
                for (int i = 0; i < responseData.relationships.matches.data.Count; i++)
                    tournament.Matches.Add(ChallongeParse.ParseMatchData(responseData.relationships.matches.data[i]));

                // Set Participants
                for (int i = 0; i < responseData.relationships.participants.data.Count; i++)
                    tournament.Participants.Add(ChallongeParse.ParseParticipantData(responseData.relationships.participants.data[i]));
                if (responseData.relationships.participants.links.meta.count != null)
                    tournament.participantCount = Convert.ToInt32(responseData.relationships.participants.links.meta.count);
                else
                    tournament.participantCount = tournament.Participants.Count;
            }

            // Set Other Options
            if (responseData.attributes.double_elimination_options != null)
            {
                tournament.DoubleEliminationOptions.grandFinalsModifier = Helper.GetGrandFinalsModifierFromString(responseData.attributes.double_elimination_options.grand_finals_modifier);
                tournament.DoubleEliminationOptions.splitParticipants = responseData.attributes.double_elimination_options.split_participants;
            }

            if (responseData.attributes.round_robin_options != null)
            {
                tournament.RoundRobinOptions.iterations = responseData.attributes.round_robin_options.iterations;
                tournament.RoundRobinOptions.pointsForGameTie = responseData.attributes.round_robin_options.pts_for_game_tie;
                tournament.RoundRobinOptions.pointsForGameWin = responseData.attributes.round_robin_options.pts_for_game_win;
                tournament.RoundRobinOptions.pointsForMatchTie = responseData.attributes.round_robin_options.pts_for_match_tie;
                tournament.RoundRobinOptions.pointsForMatchWin = responseData.attributes.round_robin_options.pts_for_match_win;
                tournament.RoundRobinOptions.ranking = Helper.GetRankingFromString(responseData.attributes.round_robin_options.ranking);
            }

            if (responseData.attributes.swiss_options != null)
            {
                tournament.SwissOptions.pointsForGameTie = responseData.attributes.swiss_options.pts_for_game_tie;
                tournament.SwissOptions.pointsForGameWin = responseData.attributes.swiss_options.pts_for_game_win;
                tournament.SwissOptions.pointsForMatchTie = responseData.attributes.swiss_options.pts_for_match_tie;
                tournament.SwissOptions.pointsForMatchWin = responseData.attributes.swiss_options.pts_for_match_win;
                tournament.SwissOptions.rounds = responseData.attributes.swiss_options.rounds;
            }

            if (responseData.attributes.free_for_all_options != null)
            {
                tournament.FreeForAllOptions.maxParticipants = responseData.attributes.free_for_all_options.max_participants;
            }

            return tournament;
        }

        public static Challonge.Models.Match ParseMatchData(Models.ResponseData responseData, List<Challonge.Models.Participant> AllParticipantDetails = null, List<Models.ResponseData> Included = null)
        {
            Challonge.Models.Match match = new Challonge.Models.Match();
            match.id = responseData.id;

            if (responseData.attributes != null)
            {
                match.identifier = responseData.attributes.identifier;
                match.round = responseData.attributes.round.Value;
                match.scoreInSets = responseData.attributes.scoreInSets;
                match.scores = responseData.attributes.scores;

                switch (responseData.attributes.state)
                {
                    case Properties.ChallongeInternalProperties.matchStateOpen:
                        match.matchState = Challonge.Properties.MatchState.Open;
                        break;
                    case Properties.ChallongeInternalProperties.matchStateComplete:
                        match.matchState = Challonge.Properties.MatchState.Complete;
                        break;
                    case Properties.ChallongeInternalProperties.matchStatePending:
                        match.matchState = Challonge.Properties.MatchState.Pending;
                        break;
                    default:
                        break;
                }
            }

            // Participant Details
            //if (Included != null)
            //{
            //    for (int i = 0; i < Included.Count; i++)
            //        if (Included[i].type == Properties.ChallongeInternalProperties.ParticipantType)
            //            match.Participants.Add(ChallongeParse.ParseParticipantData(Included[i]));
            //}
            if (responseData.relationships != null)
            {
                if(responseData.relationships.player1 != null)
                {
                    for (int k = 0; k < Included.Count; k++)
                        if (Included[k].type == Properties.ChallongeInternalProperties.ParticipantType
                            && responseData.relationships.player1.data.id == Included[k].id)
                            match.Participants.Add(ChallongeParse.ParseParticipantData(Included[k]));
                }
                if (responseData.relationships.player2 != null)
                {
                    for (int k = 0; k < Included.Count; k++)
                        if (Included[k].type == Properties.ChallongeInternalProperties.ParticipantType
                            && responseData.relationships.player2.data.id == Included[k].id)
                            match.Participants.Add(ChallongeParse.ParseParticipantData(Included[k]));
                }
                if (responseData.relationships.participants != null)
                {
                    if (Included != null)
                    {
                        for (int i = 0; i < responseData.relationships.participants.data.Count; i++)
                        {

                            for (int k = 0; k < Included.Count; k++)
                                if (Included[k].type == Properties.ChallongeInternalProperties.ParticipantType
                                    && responseData.relationships.participants.data[i].id == Included[k].id)
                                    match.Participants.Add(ChallongeParse.ParseParticipantData(Included[k]));

                        }
                    }
                    else
                    {
                        for (int i = 0; i < responseData.relationships.participants.data.Count; i++)
                        {
                            if (AllParticipantDetails != null)
                                match.Participants.Add(GetParticipantDetails(responseData.relationships.participants.data[i].id, AllParticipantDetails));
                            else
                                match.Participants.Add(ChallongeParse.ParseParticipantData(responseData.relationships.participants.data[i]));
                        }
                    }
                }
            }

            return match;
        }

        public static Challonge.Models.Participant ParseParticipantData(Models.ResponseData responseData)
        {
            Challonge.Models.Participant participant = new Challonge.Models.Participant(responseData.attributes.name);
            participant.id = responseData.id;

            if (responseData.attributes != null)
            {
                //participant.finalRank = responseData.attributes.finalRank;
                // participant.groupId = responseData.attributes.groupId;
                participant.misc = responseData.attributes.misc;
                participant.seed = responseData.attributes.seed.Value;
                // participant. responseData.attributes
                // participant.states = this.data.attributes.states;
                participant.tournamentId = responseData.attributes.tournamentId.Value;
                participant.name = responseData.attributes.name;
                if (responseData.attributes.username != null)
                    participant.username = (string)responseData.attributes.username; // TODO: should check for null
            }
            return participant;
        }

        public static Challonge.Models.Participant GetParticipantDetails(string participantID, List<Challonge.Models.Participant> Participants)
        {
            for (int i = 0; i < Participants.Count; i++)
                if (Participants[i].id == participantID)
                    return Participants[i];
            return null;
        }
    }
}
