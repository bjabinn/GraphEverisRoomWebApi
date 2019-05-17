using ApiRooms.Graph.Entities;
using ApiRooms.Graph.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Mappers
{
    public static class GraphMapper
    {
        public static CuentaTokenViewModel ConvertToCuentaTokenViewModel(this CuentaToken token)
        {
            var cuentaTokenViewModel = new CuentaTokenViewModel()
            {
                CuentaTokenId = token.CuentaTokenId,
                Email = token.Email,
                Token = token.Token,
                FechaCreacion = token.FechaCreacion,
                FechaExpiracion = token.FechaExpiracion,
                FechaCreacionToken = token.FechaCreacionToken,
                FechaExpiracionToken = token.FechaExpiracionToken

            };

            return cuentaTokenViewModel;
        }
    }
}