using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.ViewModels
{
    [Serializable]
    public class UsuarioRolPermisoViewModel
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Aplication { get; set; }
        public virtual ICollection<CreateEditRolViewModel> UsuarioRol { get; set; }

        public int? CentroIdUsuario { get; set; }
        public string NombreCentroIdUsuario { get; set; }
    }
}