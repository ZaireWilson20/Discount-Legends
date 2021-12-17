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
    /// Class:  ChallongeBaseRequest
    ///
    /// Summary:    (Serializable) a challonge base request.
    #endregion
    [Serializable]
    public class ChallongeBaseRequest
    {
        #region Documentation
        /// Function:   ChallongeBaseRequest
        ///
        /// Summary:    Constructor.
        ///
        /// Parameters:
        /// uri -   URI of the resource.
        #endregion
        public ChallongeBaseRequest(string uri)
        {
            this.uri = uri;
        }

        /// Summary:    (Immutable) type of the authorization.
        public readonly string authorizationType = "v2";

        /// Summary:    (Immutable) type of the content.
        public readonly string contentType = "application/vnd.api+json";

        /// Summary:    (Immutable) the accept value.
        public readonly string acceptValue = "application/json";

        /// Summary:    (Immutable) URI of the resource.
        public readonly string uri;

        /// Summary:    The request time.
        public DateTime requestTime = DateTime.Now;
    }
}
