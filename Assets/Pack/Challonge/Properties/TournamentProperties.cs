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
    /// Enum:   TournamentType
    ///
    /// Summary:    Values that represent tournament types.
    #endregion
    public enum TournamentType
    {
        /// Summary:    An enum constant representing the single elimination option.
        SingleElimination,

        /// Summary:    An enum constant representing the double elimination option.
        DoubleElimination,

        /// Summary:    An enum constant representing the leaderboard option.
        Leaderboard,
        
        /// Summary:    An enum constant representing the round robin option.
        RoundRobin,

        /// Summary:    An enum constant representing the swiss option.
        Swiss,

        /// Summary:    An enum constant representing the free for all option.
        FreeForAll,

        /// Summary:    An enum constant representing the time trail option.
        TimeTrail,

        /// Summary:    An enum constant representing the grand prix option.
        GrandPrix
    }


    #region Documentation
    /// Enum:   TournamentTypeFilter
    ///
    /// Summary:    Values that represent tournament type filters.
    #endregion
    public enum TournamentTypeFilter
    {

        /// Summary:    An enum constant representing any option.
        Any,

        /// Summary:    An enum constant representing the single elimination option.
        SingleElimination,

        /// Summary:    An enum constant representing the double elimination option.
        DoubleElimination,

        /// Summary:    An enum constant representing the leaderboard option.
        Leaderboard,

        /// Summary:    An enum constant representing the round robin option.
        RoundRobin,

        /// Summary:    An enum constant representing the swiss option.
        Swiss,

        /// Summary:    An enum constant representing the free for all option.
        FreeForAll,

        /// Summary:    An enum constant representing the time trail option.
        TimeTrail,

        /// Summary:    An enum constant representing the grand prix option.
        GrandPrix
    }
    
    #region Documentation
    /// Enum:   TournamentState
    ///
    /// Summary:    Values that represent tournament states.
    #endregion
    public enum TournamentState
    {
        /// Summary:    An enum constant representing the pending option.
        Pending,

        /// Summary:    An enum constant representing the underway option.
        Underway
    }

    #region Documentation
    /// Enum:   TournamentState
    ///
    /// Summary:    Values that represent tournament states.
    #endregion
    public enum TournamentStateFilter
    {
        /// Summary:    An enum constant representing the all option.
        All,

        /// Summary:    An enum constant representing the pending option.
        Pending,

        /// Summary:    An enum constant representing the underway option.
        Underway
    }


    #region Documentation
    /// Enum:   TournamentStateAction
    ///
    /// Summary:    Values that represent tournament state actions.
    #endregion
    public enum TournamentStateAction
    {
        /// Summary:    An enum constant representing the process checkin option.
        process_checkin,

        /// Summary:    An enum constant representing the abort checkin option.
        abort_checkin,

        /// Summary:    An enum constant representing the start option.
        start,

        /// Summary:    An enum constant representing the finalize option.
        finalize,

        /// Summary:    An enum constant representing the reset option.
        reset,

        /// Summary:    An enum constant representing the open predictions option.
        open_predictions
    }
}
