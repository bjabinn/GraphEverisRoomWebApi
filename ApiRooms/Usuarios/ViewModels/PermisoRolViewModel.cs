using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios
{
    [Serializable]
    public class PermisoRolViewModel
    {
        public int PermisoId { get; set; }
        public int RolId { get; set; }
        public string PermisoNombre { get; set; }
        public bool ContienePermiso { get; set; }
    }
}