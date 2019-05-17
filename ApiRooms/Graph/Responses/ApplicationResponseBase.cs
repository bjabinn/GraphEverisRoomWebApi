using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Responses
{
    public class ApplicationResponseBase
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}