using ApiRooms.Graph.Responses;
using ApiRooms.Usuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.Responses
{
    public class GetUsuarioByUserNameResponse : ApplicationResponseBase
    {
        public CreateEditUsuarioViewModel UsuarioViewModel { get; set; }
    }
}