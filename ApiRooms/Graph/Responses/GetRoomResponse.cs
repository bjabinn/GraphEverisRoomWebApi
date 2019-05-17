using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiRooms.Graph.Responses
{
    public class GetRoomResponse : ApplicationResponseBase
    {
        public List<SelectListItem> salas { get; set; }
    }
}