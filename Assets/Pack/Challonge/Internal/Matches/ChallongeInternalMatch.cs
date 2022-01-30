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
    public static class ChallongeInternalMatch
    {
        public static IEnumerator GetAllMatches(string accessToken, GetAllMatchesRequest GetAllMatchesRequest, Action<GetAllMatchesResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Get(GetAllMatchesRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, GetAllMatchesRequest);

                // Initialize Match Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.Page, GetAllMatchesRequest.pageCount.ToString());
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.PerPage, GetAllMatchesRequest.totalItemsPerPage.ToString());
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, GetAllMatchesRequest.tournamentURL);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.ParticipantID, GetAllMatchesRequest.participantID);

                switch (GetAllMatchesRequest.matchState)
                {
                    case GetAllMatchesRequest.MatchState.Pending:
                        webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.matchState, Properties.ChallongeInternalProperties.matchStatePending);
                        break;
                    case GetAllMatchesRequest.MatchState.Open:
                        webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.matchState, Properties.ChallongeInternalProperties.matchStateOpen);
                        break;
                    case GetAllMatchesRequest.MatchState.Complete:
                        webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.matchState, Properties.ChallongeInternalProperties.matchStateComplete);
                        break;
                }

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    // Deserialize Response
                    Models.GetAllMatchesResponse matchesResponse = JsonConvert.DeserializeObject<Models.GetAllMatchesResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(matchesResponse.Convert(GetAllMatchesRequest.tournamentURL));
                }

                // Failed Web Request 
                else
                {
                    GetAllMatchesResponse getAllMatchesResponse = new GetAllMatchesResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getAllMatchesResponse);
                    callbackMethod.Invoke(getAllMatchesResponse);
                }
            }
        }

        public static IEnumerator GetMatch(string accessToken, GetMatchRequest GetMatchRequest, Action<GetMatchResponse> callbackMethod)
        {
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Get(GetMatchRequest.uri))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, GetMatchRequest);

                // Initialize Tournament Request Properties
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, GetMatchRequest.tournamentURL);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.MatchID, GetMatchRequest.matchID);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetMatchResponse matchResponse = JsonConvert.DeserializeObject<Models.GetMatchResponse>(webRequest.downloadHandler.text);

                    // Return Clean Matches Response 
                    callbackMethod.Invoke(matchResponse.Convert(GetMatchRequest.tournamentURL));
                }

                // Failed Web Request 
                else
                {
                    GetMatchResponse getMatchResponse = new GetMatchResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, getMatchResponse);
                   callbackMethod.Invoke(getMatchResponse);
                }
            }
        }

        public static IEnumerator ChangeMatchState(string accessToken, ChangeMatchStateRequest ChangeMatchStateRequest, Action<GetMatchResponse> callbackMethod)
        {
            // Initialize Tournament Request Properties
            Models.ResponseData responseData = new Models.ResponseData();
            responseData.type = Properties.ChallongeInternalProperties.MatchStateAsAType;
            responseData.attributes = new Models.Attributes();
            switch (ChangeMatchStateRequest.matchStateAction)
            {
                case ChangeMatchStateRequest.MatchStateAction.Reopen:
                    responseData.attributes.state = Properties.ChallongeInternalProperties.matchStateReopen;
                    break;
                case ChangeMatchStateRequest.MatchStateAction.MarkAsUnderway:
                    responseData.attributes.state = Properties.ChallongeInternalProperties.matchStateMarkAsUnderway;
                    break;
                case ChangeMatchStateRequest.MatchStateAction.UnmarkAsUnderway:
                    responseData.attributes.state = Properties.ChallongeInternalProperties.matchStateUnmarkAsUnderway;
                    break;
            }
         
            Models.ResponseDataWrapper responseDataWrapper = new Models.ResponseDataWrapper();
            responseDataWrapper.data = responseData;
            string json = JsonConvert.SerializeObject(responseDataWrapper, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
            Debug.Log(json);
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Put(ChangeMatchStateRequest.uri, json))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, ChangeMatchStateRequest);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, ChangeMatchStateRequest.tournamentURL);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.MatchID, ChangeMatchStateRequest.matchID);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetMatchResponse matchResponse = JsonConvert.DeserializeObject<Models.GetMatchResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(matchResponse.Convert(ChangeMatchStateRequest.tournamentURL));
                }

                // Failed Web Request 
                else
                {
                    GetMatchResponse matchResponse = new GetMatchResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, matchResponse);
                    callbackMethod.Invoke(matchResponse);
                }
            }
        }

        public static IEnumerator UpdateMatchScores(string accessToken, UpdateMatchScoresRequest UpdateMatchScoresRequest, Action<GetMatchResponse> callbackMethod)
        {
            // Initialize Tournament Request Properties
            Models.ResponseData responseData = new Models.ResponseData();
            responseData.type = Properties.ChallongeInternalProperties.MatchAsAType;
            responseData.attributes = new Models.Attributes();
            responseData.attributes.match = new List<Models.MatchResult>();

            for(int i = 0; i < UpdateMatchScoresRequest.GetMatchUpdateValues().Count; i++)
            {
                Models.MatchResult match = new Models.MatchResult();
                match.participant_id = UpdateMatchScoresRequest.GetMatchUpdateValues()[i].participant_id;
                match.rank = UpdateMatchScoresRequest.GetMatchUpdateValues()[i].rank;
                match.score_set = UpdateMatchScoresRequest.GetMatchUpdateValues()[i].score_set;
                match.advancing = UpdateMatchScoresRequest.GetMatchUpdateValues()[i].advancing;
                responseData.attributes.match.Add(match);
            }

            Models.ResponseDataWrapper responseDataWrapper = new Models.ResponseDataWrapper();
            responseDataWrapper.data = responseData;
            string json = JsonConvert.SerializeObject(responseDataWrapper, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
            //Debug.Log(json);
            // Await for WebRequest to Complete
            using (var webRequest = UnityWebRequest.Put(UpdateMatchScoresRequest.uri, json))
            {
                // Initialize Web Request with base properties
                ChallongeInternalShared.InitUnityWebRequestNew(accessToken, webRequest, UpdateMatchScoresRequest);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.URL, UpdateMatchScoresRequest.tournamentURL);
                webRequest.SetRequestHeader(Properties.ChallongeInternalProperties.MatchID, UpdateMatchScoresRequest.matchID);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                // Successfull Web Request
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Web Request...");

                    // Deserialize Response
                    Models.GetMatchResponse matchResponse = JsonConvert.DeserializeObject<Models.GetMatchResponse>(webRequest.downloadHandler.text);

                    // Return Clean Tournament Response 
                    callbackMethod.Invoke(matchResponse.Convert(UpdateMatchScoresRequest.tournamentURL));
                }

                // Failed Web Request 
                else
                {
                    GetMatchResponse matchResponse = new GetMatchResponse();
                    ChallongeInternalShared.InitFailedWebRequest(webRequest, matchResponse);
                    callbackMethod.Invoke(matchResponse);
                }
            }
        }
    }
}
