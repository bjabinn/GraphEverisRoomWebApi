using ApiRooms.Graph.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiRooms.Centros.Responses
{
    public class GetCentrosResponse : ApplicationResponseBase
    {
        public IEnumerable<SelectListItem> ListaCentrosIdNombre { get; set; }
    }
}