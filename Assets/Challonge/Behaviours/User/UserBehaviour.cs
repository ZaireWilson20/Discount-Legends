using Challonge.Models;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// Namespace:  Challonge.Behaviours
///
/// Summary:    .
namespace Challonge.Behaviours
{
    /// Class:  UserBehaviour
    ///
    /// Summary:    A user behaviour.
    ///
    /// Author: Ahmed
    public class UserBehaviour : ChallongeBehaviour
    {
        /// Summary:    The challonge user credentials.
        public API.Data.ChallongeUser challongeUser;

        /// Summary:    Raised on successful Me Request
        public ChallongeEvent onMeRequestEvent;

        /// Summary:    True if send request on enable.
        public bool sendRequestOnEnable = true;

        /// Summary:    Raised on failed Me Request
        public UnityEvent onError;

        private void OnEnable()
        {
            if (sendRequestOnEnable)
                MeRequest();
        }

        /// Function:   MeRequest
        ///
        /// Summary:    Me request.
        ///
        /// Author: Ahmed
        public void MeRequest()
        {
            API.User.MeRequest(challongeUser.user.accessToken, (userResponse) =>
            {
                if (userResponse.Result == Properties.Result.SUCCESS)
                {
                    challongeUser.user.email = userResponse.data.email;
                    challongeUser.user.username = userResponse.data.username;
                    challongeUser.user.imageUrl = userResponse.data.imageUrl;

                    Internal.ChallongeMaster.Instance.CoroutineWrapper(URL.GetTexture(challongeUser.user.imageUrl, (texture) =>
                    {
                        challongeUser.user.userImage = texture;
     
                        if(onMeRequestEvent != null)
                            onMeRequestEvent.Raise();
                    }));
                }
                else
                {
                    onError.Invoke();
                }
            });
        }

        /// Function:   CommunitiesRequest
        ///
        /// Summary:    Communities request.
        ///
        /// Author: Ahmed
        public void CommunitiesRequest()
        {
            API.User.CommunitiesRequest(challongeUser.user.accessToken, (userResponse) =>
            {
                if (userResponse.Result == Properties.Result.SUCCESS)
                {
                   
                }
                else
                {
                    onError.Invoke();
                }
            });
        }

        /// Function:   ShowUsername
        ///
        /// Summary:    Shows the username.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// textMeshProUGUI -   The text mesh pro ugui.
        public void ShowUsername(TextMeshProUGUI textMeshProUGUI)
        {
            textMeshProUGUI.text = challongeUser.user.username;
        }

        /// Function:   ShowUserProfilePic
        ///
        /// Summary:    Shows the user profile picture.
        ///
        /// Author: Ahmed
        ///
        /// Parameters:
        /// rawImage -  The raw image.
        public void ShowUserProfilePic(UnityEngine.UI.RawImage rawImage)
        {
            rawImage.texture = challongeUser.user.userImage;
        }
    }
}
