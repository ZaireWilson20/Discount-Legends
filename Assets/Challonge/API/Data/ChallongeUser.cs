using UnityEngine;
using Challonge.Models;

namespace Challonge.API.Data
{
    [CreateAssetMenu(fileName = "Challonge User", menuName = "Challonge/Credentials/Challonge User")]
    public class ChallongeUser : ScriptableObject
    {
        public Models.ChallongeUser user;

        /// Summary:    The permissions.
        public Properties.Permissions Permissions;
    }
}
