using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Challonge.Models;
using System.Threading.Tasks;

namespace Challonge.Internal.API
{
    public class ChallongeInternalAuthorization
    {
        public static void DeviceGrantAuthorizationRequest(string url, DeviceGrantAuthorizationRequestData deviceGrantAuthorizationRequestData, Action<DeviceGrantAuthorizationResponse> callbackMethod)
        {
            string json = HTTPRequest.GetJson(deviceGrantAuthorizationRequestData);
            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.PostRequest(url, json, "", (webrequest) =>
            {
                if (webrequest.result == UnityWebRequest.Result.Success)
                {
                    DeviceGrantAuthorizationResponse response = new DeviceGrantAuthorizationResponse();
                    response.data = JsonConvert.DeserializeObject<ChallongeInternalAuthModels.DeviceGrantAuthorizationResponseData>(webrequest.downloadHandler.text);
                    callbackMethod.Invoke(response);
                }
                else
                {
                    DeviceGrantAuthorizationResponse response = new DeviceGrantAuthorizationResponse();
                    response.Result = HTTPRequest.GetErrorResult(webrequest);
                    callbackMethod.Invoke(response);
                }
            }));
        }

        public static void TokenRequest(string url, TokenRequestData tokenRequestData, Action<TokenResponse> callbackMethod)
        {
            string json = HTTPRequest.GetJson(tokenRequestData);
            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.PostRequest(url, json, "", (webrequest) =>
            {
                if(webrequest.result == UnityWebRequest.Result.Success)
                {
                    TokenResponse tokenResponse = new TokenResponse();
                    tokenResponse.data = JsonConvert.DeserializeObject<ChallongeInternalAuthModels.TokenResponseData>(webrequest.downloadHandler.text);
                    callbackMethod.Invoke(tokenResponse);
                }
                else
                {
                    TokenResponse tokenResponse = new TokenResponse();
                    tokenResponse.Result = HTTPRequest.GetErrorResult(webrequest);
                    callbackMethod.Invoke(tokenResponse);
                }
            }));
        }
    }
}
