using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    [CreateAssetMenu(menuName = "Challonge/Params/Create Free For All Params")]

    public class FreeForAllParams : CreateTournamentParams
    {
        public Models.FreeForAllOptions FreeForAllOptions;

        void Reset()
        {
            tournamentType = TournamentType.FreeForAll;
        }
    }
}
