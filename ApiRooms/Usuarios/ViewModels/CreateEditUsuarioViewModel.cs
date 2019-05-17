using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.ViewModels
{
    [Serializable]
    public class CreateEditUsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Aplication { get; set; }
        public virtual ICollection<UsuarioRolViewModel> UsuarioRol { get; set; }

        public virtual int? CentroIdUsuario { get; set; }

        public bool Activo { get; set; }
        /*
         * UsuarioRol[i].RoleId
         * UsuarioRol[i].Name
         * UsuarioRol[i].HasRole
         * 
         * RoleId
         * Nombre Rol
         * Bool lo tiene
         */
    }
}