using ApiRooms.Graph.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Centros.Responses
{
    public class GetTokenIdByCentroIdResponse : ApplicationResponseBase
    {
        public int tokenId { get; set; }
    }
}