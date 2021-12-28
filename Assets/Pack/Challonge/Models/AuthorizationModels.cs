using Challonge.Properties;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Challonge.Models
{
    [Serializable]
    public class DeviceGrantAuthorizationRequestData
    {
        public string client_id;
        public string scope = "me tournaments:read tournaments:write matches:read matches:write participants:read participants:write application:manage";

        public DeviceGrantAuthorizationRequestData(string client_id)
        {
            this.client_id = client_id;
        }
    }

    [Serializable]
    public abstract class TokenRequestData
    {
        public string client_id;
        public string grant_type;

        public TokenRequestData(string client_id, string grant_type)
        {
            this.client_id = client_id;
            this.grant_type = grant_type;
        }
    }

    [Serializable]
    public class DeviceGrantAccessTokenData : TokenRequestData
    {
        public string device_code;

        public DeviceGrantAccessTokenData(string client_id, string device_code) : base(client_id, "urn:ietf:params:oauth:grant-type:device_code")
        { 
            this.device_code = device_code;
        }
    }

    [Serializable]
    public class ApplicationGrantAccessTokenData : TokenRequestData
    {
        public string client_secret;
        public string scope = "tournaments:read tournaments:write matches:read matches:write participants:read participants:write attachments:read attachments:write application:manage communities:manage";

        public ApplicationGrantAccessTokenData(string client_id, string client_secret) : base(client_id, "client_credentials")
        {
            this.client_secret = client_secret;
        }
    }

    [Serializable]
    public class TokenResponse : ChallongeBaseResponse
    {
        public Internal.API.ChallongeInternalAuthModels.TokenResponseData data;
    }

    [Serializable]
    public class DeviceGrantAuthorizationResponse : ChallongeBaseResponse
    {
        public Internal.API.ChallongeInternalAuthModels.DeviceGrantAuthorizationResponseData data;
    }
}