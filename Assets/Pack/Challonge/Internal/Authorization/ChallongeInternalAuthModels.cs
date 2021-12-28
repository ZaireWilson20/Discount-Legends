namespace Challonge.Internal.API
{
    public class ChallongeInternalAuthModels
    {
        public class DeviceGrantAuthorizationResponseData
        {
            public string device_code { get; set; }
            public string user_code { get; set; }
            public string verification_uri { get; set; }
            public string verification_uri_complete { get; set; }
            public int expires_in { get; set; }
            public int interval { get; set; }
        }

        public class TokenResponseData
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public int expires_in { get; set; }
            public int created_at { get; set; }
            public string error { get; set; }
            public string error_description { get; set; }
        }
    }
}