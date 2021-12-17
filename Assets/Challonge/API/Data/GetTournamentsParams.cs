using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    [CreateAssetMenu(menuName = "Challonge/Params/Get Tournaments Params")]
    public class GetTournamentsParams : ScriptableObject
    {
        /// Summary:    State of the tournament.
        public Challonge.Models.GetAllTournamentsRequest.TournamentState tournamentState;

        /// Summary:    Type of the tournament.
        public TournamentTypeFilter tournamentType;

        /// Summary:    The created before date.
        public string createdBeforeDate = "";

        /// Summary:    The created after date.
        public string createdAfterDate = "";

        /// Summary:    Number of pages.
        public int pageCount = 1;

        /// Summary:    The total items per page.
        public int totalItemsPerPage = 25;
    }
}
