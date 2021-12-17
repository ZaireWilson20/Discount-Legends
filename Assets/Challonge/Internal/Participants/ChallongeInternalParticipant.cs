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
    public static class ChallongeInternalParticipant
    {
        public static IEnumerator GetAllParticipants(string accessToken, GetAllParticipantsRequest GetAllParticipantsRequest, Action<GetAllParticipantsResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Get(GetAllParticipantsRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, GetAllParticipantsRequest);

                // Initialize Participant Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, GetAllParticipantsRequest.tournamentURL);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetAllParticipantsResponse participantsResponse = JsonConvert.DeserializeObject<Models.GetAllParticipantsResponse>(webRequest.downloadHandler.text);

                    // Return Clean Participants Response 
                    callbackMethod.Invoke(participantsResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetAllParticipantsResponse getAllParticipantsResponse = new GetAllParticipantsResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getAllParticipantsResponse);
                    callbackMethod.Invoke(getAllParticipantsResponse);
                }
            }
        }

        public static IEnumerator GetParticipant(string accessToken, GetParticipantRequest GetParticipantRequest, Action<GetParticipantResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Get(GetParticipantRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, GetParticipantRequest);

                // Initialize Tournament Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, GetParticipantRequest.tournamentURL);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.ParticipantID, GetParticipantRequest.participantID);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetParticipantResponse participantResponse = JsonConvert.DeserializeObject<Models.GetParticipantResponse>(webRequest.downloadHandler.text);

                    // Return Clean Participant Response 
                    callbackMethod.Invoke(participantResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetParticipantResponse getParticipantResponse = new GetParticipantResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getParticipantResponse);
                    callbackMethod.Invoke(getParticipantResponse);
                }
            }
        }

        public static void CreateParticipant(string accessToken, CreateParticipantRequest CreateParticipantRequest, Action<GetParticipantResponse> callbackMethod)
        {
            // Initialize Partcipant Request Properties
            Models.ResponseData responseData = new Models.ResponseData();
            responseData.type = Properties.ChallongeInternalProperties.ParticipantsType;
            responseData.attributes = Helper.ParticipantToAttributes(CreateParticipantRequest.participant);

            Models.ResponseDataWrapper dataWrapper = new Models.ResponseDataWrapper();
            dataWrapper.data = responseData;
            string json = HTTPRequest.GetJson(dataWrapper);

            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.PostRequest(CreateParticipantRequest.uri, json, accessToken, (webrequest) =>
            {
                if (webrequest.result == UnityWebRequest.Result.Success)
                {
                    // Deserialize Response
                    Models.ResponseData response = JsonConvert.DeserializeObject<Models.ResponseData>(webrequest.downloadHandler.text);
                    Models.GetParticipantResponse participantResponse = new Models.GetParticipantResponse();
                    participantResponse.data = response;

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(participantResponse.Convert());
                }
                else
                {
                    GetParticipantResponse response = new GetParticipantResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webrequest, response);
                    callbackMethod.Invoke(response);
                }
            }));
        }

        public static void CreateParticipants(string accessToken, CreateParticipantsRequest CreateParticipantsRequest, Action<GetAllParticipantsResponse> callbackMethod)
        {
            // Initialize Partcipant Request Properties
            Models.ResponseData responseData = new Models.ResponseData();
            responseData.type = Properties.ChallongeInternalProperties.ParticipantsType;
            responseData.attributes = new Models.Attributes();
            responseData.attributes.participants = Helper.ParticipantsToAttributesList(CreateParticipantsRequest.participants);

            Models.ResponseDataWrapper dataWrapper = new Models.ResponseDataWrapper();
            dataWrapper.data = responseData;
            string json = HTTPRequest.GetJson(dataWrapper);

            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.PostRequest(CreateParticipantsRequest.uri, json, accessToken, (webrequest) =>
            {
                if (webrequest.result == UnityWebRequest.Result.Success)
                {
                    // Deserialize Response
                    Models.ResponseDataListWrapper participantsResponseData = JsonConvert.DeserializeObject<Models.ResponseDataListWrapper>(webrequest.downloadHandler.text);
                    Models.GetAllParticipantsResponse participantsResponse = new Models.GetAllParticipantsResponse();
                    participantsResponse.data = participantsResponseData.data;

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(participantsResponse.Convert());
                }
                else
                {
                    GetAllParticipantsResponse response = new GetAllParticipantsResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webrequest, response);
                    callbackMethod.Invoke(response);
                }
            }));
        }

        public static IEnumerator DeleteParticipant(string accessToken, DeleteParticipantRequest DeleteParticipantRequest, Action<ChallongeBaseResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Delete(DeleteParticipantRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, DeleteParticipantRequest);

                // Initialize Tournament Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, DeleteParticipantRequest.tournamentURL);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    ChallongeBaseResponse baseResponse = new ChallongeBaseResponse();
                    baseResponse.Result = Challonge.Properties.Result.SUCCESS;

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(baseResponse);
                }

                // Failed Web Request 
                else
                {
                    ChallongeBaseResponse baseResponse = new ChallongeBaseResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, baseResponse);
                    callbackMethod.Invoke(baseResponse);
                }
            }
        }

        public static IEnumerator ClearParticipants(string accessToken, ClearParticipantsRequest ClearParticipantsRequest, Action<ChallongeBaseResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Delete(ClearParticipantsRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, ClearParticipantsRequest);

                // Initialize Tournament Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, ClearParticipantsRequest.tournamentURL);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    ChallongeBaseResponse baseResponse = new ChallongeBaseResponse();
                    baseResponse.Result = Challonge.Properties.Result.SUCCESS;

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(baseResponse);
                }

                // Failed Web Request 
                else
                {
                    ChallongeBaseResponse baseResponse = new ChallongeBaseResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, baseResponse);
                    callbackMethod.Invoke(baseResponse);
                }
            }
        }

        public static IEnumerator RandomizeParticipants(string accessToken, RandomizeParticipantsRequest RandomizeParticipantsRequest, Action<GetAllParticipantsResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Put(RandomizeParticipantsRequest.uri, "{}"))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, RandomizeParticipantsRequest);

                // Initialize Participant Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, RandomizeParticipantsRequest.tournamentURL);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    // Deserialize Response
                    Models.GetAllParticipantsResponse participantsResponse = JsonConvert.DeserializeObject<Models.GetAllParticipantsResponse>(webRequest.downloadHandler.text);

                    // Return Clean Participants Response 
                    callbackMethod.Invoke(participantsResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetAllParticipantsResponse getAllParticipantsResponse = new GetAllParticipantsResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getAllParticipantsResponse);
                    callbackMethod.Invoke(getAllParticipantsResponse);
                }
            }
        }
    }
}
