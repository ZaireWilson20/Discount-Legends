using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Challonge.Properties;

namespace Challonge.API.Data
{
    [CreateAssetMenu(menuName = "Challonge/Params/Leaderboard Params")]
    public class LeaderboardParams : CreateTournamentParams
    {
        void Reset()
        {
            tournamentType = TournamentType.Leaderboard;
        }
    }
}
