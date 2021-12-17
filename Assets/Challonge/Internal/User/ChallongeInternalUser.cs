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
    public static class ChallongeInternalUser
    {
        public static void MeRequest(string url, string accessToken, Action<UserResponse> callbackMethod)
        {
            string json = "";
            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.GetRequest(url, json, accessToken, (webrequest) =>
            {
                if (webrequest.result == UnityWebRequest.Result.Success)
                {
                    // Debug.Log(webrequest.downloadHandler.text);
                    Models.GetUserResponse response = new Models.GetUserResponse();
                    Models.ResponseDataWrapper responseDataWrapper = JsonConvert.DeserializeObject<Models.ResponseDataWrapper>(webrequest.downloadHandler.text);
                    response.data = responseDataWrapper.data;
                    UserResponse userResponse = new UserResponse();
                    userResponse.data.email = response.data.attributes.email;
                    userResponse.data.username = response.data.attributes.username;
                    userResponse.data.imageUrl = response.data.attributes.imageUrl;
                    userResponse.data.accessToken = accessToken;
                    callbackMethod.Invoke(userResponse);
                }
                else
                {
                    UserResponse response = new UserResponse();
                    response.Result = HTTPRequest.GetErrorResult(webrequest);
                    callbackMethod.Invoke(response);
                }
            }));
        }

        public static void CommunitiesRequest(string url, string accessToken, Action<CommunitiesResponse> callbackMethod)
        {
            string json = "";
            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.GetRequest(url, json, accessToken, (webrequest) =>
            {
                if (webrequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log(webrequest.downloadHandler.text);
                    Models.GetCommunitiesResponse response = new Models.GetCommunitiesResponse();
                    Models.ResponseDataWrapper responseDataWrapper = JsonConvert.DeserializeObject<Models.ResponseDataWrapper>(webrequest.downloadHandler.text);
                    response.data = responseDataWrapper.data;
                    CommunitiesResponse communitiesResponse = new CommunitiesResponse();
                    communitiesResponse.data.id = response.data.id;
                    communitiesResponse.data.identifier = response.data.attributes.identifier;
                    communitiesResponse.data.name = response.data.attributes.name;
                    communitiesResponse.data.description = response.data.attributes.description;
                    communitiesResponse.data.permalink = response.data.attributes.permalink;
                    communitiesResponse.data.subdomain = response.data.attributes.subdomain;
                    callbackMethod.Invoke(communitiesResponse);
                }
                else
                {
                    CommunitiesResponse response = new CommunitiesResponse();
                    response.Result = HTTPRequest.GetErrorResult(webrequest);
                    callbackMethod.Invoke(response);
                }
            }));
        }
    }
}
