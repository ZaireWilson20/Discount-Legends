using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Challonge.Models;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;

namespace Challonge.Internal
{
    public static class Helper
    {
        public static Models.ResponseData TournamentToResponseData(Models.ResponseData responseData, Tournament Tournament)
        {
            responseData.type = "Tournaments";
            responseData.attributes = new Models.Attributes();
            responseData.attributes.name = Tournament.name;
            responseData.attributes.url = Tournament.url;
            responseData.attributes.tournament_type = GetTournamentTypeAsString(Tournament.tournamentType);
            responseData.attributes.@private = Tournament.isPrivate;
            if (Tournament.timestamps.startsAt.HasValue)
                responseData.attributes.starts_at = Tournament.timestamps.startsAt.Value.ToString("dd/M/yyyy hh:mm:ss tt");
            else
                responseData.attributes.starts_at = DateTime.Today.AddDays(1).ToString("dd/M/yyyy hh:mm:ss tt");
            responseData.attributes.start_time = responseData.attributes.starts_at;
            responseData.attributes.description = Tournament.description;
            responseData.attributes.notifications = new Models.Notifications();
            responseData.attributes.notifications.upon_matches_open = Tournament.notifyUponMatchesOpen;
            responseData.attributes.notifications.upon_tournament_ends = Tournament.notifyUponTournamentEnds;
            responseData.attributes.match_options = new Models.MatchOptions();
            responseData.attributes.match_options.accept_attachments = Tournament.acceptAttachments;
            responseData.attributes.match_options.third_place_match = Tournament.hasThirdPlaceMatch;
            responseData.attributes.registration_options = new Models.RegistrationOptions();
            // Should Validate Checkin Duration greater than 15
            if (Tournament.checkInDuration < 15)
                responseData.attributes.registration_options.check_in_duration = 15;
            else
                responseData.attributes.registration_options.check_in_duration = Tournament.checkInDuration;
            responseData.attributes.registration_options.open_signup = Tournament.openSignup;
            // Should Validate Sign Up Cap greater than 3
            if (responseData.attributes.registration_options.signup_cap < 3)
                responseData.attributes.registration_options.signup_cap = 100;
            else
                responseData.attributes.registration_options.signup_cap = Tournament.signupCap;
            responseData.attributes.seeding_options = new Models.SeedingOptions();
            responseData.attributes.seeding_options.hide_seeds = Tournament.hideSeeds;
            responseData.attributes.seeding_options.sequential_pairings = Tournament.sequentialPairings;
            responseData.attributes.double_elimination_options = new Models.DoubleEliminationOptions();
            responseData.attributes.double_elimination_options.split_participants = Tournament.DoubleEliminationOptions.splitParticipants;
            responseData.attributes.double_elimination_options.grand_finals_modifier = GetGrandFinalsModifierAsString(Tournament.DoubleEliminationOptions.grandFinalsModifier);
            responseData.attributes.swiss_options = new Models.SwissOptions();
            responseData.attributes.swiss_options.pts_for_game_tie = Tournament.SwissOptions.pointsForGameTie;
            responseData.attributes.swiss_options.pts_for_game_win = Tournament.SwissOptions.pointsForGameWin;
            responseData.attributes.swiss_options.pts_for_match_tie = Tournament.SwissOptions.pointsForMatchTie;
            responseData.attributes.swiss_options.pts_for_match_win = Tournament.SwissOptions.pointsForMatchWin;
            responseData.attributes.swiss_options.rounds = Tournament.SwissOptions.rounds;
            responseData.attributes.round_robin_options = new Models.RoundRobinOptions();
            responseData.attributes.round_robin_options.iterations = Tournament.RoundRobinOptions.iterations;
            if (Tournament.RoundRobinOptions.ranking != RoundRobinOptions.Ranking.Default)
                responseData.attributes.round_robin_options.ranking = GetRankingAsString(Tournament.RoundRobinOptions.ranking);
            else
                responseData.attributes.round_robin_options.ranking = "";
            responseData.attributes.round_robin_options.pts_for_game_tie = Tournament.RoundRobinOptions.pointsForGameTie;
            responseData.attributes.round_robin_options.pts_for_game_win = Tournament.RoundRobinOptions.pointsForGameWin;
            responseData.attributes.round_robin_options.pts_for_match_tie = Tournament.RoundRobinOptions.pointsForMatchTie;
            responseData.attributes.round_robin_options.pts_for_match_win = Tournament.RoundRobinOptions.pointsForMatchWin;
            responseData.attributes.free_for_all_options = new Models.FreeForAllOptions();
            responseData.attributes.free_for_all_options.max_participants = Tournament.FreeForAllOptions.maxParticipants;
            return responseData;
        }

        public static string GetTournamentTypeAsString(Challonge.Properties.TournamentType tournamentType)
        {
            switch (tournamentType)
            {
                case Challonge.Properties.TournamentType.SingleElimination:
                    return Properties.ChallongeInternalProperties.SingleElimination;
                case Challonge.Properties.TournamentType.DoubleElimination:
                    return Properties.ChallongeInternalProperties.DoubleElimination;
                case Challonge.Properties.TournamentType.RoundRobin:
                    return Properties.ChallongeInternalProperties.RoundRobin;
                case Challonge.Properties.TournamentType.Swiss:
                    return Properties.ChallongeInternalProperties.Swiss;
                case Challonge.Properties.TournamentType.FreeForAll:
                    return Properties.ChallongeInternalProperties.FreeForAll;
                case Challonge.Properties.TournamentType.TimeTrail:
                    return Properties.ChallongeInternalProperties.TimeTrail;
                case Challonge.Properties.TournamentType.GrandPrix:
                    return Properties.ChallongeInternalProperties.GrandPrix;
            }

            return "";
        }

        public static Challonge.Properties.TournamentType ToTournamentType(Challonge.Properties.TournamentTypeFilter tournamentTypeFilter)
        {
            switch (tournamentTypeFilter)
            {
                case Challonge.Properties.TournamentTypeFilter.SingleElimination:
                    return Challonge.Properties.TournamentType.SingleElimination;
                case Challonge.Properties.TournamentTypeFilter.DoubleElimination:
                    return Challonge.Properties.TournamentType.DoubleElimination;
                case Challonge.Properties.TournamentTypeFilter.RoundRobin:
                    return Challonge.Properties.TournamentType.RoundRobin;
                case Challonge.Properties.TournamentTypeFilter.Swiss:
                    return Challonge.Properties.TournamentType.Swiss;
                case Challonge.Properties.TournamentTypeFilter.FreeForAll:
                    return Challonge.Properties.TournamentType.FreeForAll;
                case Challonge.Properties.TournamentTypeFilter.TimeTrail:
                    return Challonge.Properties.TournamentType.TimeTrail;
                case Challonge.Properties.TournamentTypeFilter.GrandPrix:
                    return Challonge.Properties.TournamentType.GrandPrix;
                default:
                    return Challonge.Properties.TournamentType.SingleElimination;
            }
        }

        public static string GetGrandFinalsModifierAsString(DoubleEliminationOptions.GrandFinalsModifier grandFinalsModifier)
        {
            switch (grandFinalsModifier)
            {
                case DoubleEliminationOptions.GrandFinalsModifier.Skip:
                    return Properties.ChallongeInternalProperties.Skip;
                case DoubleEliminationOptions.GrandFinalsModifier.SingleMatch:
                    return Properties.ChallongeInternalProperties.SingleMatch;
                default:
                    return "";
            }
        }

        public static DoubleEliminationOptions.GrandFinalsModifier GetGrandFinalsModifierFromString(string value)
        {
            switch (value)
            {
                case Properties.ChallongeInternalProperties.Skip:
                    return DoubleEliminationOptions.GrandFinalsModifier.Skip;
                case Properties.ChallongeInternalProperties.SingleMatch:
                    return DoubleEliminationOptions.GrandFinalsModifier.SingleMatch;
                default:
                    return DoubleEliminationOptions.GrandFinalsModifier.Default;
            }
        }

        public static RoundRobinOptions.Ranking GetRankingFromString(string value)
        {
            switch (value)
            {
                case Properties.ChallongeInternalProperties.MatchWins:
                    return RoundRobinOptions.Ranking.MatchWins;
                case Properties.ChallongeInternalProperties.GameWins:
                    return RoundRobinOptions.Ranking.GameWins;
                case Properties.ChallongeInternalProperties.GameWinPercentage:
                    return RoundRobinOptions.Ranking.GameWinPercentage;
                case Properties.ChallongeInternalProperties.PointsScored:
                    return RoundRobinOptions.Ranking.PointsScored;
                case Properties.ChallongeInternalProperties.PointsDifference:
                    return RoundRobinOptions.Ranking.PointsDifference;
                case Properties.ChallongeInternalProperties.Custom:
                    return RoundRobinOptions.Ranking.Custom;
                default:
                    return RoundRobinOptions.Ranking.Default;
            }
        }

        public static string GetRankingAsString(RoundRobinOptions.Ranking value)
        {
            switch (value)
            {
                case RoundRobinOptions.Ranking.Default:
                    return "";
                case RoundRobinOptions.Ranking.MatchWins:
                    return Properties.ChallongeInternalProperties.MatchWins;
                case RoundRobinOptions.Ranking.GameWins:
                    return Properties.ChallongeInternalProperties.GameWins;
                case RoundRobinOptions.Ranking.GameWinPercentage:
                    return Properties.ChallongeInternalProperties.GameWinPercentage;
                case RoundRobinOptions.Ranking.PointsScored:
                    return Properties.ChallongeInternalProperties.PointsScored;
                case RoundRobinOptions.Ranking.PointsDifference:
                    return Properties.ChallongeInternalProperties.PointsDifference;
                case RoundRobinOptions.Ranking.Custom:
                    return Properties.ChallongeInternalProperties.Custom;
                default:
                    return "";
            }
        }

        public static Models.Attributes ParticipantToAttributes(Participant participant)
        {
            Models.Attributes attributes = new Models.Attributes();
            
            attributes.name = participant.name;
            if (participant.email != "")
                attributes.email = participant.email;
            if (participant.seed.HasValue)
                attributes.seed = participant.seed.Value;
            else
                attributes.seed = 1;
            attributes.misc = participant.misc;
            if (participant.username != "")
                attributes.username = participant.username;

            return attributes;
        }

        public static List<Models.Attributes> ParticipantsToAttributesList(List<Participant> participants)
        {
            List<Models.Attributes> attributesList = new List<Models.Attributes>();
            
            for (int i = 0; i < participants.Count; i++)
                attributesList.Add(ParticipantToAttributes(participants[i]));
            return attributesList;
        }

        public static string RandomString(int length)
        {
            System.Random random = new System.Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
