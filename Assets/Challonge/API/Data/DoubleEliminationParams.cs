using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    [CreateAssetMenu(menuName = "Challonge/Params/Create Double Elimination Params")]
    public class DoubleEliminationParams : CreateTournamentParams
    {
        public Models.DoubleEliminationOptions DoubleEliminationOptions;

        void Reset()
        {
            tournamentType = TournamentType.DoubleElimination;
        }
    }
}
