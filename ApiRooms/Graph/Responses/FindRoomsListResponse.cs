using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Responses
{
    public class FindRoomsListResponse
    {
        public List<Value> value { get; set; }
    }
    public class Value
    {
        public string name { get; set; }
        public string address { get; set; }
    }
}