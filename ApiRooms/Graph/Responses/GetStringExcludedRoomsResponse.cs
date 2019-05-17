using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Responses
{
    public class GetStringExcludedRoomsResponse: ApplicationResponseBase
    {
        public string StringExcludedRooms { get; set; }
    }
}