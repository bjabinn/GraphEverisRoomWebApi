using ApiRooms.Graph.Entities;
using ApiRooms.Usuarios.DataTable;
using ApiRooms.Usuarios.Mapper;
using ApiRooms.Usuarios.Repositories;
using ApiRooms.Usuarios.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.Services
{
    public class UsuarioService:IUsuarioService
    {
        #region Fields

        private readonly IUsuarioRepository _usuarioRepository;

        #endregion

        #region Constructors

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        #endregion

        public GetUsuariosResponse GetUsuarios(DataTableRequest request)
        {
            var response = new GetUsuariosResponse();

            try
            {
                var query = FilterString(request.CustomFilters);
                var filtered = query.ApplyColumnSettings(request, UsuarioMapper.GetPropertiePath);

                response.UsuarioViewModel = filtered.ConvertToUsuarioViewModel();
                response.TotalElementos = query.Count();
                response.IsValid = true;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public GetUsuariosResponse GetUsuariosByCentroUsuarioId(int CentroUsuarioId)
        {
            var request = new DataTableRequest();

            request.CustomFilters = new Dictionary<string, string>();
            request.CustomFilters.Add("CentroUsuarioId", CentroUsuarioId.ToString());

            return GetUsuarios(request);

        }
        public GetUsuariosResponse GetUsuarios()
        {
            var response = new GetUsuariosResponse();

            try
            {
                response.UsuarioViewModel = UsuarioMapper.ConvertToUsuarioViewModel(_usuarioRepository.GetByCriteria(x => x.IsActivo));
                response.IsValid = true;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public GetUsuarioByIdResponse GetUsuarioById(int usuarioId)
        {
            var response = new GetUsuarioByIdResponse();

            try
            {
                var usuario = _usuarioRepository.GetOne(x => x.UsuarioId == usuarioId && x.IsActivo);

                var usuarioViewModel = usuario.ConvertToCreateEditUsuarioViewModel();

                response.UsuarioViewModel = usuarioViewModel;

                response.IsValid = true;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public GetUsuarioByUserNameResponse GetUsuarioByUserName(string userName)
        {
            var response = new GetUsuarioByUserNameResponse();

            try
            {
                var usuario = _usuarioRepository.GetOne(x => x.UserName == userName && x.IsActivo);

                var usuarioViewModel = usuario.ConvertToCreateEditUsuarioViewModel();

                response.UsuarioViewModel = usuarioViewModel;

                response.IsValid = true;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public GetUsuariosByNombreUsuarioResponse GetUsuariosByNombreUsuario(string textSearch)
        {
            var response = new GetUsuariosByNombreUsuarioResponse() { IsValid = true };

            try
            {
                var listaUsuarios = _usuarioRepository.GetByCriteria(x => x.IsActivo);
                response.Usuarios = listaUsuarios.ConvertToUsuarioViewModel().ToList();
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        private IQueryable<Usuario> FilterString(IDictionary<string, string> customFilter)
        {
            var query = _usuarioRepository.GetAll();

            query = query.Where(x => x.IsActivo);

            if (customFilter.ContainsKey("Usuario") && (customFilter["Usuario"] != string.Empty))
            {
                var nombre = customFilter["Usuario"];
                query = query.Where(x => x.Nombre.Contains(nombre));
            }

            if (customFilter.ContainsKey("Rol") && (customFilter["Rol"] != string.Empty))
            {
                var rol = customFilter["Rol"];
                query = query.Where(x => x.UsuarioRol.Select(y => y.RolId.ToString()).Contains(rol));
            }

            if (customFilter.ContainsKey("CentroSearch") && (customFilter["CentroSearch"] != string.Empty))
            {
                var CentroUsuarioId = Convert.ToInt32(customFilter["CentroSearch"]);
                query = query.Where(x => x.CentroId == CentroUsuarioId);
            }

            return query;
        }

        public SearchUserNameUsadoResponse SearchUserNameUsado(int idUsuario, string userName)
        {
            throw new NotImplementedException();
        }
    }
}