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
    public static class ChallongeInternalTournament
    {
        public static IEnumerator GetAllTournaments(string accessToken, GetAllTournamentsRequest GetAllTournamentsRequest, Action<GetAllTournamentsResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Get(GetAllTournamentsRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, GetAllTournamentsRequest);

                // Initialize Tournament Request Properties           
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.Page, GetAllTournamentsRequest.pageCount.ToString());
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.PerPage, GetAllTournamentsRequest.totalItemsPerPage.ToString());
                if(GetAllTournamentsRequest.createdAfterDate != "")
                    webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.CreatedAfter, GetAllTournamentsRequest.createdAfterDate);
                if (GetAllTournamentsRequest.createdBeforeDate != "")
                    webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.CreatedBefore, GetAllTournamentsRequest.createdBeforeDate);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.TournamentType, Helper.GetTournamentTypeAsString(Helper.ToTournamentType(GetAllTournamentsRequest.tournamentType)));
                
                switch (GetAllTournamentsRequest.tournamentState)
                {
                    case GetAllTournamentsRequest.TournamentState.any:
                        break;
                    default:
                        webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.TournamentState, GetAllTournamentsRequest.tournamentState.ToString());
                        break;
                }

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetAllTournamentsResponse tournamentsResponse = JsonConvert.DeserializeObject<Models.GetAllTournamentsResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(tournamentsResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetAllTournamentsResponse getAllTournamentsResponse = new GetAllTournamentsResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getAllTournamentsResponse);
                    callbackMethod.Invoke(getAllTournamentsResponse);
                }
            }
        }

        public static IEnumerator GetTournament(string accessToken, GetTournamentRequest GetTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Get(GetTournamentRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, GetTournamentRequest);

                // Initialize Tournament Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, GetTournamentRequest.tournamentURL);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetTournamentResponse tournamentsResponse = JsonConvert.DeserializeObject<Models.GetTournamentResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(tournamentsResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetTournamentResponse getTournamentResponse = new GetTournamentResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getTournamentResponse);
                    callbackMethod.Invoke(getTournamentResponse);
                }
            }
        }

        public static void CreateTournament(string accessToken, CreateTournamentRequest CreateTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        {
            // Check Tournament URL
            if (CreateTournamentRequest.Tournament.url == "")
            {
                CreateTournamentRequest.Tournament.url = Helper.RandomString(7);
            }

            // Initialize Tournament Request Properties
            Models.ResponseData responseData = Helper.TournamentToResponseData(new Models.ResponseData(), CreateTournamentRequest.Tournament);
            Models.ResponseDataWrapper dataWrapper = new Models.ResponseDataWrapper();
            dataWrapper.data = responseData;
            string json = HTTPRequest.GetJson(dataWrapper);

            ChallongeMaster.Instance.CoroutineWrapper(HTTPRequest.PostRequest(CreateTournamentRequest.uri, json, accessToken, (webrequest) =>
            {
                if (webrequest.result == UnityWebRequest.Result.Success)
                {
                    Models.GetTournamentResponse response = JsonConvert.DeserializeObject<Models.GetTournamentResponse>(webrequest.downloadHandler.text);
                    callbackMethod.Invoke(response.Convert());
                }
                else
                {
                    GetTournamentResponse response = new GetTournamentResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webrequest, response);
                    callbackMethod.Invoke(response);
                }
            }));
        }

        #region CreateTournamentBackUp

        //public static IEnumerator CreateTournament(CreateTournamentRequest CreateTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        //{
        //    // Initialize Tournament Request Properties
        //    Models.ResponseData responseData = Helper.TournamentToResponseData(new Models.ResponseData(), CreateTournamentRequest.Tournament);
            
        //    WWWForm form = new WWWForm();
        //    Models.ResponseDataWrapper dataWrapper = new Models.ResponseDataWrapper();
        //    dataWrapper.data = responseData;
        //    string json = JsonConvert.SerializeObject(dataWrapper, Formatting.None, new JsonSerializerSettings
        //    {
        //        NullValueHandling = NullValueHandling.Ignore
        //    });

        //    Debug.Log(json);

        //    form.AddField(Properties.ChallongeInternalProperties.Data, json);
        //    //webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.Data, JsonConvert.SerializeObject(responseData));

        //    // Await for WebRequest to Complete
        //    using (var webRequest = UnityWebRequest.Post(CreateTournamentRequest.uri, json))
        //    {
        //        // Initialize Web Request with base properties
        //        ChallongeInternalShared.InitUnityWebRequest(webRequest, CreateTournamentRequest);

        //        // Request and wait for the desired page.
        //        yield return webRequest.SendWebRequest();

        //        // Successfull Web Request
        //        if (webRequest.result == UnityWebRequest.Result.Success)
        //        {
        //            Debug.Log("Success Web Request...");

        //            // Deserialize Response
        //            Models.ResponseData tournamentResponseData = JsonConvert.DeserializeObject<Models.ResponseData>(webRequest.downloadHandler.text);
        //            Models.GetTournamentResponse tournamentsResponse = new Models.GetTournamentResponse();
        //            tournamentsResponse.data = tournamentResponseData;

        //            // Return Clean Tournament Response 
        //            callbackMethod.Invoke(tournamentsResponse.Convert());
        //        }

        //        // Failed Web Request 
        //        else
        //        {
        //            GetTournamentResponse getTournamentResponse = new GetTournamentResponse();
        //            ChallongeInternalShared.InitFailedWebRequest(webRequest, getTournamentResponse);
        //            callbackMethod.Invoke(getTournamentResponse);
        //        }
        //    }
        //}

        #endregion

        public static IEnumerator ChangeTournamentState(string accessToken, ChangeTournamentStateRequest ChangeTournamentStateRequest, Action<GetTournamentResponse> callbackMethod)
        {
            // Initialize Tournament Request Properties
            Models.ResponseData responseData = new Models.ResponseData();
            responseData.type = Properties.ChallongeInternalProperties.TournamentStateAsType;
            responseData.attributes = new Models.Attributes();
            responseData.attributes.state = ChangeTournamentStateRequest.stateAction.ToString();
            Models.ResponseDataWrapper responseDataWrapper = new Models.ResponseDataWrapper();
            responseDataWrapper.data = responseData;
            string json = JsonConvert.SerializeObject(responseDataWrapper, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
            Debug.Log(json);
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Put(ChangeTournamentStateRequest.uri, json))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, ChangeTournamentStateRequest);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, ChangeTournamentStateRequest.tournamentURL);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetTournamentResponse tournamentsResponse = JsonConvert.DeserializeObject<Models.GetTournamentResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(tournamentsResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetTournamentResponse getTournamentResponse = new GetTournamentResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getTournamentResponse);
                    callbackMethod.Invoke(getTournamentResponse);
                }
            }
        }

        public static IEnumerator UpdateTournament(string accessToken, UpdateTournamentRequest UpdateTournamentRequest, Action<GetTournamentResponse> callbackMethod)
        {
            // Initialize Tournament Request Properties
            Models.ResponseData responseData = Helper.TournamentToResponseData(new Models.ResponseData(), UpdateTournamentRequest.Tournament);
  
            Models.ResponseDataWrapper responseDataWrapper = new Models.ResponseDataWrapper();
            responseDataWrapper.data = responseData;
            string json = JsonConvert.SerializeObject(responseDataWrapper, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
            Debug.Log(json);
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Put(UpdateTournamentRequest.uri, json))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, UpdateTournamentRequest);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, UpdateTournamentRequest.Tournament.url);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetTournamentResponse tournamentsResponse = JsonConvert.DeserializeObject<Models.GetTournamentResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(tournamentsResponse.Convert());
                }

                // Failed Web Request 
                else
                {
                    GetTournamentResponse getTournamentResponse = new GetTournamentResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getTournamentResponse);
                    callbackMethod.Invoke(getTournamentResponse);
                }
            }
        }

        public static IEnumerator DeleteTournament(string accessToken, DeleteTournamentRequest DeleteTournamentRequest, Action<ChallongeBaseResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Delete(DeleteTournamentRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, DeleteTournamentRequest);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Return Delete Tournament Response 
                    callbackMethod.Invoke(new ChallongeBaseResponse());
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
    }
}
