using Challonge.Models;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

/// Namespace:  Challonge.Behaviours
///
/// Summary:    .
namespace Challonge.Behaviours
{
    /// Class:  AppAuthorizationBehaviour
    ///
    /// Summary:    An application authorization behaviour.
    ///
    /// Author: Ahmed
    public class AppAuthorizationBehaviour : ChallongeBehaviour
    {
        /// Summary:    Scriptable Object describing the application.
        public API.Data.ChallongeApplication applicationData;

        /// Summary:    Event Raised on successful app authorization
        public ChallongeEvent onAppAuthorizationEvent;

        /// Summary:    Event Raised on Failed app authorization
        private UnityEvent onError;

        /// Summary:    True if send request on enable.
        public bool sendRequestOnEnable = true;

        private void OnEnable()
        {
            if (sendRequestOnEnable)
                RequestToken();
        }

        /// Function:   RequestToken
        ///
        /// Summary:    Request an application token. Should be called at beginning of application start
        ///
        /// Author: Ahmed
        public void RequestToken()
        {
            if (applicationData == null)
            {
                Debug.LogError("(Challonge) Auth Request Fail: Reference to Challonge Application Data Missing in Monobehaviour");
                return;
            }

            if (applicationData.client_id == "")
            {
                Debug.LogError("(Challonge) Auth Request Fail: Client Id Missing");
                return;
            }

            if (applicationData.client_secret == "")
            {
                Debug.LogError("(Challonge) Auth Request Fail: Client Secret Missing");
                return;
            }

            API.Authorization.ApplicationTokenRequest(applicationData.client_id, applicationData.client_secret, (response) =>
            {
                if (response.Result == Properties.Result.SUCCESS)
                {
                    // Save Info
                    applicationData.accessToken = response.data.access_token;
                    applicationData.tokenExpiration = response.data.expires_in;

                    if (onAppAuthorizationEvent != null)
                        onAppAuthorizationEvent.Raise();
                }
                else
                {
                    Debug.Log("(Challonge) Auth Request Fail: " + response.Result.ToString());
                    onError.Invoke();
                }
            });
        }
    }
}
