using System;
using System.Collections;
using Challonge.Models;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

#region Documentation
/// Namespace:  Challonge
///
/// Summary:    .
#endregion
namespace Challonge.API
{
    #region Documentation
    /// Class:  ChallongeUsersAPI
    ///
    /// Summary:    A challonge users a pi.
    #endregion
    public static class User
    {
        /// Function:   MeRequest
        ///
        /// Summary:    Me request.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// accessToken -       The access token.
        /// callbackMethod -    The callback method.
        public static void MeRequest(string accessToken, Action<UserResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalUser.MeRequest(URL.baseURL + URL.userRequest, accessToken, callbackMethod);
        }

        /// Function:   CommunitiesRequest
        ///
        /// Summary:    Communities request.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// accessToken -       The access token.
        /// callbackMethod -    The callback method.
        public static void CommunitiesRequest(string accessToken, Action<CommunitiesResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalUser.CommunitiesRequest(URL.baseURL + URL.userRequest, accessToken, callbackMethod);
        }
    }
}
