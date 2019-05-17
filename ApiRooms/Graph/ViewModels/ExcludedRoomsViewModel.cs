using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.ViewModels
{
    public class ExcludedRoomsViewModel
    {
        public int CentroId { get; set; }
        public int? OficinaId { get; set; }
        public List<ExcludedRoomViewModel> ExcludedRooms { get; set; }
    }
}