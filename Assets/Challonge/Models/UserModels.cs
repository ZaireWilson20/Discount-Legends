using Challonge.Properties;
using System;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/// Namespace:  Challonge.Models
///
/// Summary:    .
#endregion
namespace Challonge.Models
{
    [Serializable]
    public class ChallongeUser
    {
        public string username;

        public string email;

        public string imageUrl;

        public string accessToken;

        public string refreshToken;

        public int tokenExpiration;

        public Texture userImage;
    }

    public class Community
    {
        public string id;

        public string name;

        public string permalink;

        public string subdomain;

        public string identifier;

        public string description;
    }

    public class UserRequestData
    {
        public string access_token;

        public UserRequestData(string access_token)
        {
            this.access_token = access_token;
        }
    }

    [Serializable]
    public class UserResponse : ChallongeBaseResponse
    {
        public ChallongeUser data = new ChallongeUser();
    }

    [Serializable]
    public class CommunitiesResponse : ChallongeBaseResponse
    {
        public Community data = new Community();
    }
}
