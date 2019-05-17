using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Centros.ViewModels
{
    [Serializable]
    public class CentroViewModel
    {
        public int CentroId { get; set; }
        public string Nombre { get; set; }
    }
}