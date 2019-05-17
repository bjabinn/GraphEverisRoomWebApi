using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.ViewModels
{
    public class GetRoomViewModel
    {
        public int CentroId { get; set; }
        public int? OficinaId { get; set; }
        public int EntrevistadorId { get; set; }
        public DateTime Fecha { get; set; }
        public bool IgnorarDisponibilidad { get; set; }

    }
}