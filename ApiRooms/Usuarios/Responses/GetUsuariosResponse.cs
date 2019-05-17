using ApiRooms.Graph.Responses;
using ApiRooms.Usuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.Responses
{
    public class GetUsuariosResponse : ApplicationResponseBase
    {
        public IEnumerable<UsuarioRowViewModel> UsuarioViewModel { get; set; }
        public int TotalElementos { get; set; }
    }
}