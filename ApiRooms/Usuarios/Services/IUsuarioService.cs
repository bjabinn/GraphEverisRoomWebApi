using ApiRooms.Usuarios.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRooms.Usuarios.Services
{
    public interface IUsuarioService
    {
        #region Query Request

        GetUsuariosResponse GetUsuarios();
        GetUsuariosResponse GetUsuarios(DataTableRequest request);
        GetUsuarioByIdResponse GetUsuarioById(int usuarioId);
        GetUsuarioByUserNameResponse GetUsuarioByUserName(string userName);
        SearchUserNameUsadoResponse SearchUserNameUsado(int idUsuario, string userName);
        GetUsuariosResponse GetUsuariosByCentroUsuarioId(int CentroUsuarioId);
        #endregion
    }
}
