using System;

#region Documentation
/// Namespace:  Challonge.Models
///
/// Summary:    .
#endregion
namespace Challonge.Models
{
    #region Documentation
    /// Class:  Timestamps
    ///
    /// Summary:    (Serializable) a timestamps.
    #endregion
    [Serializable]
    public class Timestamps
    {
        /// Summary:    The starts at.
        public DateTime? startsAt;

        /// Summary:    The created at Date/Time.
        public DateTime createdAt;

        /// Summary:    The updated at Date/Time.
        public DateTime updatedAt;

        /// Summary:    The completed at.
        public DateTime? completedAt;
    }

    #region Documentation
    /// Class:  Links
    ///
    /// Summary:    (Serializable) a links.
    #endregion
    [Serializable]
    public class Links
    {
        #region Documentation
        /// Property:   related
        ///
        /// Summary:    Gets or sets the related.
        ///
        /// Returns:    The related.
        #endregion
        public string related { get; set; }

        #region Documentation
        /// Property:   meta
        ///
        /// Summary:    Gets or sets the meta.
        ///
        /// Returns:    The meta.
        #endregion
        public Meta meta { get; set; }

        #region Documentation
        /// Property:   self
        ///
        /// Summary:    Gets or sets the self.
        ///
        /// Returns:    The self.
        #endregion
        public string self { get; set; }

        #region Documentation
        /// Property:   next
        ///
        /// Summary:    Gets or sets the next.
        ///
        /// Returns:    The next.
        #endregion
        public string next { get; set; }

        #region Documentation
        /// Property:   prev
        ///
        /// Summary:    Gets or sets the previous.
        ///
        /// Returns:    The previous.
        #endregion
        public string prev { get; set; }
    }

    #region Documentation
    /// Class:  Meta
    ///
    /// Summary:    (Serializable) a meta.
    #endregion
    [Serializable]
    public class Meta
    {

        #region Documentation
        /// Property:   count
        ///
        /// Summary:    Gets or sets the number of. 
        ///
        /// Returns:    The count.
        #endregion
        public object count { get; set; }
    }

    #region Documentation
    /// Class:  Relationships
    ///
    /// Summary:    A relationships.
    #endregion
    public class Relationships
    {

        #region Documentation
        /// Property:   matches
        ///
        /// Summary:    Gets or sets the matches.
        ///
        /// Returns:    The matches.
        #endregion
        public MatchesRelationships matches { get; set; }

        #region Documentation
        /// Property:   participants
        ///
        /// Summary:    Gets or sets the participants.
        ///
        /// Returns:    The participants.
        #endregion
        public ParticipantsRelationships participants { get; set; }

        #region Documentation
        /// Property:   invitation
        ///
        /// Summary:    Gets or sets the invitation.
        ///
        /// Returns:    The invitation.
        #endregion
        public Invitation invitation { get; set; }
    }

    #region Documentation
    /// Class:  States
    ///
    /// Summary:    A states.
    #endregion
    public class States
    {
        #region Documentation
        /// Property:   active
        ///
        /// Summary:    Gets or sets a value indicating whether the active.
        ///
        /// Returns:    True if active, false if not.
        #endregion
        public bool active { get; set; }
    }

    #region Documentation
    /// Class:  Invitation
    ///
    /// Summary:    An invitation.
    #endregion
    public class Invitation
    {

    }

    #region Documentation
    /// Class:  MatchesRelationships
    ///
    /// Summary:    (Serializable) the matches relationships.
    #endregion
    [Serializable]
    public class MatchesRelationships
    {
        #region Documentation
        /// Property:   links
        ///
        /// Summary:    Gets or sets the links.
        ///
        /// Returns:    The links.
        #endregion
        public Links links { get; set; }
    }

    #region Documentation
    /// Class:  ParticipantsRelationships
    ///
    /// Summary:    (Serializable) the participants relationships.
    #endregion
    [Serializable]
    public class ParticipantsRelationships
    {
        #region Documentation
        /// Property:   links
        ///
        /// Summary:    Gets or sets the links.
        ///
        /// Returns:    The links.
        #endregion
        public Links links { get; set; }
    }
}
