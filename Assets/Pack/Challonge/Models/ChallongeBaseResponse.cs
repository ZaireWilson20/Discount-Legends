using System;
using System.Collections.Generic;

#region Documentation
/// Namespace:  Challonge.Models
///
/// Summary:    .
#endregion
namespace Challonge.Models
{
    #region Documentation
    /// Class:  ChallongeBaseResponse
    ///
    /// Summary:    (Serializable) a challonge base response.
    #endregion
    [Serializable]
    public class ChallongeBaseResponse
    {
        /// Summary:    The result.
        public Properties.Result Result;

        /// Summary:    Message describing the request result.
        public string requestResultMessage;

        /// Summary:    Message describing the error request result.
        public string errorMessage;

        /// Summary:    The links.
        public Links links;
    }
}
