using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    [CreateAssetMenu(menuName = "Challonge/Params/Create Round Robin Params")]
    public class RoundRobinParams : CreateTournamentParams
    {
        public Models.RoundRobinOptions RoundRobinOptions;

        void Reset()
        {
            tournamentType = TournamentType.RoundRobin;
        }
    }
}
