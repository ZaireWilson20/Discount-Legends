using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Challonge.Models;
using Challonge.Internal.Properties;
using UnityEngine.Networking;

namespace Challonge.Internal.API
{
    public class HTTPPostResponse
    {
        public string response;
        public Exception exception;

        public HTTPPostResponse(string response, Exception e)
        {
            this.response = response;
            this.exception = e;
        }
    }

    public static class HTTPRequest
    {
        public static string GetJson(System.Object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static Challonge.Properties.Result GetErrorResult(UnityWebRequest unityWebRequest)
        {
            switch (unityWebRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    return Challonge.Properties.Result.CONNECTION_ERROR;
                case UnityWebRequest.Result.ProtocolError:
                    return Challonge.Properties.Result.PROTOCAL_ERROR;
                case UnityWebRequest.Result.DataProcessingError:
                    return Challonge.Properties.Result.DATA_PROCESSING_ERROR;
            }

            return Challonge.Properties.Result.CONNECTION_ERROR;
        }

        public static IEnumerator GetRequest(string url, string json, string accessToken, Action<UnityWebRequest> callBackMethod)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                //byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
                //webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
                //webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader(ChallongeInternalProperties.ContentType, ChallongeInternalProperties.ContentTypeValue);
                webRequest.SetRequestHeader(ChallongeInternalProperties.Accept, ChallongeInternalProperties.AcceptValue);
                webRequest.SetRequestHeader(ChallongeInternalProperties.AuthorizationType, ChallongeInternalProperties.AuthorizationTypeValue);
                webRequest.SetRequestHeader(ChallongeInternalProperties.Authorization, "Bearer " + accessToken);

                //Send the request then wait here until it returns
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Error While Sending: " + webRequest.error);
                    Debug.Log(webRequest.downloadHandler.text);
                }

                callBackMethod.Invoke(webRequest);
            }
        }

        public static IEnumerator PostRequest(string url, string json, string accessToken, Action<UnityWebRequest> callBackMethod)
        {
            using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
            {
                byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader(ChallongeInternalProperties.ContentType, ChallongeInternalProperties.ContentTypeValue);
                webRequest.SetRequestHeader(ChallongeInternalProperties.Accept, ChallongeInternalProperties.AcceptValue);
                webRequest.SetRequestHeader(ChallongeInternalProperties.AuthorizationType, ChallongeInternalProperties.AuthorizationTypeValue);
                if(accessToken != "")
                    webRequest.SetRequestHeader(ChallongeInternalProperties.Authorization, "Bearer " + accessToken);

                //Send the request then wait here until it returns
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                    Debug.Log("Error While Sending: " + webRequest.error);

                callBackMethod.Invoke(webRequest);
            }
        }

        /// <summary>
        /// Send HTTP POST Call
        /// </summary>
        /// <param name="targetUri">URL of HTTP Call</param>
        /// <param name="JSONMessage">Message for body of POST call</param>
        /// <returns>JSON Response</returns>
        public static HTTPPostResponse SendHttpPOSTCall(string targetUri, string JSONMessage, ChallongeBaseRequest challongeBaseRequest)
        {
            //Initialize HTTP Request Object with params
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(targetUri);
            req.Method = "POST";
            //req.Headers.Add(ChallongeInternalProperties.Authorization, challongeBaseRequest.authValue);
            //req.Headers.Add(ChallongeInternalProperties.AuthorizationType, challongeBaseRequest.authorizationType);
            req.ContentType = ChallongeInternalProperties.ContentTypeValue; // challongeBaseRequest.contentType;
            // req.Accept = challongeBaseRequest.acceptValue;

            //Set Body
            byte[] byteArray = Encoding.UTF8.GetBytes(JSONMessage);
            req.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = req.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            //Try to send and receive response
            try
            {
                //Get response from request and store into resp. Store it into Stream Reader
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    return new HTTPPostResponse(reader.ReadToEnd(), null);
                }
            }
            catch (Exception e)
            {
                //Log Error if it occurs
                Debug.LogErrorFormat(
                    "Unable to perform request on URI {0}: {1}", targetUri, e.Message);
                Debug.LogFormat(
                    "Stack trace:{0}{1}", Environment.NewLine, e.StackTrace);
                return new HTTPPostResponse(string.Empty, e);
            }
        }
    }
}
