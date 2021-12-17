using System;
using Challonge.Models;

#region Documentation
/// Namespace:  Challonge
///
/// Summary:    .
#endregion
namespace Challonge.API
{
    #region Documentation
    /// Class:  ChallongeAuthenticationAPI
    ///
    /// Summary:    A challonge authentication api.
    #endregion
    public static class Authorization
    {
        /// Function:   DeviceGrantAuthorizationRequest
        ///
        /// Summary:    Device grant authorization request.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// client_id -         Identifier for the client.
        /// callbackMethod -    The callback method.
        public static void DeviceGrantAuthorizationRequest(string client_id, Action<DeviceGrantAuthorizationResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalAuthorization.DeviceGrantAuthorizationRequest(string.Concat(URL.baseURLOauth, URL.deviceGrantAuthorizationRequest), new DeviceGrantAuthorizationRequestData(client_id), callbackMethod);
        }

        /// Function:   DeviceGrantAccessTokenRequest
        ///
        /// Summary:    Device grant access token request.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// client_id -         Identifier for the client.
        /// device_code -       The device code.
        /// callbackMethod -    The callback method.
        public static void DeviceGrantAccessTokenRequest(string client_id, string device_code, Action<TokenResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalAuthorization.TokenRequest(string.Concat(URL.baseURLOauth, URL.tokenRequest), new DeviceGrantAccessTokenData(client_id, device_code), callbackMethod);
        }

        /// Function:   ApplicationTokenRequest
        ///
        /// Summary:    Application token request.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// client_id -         Identifier for the client.
        /// client_secret -     The client secret.
        /// callbackMethod -    The callback method.
        public static void ApplicationTokenRequest(string client_id, string client_secret, Action<TokenResponse> callbackMethod)
        {
            Internal.API.ChallongeInternalAuthorization.TokenRequest(URL.applicationTokenRequest, new ApplicationGrantAccessTokenData(client_id, client_secret), callbackMethod);
        }
    }
}
