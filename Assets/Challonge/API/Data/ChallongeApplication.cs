using System;
using System.Collections.Generic;
using UnityEngine;

/// Namespace:  Challonge.API.Data
///
/// Summary:    .
namespace Challonge.API.Data
{
    /// Class:  ChallongeAppl
    ///
    /// Summary:    An application data.
    ///
    /// Author: Ahmed
    [CreateAssetMenu(menuName = "Challonge/Credentials/Challonge Application")]
    public class ChallongeApplication : ScriptableObject
    {
        /// Summary:    Identifier for the client.
        public string client_id;

        /// Summary:    The client secret.
        public string client_secret;

        /// Summary:    The access token.
        public string accessToken;

        /// Summary:    The token expiration.
        public int tokenExpiration;

        /// Summary:    The permissions.
        public Properties.Permissions Permissions;
    }
}
