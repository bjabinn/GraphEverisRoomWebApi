using ApiRooms.Graph.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Responses
{
    public class GetExcludedRoomsResponse: ApplicationResponseBase
    {
        public ExcludedRoomsViewModel ExcludedRoomsList { get; set; }
    }
}