using System;
using System.Collections.Generic;

namespace Challonge.Internal.Models
{
    [Serializable]
    public class GetUserResponse
    {
        public ResponseData data { get; set; }
    }

    [Serializable]
    public class GetCommunitiesResponse
    {
        public ResponseData data { get; set; }
    }
}
