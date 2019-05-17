using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.ViewModels
{
    public class GraphModel
    {
        public List<Sala> Salas { get; set; }
    }
    public class Sala
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}