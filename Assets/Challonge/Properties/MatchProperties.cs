using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#region Documentation
/// Namespace:  Challonge.Properties
///
/// Summary:    .
#endregion
namespace Challonge.Properties
{
    #region Documentation
    /// Enum:   MatchState
    ///
    /// Summary:    Values that represent match states.
    #endregion
    public enum MatchState
    {
        Open,
        Pending,
        Complete
    }

    [Serializable]
    public class UIFreeforAllMatchSeeding
    {
        public TextMeshProUGUI seeding;

        public TextMeshProUGUI partipantName;
    }

}
