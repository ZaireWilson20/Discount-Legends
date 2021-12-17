using System;
using System.Collections.Generic;

#region Documentation
/// Namespace:  Challonge.Properties
///
/// Summary:    .
#endregion
namespace Challonge.Properties
{
    #region Documentation
    /// Enum:   Result
    ///
    /// Summary:    Values that represent results.
    #endregion
    public enum Result
    {
        /// Summary:    An enum constant representing the success option.
        SUCCESS,

        /// Summary:    An enum constant representing the connection error option.
        CONNECTION_ERROR,

        /// Summary:    An enum constant representing the failed authentication option.
        FAILED_AUTHENTICATION,

        /// Summary:    An enum constant representing the data processing error option.
        DATA_PROCESSING_ERROR,

        /// Summary:    An enum constant representing the protocal error option.
        PROTOCAL_ERROR,

        /// Summary:    An enum constant representing the httppost error option.
        HTTPPOST_ERROR
    }

    public enum Scope
    {
        User,
        Application
    }

    [Serializable]
    public class Permissions
    {
        public bool readTournaments = true;

        public bool writeTournaments = true;

        public bool readMatches = true;

        public bool writeMatches = true;

        public bool readParticipants = true;

        public bool writeParticipants = true;

        public bool readMatchAttachements = true;

        public bool writeMatchAttachements = true;
    }
}