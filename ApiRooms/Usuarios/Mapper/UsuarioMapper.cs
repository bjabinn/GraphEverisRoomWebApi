using ApiRooms.Graph.Entities;
using ApiRooms.Usuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.Mapper
{
    public static class UsuarioMapper
    {
        #region Mappers

        public static CreateEditUsuarioViewModel ConvertToCreateEditUsuarioViewModel(this Usuario usuario)
        {
            if (usuario == null) return new CreateEditUsuarioViewModel();

            return new CreateEditUsuarioViewModel()
            {
                UsuarioId = usuario.UsuarioId,
                Usuario = usuario.Nombre,
                UserName = usuario.UserName,
                Aplication = usuario.Aplicacion,
                Email = usuario.Email,
                UsuarioRol = usuario.UsuarioRol.Select(x => x.ConvertToUsuarioRolViewModel()).ToList(),
                CentroIdUsuario = usuario.CentroId == null ? null : usuario.CentroId,
                Activo = usuario.IsActivo

            };
        }

        public static UsuarioRolPermisoViewModel ConvertToUsuarioRolPermisoViewModel(this Usuario usuario)
        {
            if (usuario == null) return new UsuarioRolPermisoViewModel();

            return new UsuarioRolPermisoViewModel()
            {
                UsuarioId = usuario.UsuarioId,
                Usuario = usuario.Nombre,
                UserName = usuario.UserName,
                Aplication = usuario.Aplicacion,
                Email = usuario.Email,
                CentroIdUsuario = usuario.CentroId == null ? null : usuario.CentroId,
                NombreCentroIdUsuario = usuario.CentroId == null ? string.Empty : usuario.Centro.Nombre,
                UsuarioRol = usuario.UsuarioRol.Select(x => x.ConvertToUsuarioRolPermisoViewModel()).ToList()
            };
        }


        public static IEnumerable<UsuarioRowViewModel> ConvertToUsuarioViewModel(this IEnumerable<Usuario> usuarioList)
        {
            var usuarioRowViewModelList = new List<UsuarioRowViewModel>();

            if (usuarioList == null)
            {
                return usuarioRowViewModelList;
            }

            usuarioRowViewModelList = usuarioList.Select(x => x.ConvertToUsuarioRowViewModel()).ToList();

            return usuarioRowViewModelList;
        }

        public static void UpdateUsuario(this Usuario usuario, CreateEditUsuarioViewModel createEditUsuarioViewModel)
        {
            usuario.UsuarioId = createEditUsuarioViewModel.UsuarioId;
            usuario.Aplicacion = createEditUsuarioViewModel.Aplication;
            usuario.Email = createEditUsuarioViewModel.Email;
            usuario.Nombre = createEditUsuarioViewModel.Usuario;
            usuario.UsuarioRol = createEditUsuarioViewModel.UsuarioRol.Where(y => y.ContieneRol).Select(x => x.ConvertToUsuarioRolViewModel()).ToList();
            usuario.UserName = createEditUsuarioViewModel.UserName;
            usuario.IsActivo = true;
            usuario.Aplicacion = "Recruiting";
            usuario.CentroId = createEditUsuarioViewModel.CentroIdUsuario;

        }

        public static string GetPropertiePath(string name)
        {
            string attributeName = null;

            switch (name)
            {
                case "Nombre":
                    attributeName = "Nombre";
                    break;
                case "CentroIdUsuario":
                    attributeName = "CentroIdUsuario";
                    break;
                default:
                    attributeName = "UsuarioId";
                    break;

            }
            return attributeName;
        }

        #endregion

        #region Private Methods
        private static UsuarioRowViewModel ConvertToUsuarioRowViewModel(this Usuario usuario)
        {
            var usuarioRowViewModel = new UsuarioRowViewModel()
            {
                UsuarioId = usuario.UsuarioId,
                Usuario = usuario.Nombre,
                UserName = usuario.UserName,
                CentroId = usuario.CentroId,
                Roles = usuario.UsuarioRol.Select(x => x.ConvertToUsuarioRolViewModel()).ToList()
            };

            return usuarioRowViewModel;
        }

        private static UsuarioRolViewModel ConvertToUsuarioRolViewModel(this UsuarioRol usuarioRol)
        {
            var usuarioRolViewModel = new UsuarioRolViewModel()
            {
                UsuarioId = usuarioRol.UsuarioId,
                RolId = usuarioRol.RolId,
                RolNombre = usuarioRol.Rol.Nombre,
                ContieneRol = true
            };

            return usuarioRolViewModel;
        }

        private static CreateEditRolViewModel ConvertToUsuarioRolPermisoViewModel(this UsuarioRol usuarioRol)
        {
            var usuarioRolViewModel = new CreateEditRolViewModel()
            {
                Descripcion = usuarioRol.Rol.Descripcion,
                Nombre = usuarioRol.Rol.Nombre,
                RolId = usuarioRol.RolId,
                UsuarioRol = usuarioRol.Rol.UsuarioRol.Select(x => x.ConvertToUsuarioRolToViewModel()).ToList(),
                PermisoRol = usuarioRol.Rol.PermisoRol.Select(x => x.ConvertToUsuarioPermisoToViewModel()).ToList()
            };

            return usuarioRolViewModel;
        }

        private static UsuarioRolViewModel ConvertToUsuarioRolToViewModel(this UsuarioRol usuarioRol)
        {
            var usuarioRolViewModel = new UsuarioRolViewModel()
            {
                ContieneRol = true,
                RolId = usuarioRol.RolId,
                RolNombre = usuarioRol.Rol.Nombre,
                UsuarioId = usuarioRol.UsuarioId
            };

            return usuarioRolViewModel;
        }

        private static PermisoRolViewModel ConvertToUsuarioPermisoToViewModel(this PermisoRol permisoRol)
        {
            var permisoRolViewModel = new PermisoRolViewModel()
            {
                ContienePermiso = true,
                PermisoId = permisoRol.PermisoId,
                PermisoNombre = permisoRol.Permiso.Nombre,
                RolId = permisoRol.RolId
            };

            return permisoRolViewModel;
        }

        private static UsuarioRol ConvertToUsuarioRolViewModel(this UsuarioRolViewModel usuarioRolViewModel)
        {
            var usuarioRol = new UsuarioRol()
            {
                UsuarioId = usuarioRolViewModel.UsuarioId,
                RolId = usuarioRolViewModel.RolId
            };

            return usuarioRol;
        }
        #endregion
    }
}