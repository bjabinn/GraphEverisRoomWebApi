using ApiRooms.Usuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios
{
    [Serializable]
    public class CreateEditRolViewModel
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<UsuarioRolViewModel> UsuarioRol { get; set; }
        public virtual ICollection<PermisoRolViewModel> PermisoRol { get; set; }
        public bool Activo { get; set; }
    }
}