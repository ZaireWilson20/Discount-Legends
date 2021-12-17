using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// Namespace:  Challonge.Behaviours
///
/// Summary:    .
namespace Challonge.Behaviours
{
    /// Class:  DeviceAuthorizationBehaviour
    ///
    /// Summary:    A device authorization behaviour.
    ///
    /// Author: Ahmed
    public class DeviceAuthorizationBehaviour : ChallongeBehaviour
    {
        /// Summary:    Information describing the application.
        public API.Data.ChallongeApplication applicationData;

        /// Summary:    The challonge user.
        public API.Data.ChallongeUser challongeUser;

        /// Summary:    The on device grant access event.
        public ChallongeEvent onDeviceGrantAccessEvent;

        /// Summary:    The on request token success event.
        public ChallongeEvent onRequestTokenSuccessEvent;

        /// Summary:    True if send device grant authentication enable.
        public bool sendDeviceGrantAuthOnEnable = true;

        /// Summary:    The user code.
        public string userCode;

        /// Summary:    URI of the resource.
        public string uri;

        /// Summary:    The expiration time.
        public int expirationTime;

        /// Summary:    The interval.
        private int interval;

        /// Summary:    The device code.
        private string device_code;

        /// Summary:    The access code label.
        public TextMeshProUGUI accessCodeLabel;

        /// Summary:    The qr code.
        public UnityEngine.UI.RawImage qrCode;

        /// Summary:    The on error.
        public UnityEvent onError;

        private void OnEnable()
        {
            if (sendDeviceGrantAuthOnEnable)
                DeviceGrantAuthorizationRequest();
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        /// Function:   DeviceGrantAuthorizationRequest
        ///
        /// Summary:    Device grant authorization request.
        ///
        /// Author: Ahmed
        public void DeviceGrantAuthorizationRequest()
        {
            API.Authorization.DeviceGrantAuthorizationRequest(applicationData.client_id, (response) =>
            {
                if (response.Result == Properties.Result.SUCCESS)
                {
                    device_code = response.data.device_code;
                    userCode = response.data.user_code;
                    uri = response.data.verification_uri_complete;
                    expirationTime = response.data.expires_in;
                    interval = response.data.interval;

                    InvokeRepeating("CheckExpiration", 1, 1);
                    InvokeRepeating("RequestToken", interval, interval);

                    if (accessCodeLabel != null)
                        accessCodeLabel.text = userCode;

                    if (qrCode != null)
                        qrCode.texture = QRGenerator.EncodeString(uri);

                    onDeviceGrantAccessEvent.Raise();
                }
                else
                {

                }
            });
        }

        /// Function:   RequestToken
        ///
        /// Summary:    Request token.
        ///
        /// Author: Ahmed
        public void RequestToken()
        {
            API.Authorization.DeviceGrantAccessTokenRequest(applicationData.client_id, device_code, (response) =>
            {
                if (response.Result == Properties.Result.SUCCESS)
                {
                    CancelInvoke();

                    // Save Info
                    challongeUser.user.accessToken = response.data.access_token;
                    challongeUser.user.refreshToken = response.data.refresh_token;
                    challongeUser.user.tokenExpiration = response.data.expires_in;

                    onRequestTokenSuccessEvent.Raise();
                }
            });
        }

        /// Function:   CheckExpiration
        ///
        /// Summary:    Check expiration.
        ///
        /// Author: Ahmed
        private void CheckExpiration()
        {
            expirationTime--;
            if (expirationTime == 0)
            {
                CancelInvoke();
                DeviceGrantAuthorizationRequest();
            }
        }

        /// Function:   ResetData
        ///
        /// Summary:    Resets the data.
        ///
        /// Author: Ahmed
        public void ResetData()
        {
            uri = "";
            userCode = "";
            expirationTime = 0;
        }

        /// Function:   OpenURL
        ///
        /// Summary:    Opens the URL.
        ///
        /// Author: Ahmed
        public void OpenURL()
        {
            if (uri != "")
                Application.OpenURL(uri);
            else
                Debug.Log("No URI Found. Call SendAuthRequest() before calling OpenURL()");
        }
    }
}
