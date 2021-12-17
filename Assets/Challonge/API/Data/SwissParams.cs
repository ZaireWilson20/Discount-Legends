using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    [CreateAssetMenu(menuName = "Challonge/Params/Create Swiss Params")]
    public class SwissParams : CreateTournamentParams
    {
        public Models.SwissOptions SwissOptions;

        void Reset()
        {
            tournamentType = TournamentType.Swiss;
        }
    }
}
