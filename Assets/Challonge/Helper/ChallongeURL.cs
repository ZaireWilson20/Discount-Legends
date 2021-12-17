using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Challonge
{ 
    public static class URL
    {
        public const string baseURLOauth = "https://auth.challonge.com/oauth/"; 

        public const string baseURL = "https://api.challonge.com/v2/";

        public const string applicationTokenRequest = "https://api.challonge.com/oauth/token";

        public const string userRequest = "me.json";

        public const string tokenRequest = "token";

        public const string deviceGrantAuthorizationRequest = "authorize_device";

        public static string Generate(Properties.Scope scope, string requestUrl)
        {
            switch (scope)
            {
                case Properties.Scope.User:
                    return URL.baseURL + requestUrl;
                case Properties.Scope.Application:
                    return URL.baseURL + "application/" + requestUrl; 
            }

            return "";
        }

        public static IEnumerator GetTexture(string imageURL, Action<Texture> callbackMethod)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
            yield return www.SendWebRequest();
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            callbackMethod.Invoke(myTexture);
            /*
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                callbackMethod.Invoke(myTexture);
            }*/
        }
    }
}
