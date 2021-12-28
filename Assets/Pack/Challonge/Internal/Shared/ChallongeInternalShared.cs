using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Challonge.Models;
using Challonge.Internal.Properties;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Challonge.Internal.API
{
    public static class ChallongeInternalShared
    {
        public static void InitUnityWebRequestNew(string accessToken, UnityWebRequest webRequest, ChallongeBaseRequest challongeBaseRequest)
        {
            webRequest.SetRequestHeader(ChallongeInternalProperties.AuthorizationType, challongeBaseRequest.authorizationType);
            webRequest.SetRequestHeader(ChallongeInternalProperties.Authorization, "Bearer " + accessToken);
            webRequest.SetRequestHeader(ChallongeInternalProperties.ContentType, challongeBaseRequest.contentType);
            webRequest.SetRequestHeader(ChallongeInternalProperties.Accept, challongeBaseRequest.acceptValue);
        }

        public static void InitFailedWebRequest(UnityWebRequest webRequest, ChallongeBaseResponse challongeBaseResponse)
        {
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    challongeBaseResponse.Result = Challonge.Properties.Result.CONNECTION_ERROR;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    challongeBaseResponse.Result = Challonge.Properties.Result.PROTOCAL_ERROR;
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    challongeBaseResponse.Result = Challonge.Properties.Result.DATA_PROCESSING_ERROR;
                    break;
                default:
                    challongeBaseResponse.Result = Challonge.Properties.Result.CONNECTION_ERROR;
                    break;
            }

            challongeBaseResponse.requestResultMessage = webRequest.error;
            Debug.Log("Raw Error Respone: " + webRequest.downloadHandler.text);
            challongeBaseResponse.errorMessage = JsonConvert.DeserializeObject<Internal.Models.ResponseData>(webRequest.downloadHandler.text).errors.detail;

            Debug.Log("[CHALLONGE] Web Request Failure ("+ challongeBaseResponse.Result.ToString() +") - " + challongeBaseResponse.errorMessage);
        }

        public static void InitFailedPostRequest(Exception e, string uri, ChallongeBaseResponse challongeBaseResponse)
        {
            challongeBaseResponse.Result = Challonge.Properties.Result.HTTPPOST_ERROR;
            challongeBaseResponse.requestResultMessage = String.Format("Unable to perform request on URI {0}: {1}", uri, e.Message);

            Debug.Log("Failed Web Request...");
            Debug.Log(challongeBaseResponse.requestResultMessage);
        }
    }
}
