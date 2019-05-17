using ApiRooms.Centros.Mappers;
using ApiRooms.Centros.Repositories;
using ApiRooms.Centros.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Centros.Services
{
    public class CentroService : ICentroService
    {
        #region Fields

        private readonly ICentroRepository _centroRepository;

        #endregion
        #region Constructors

        public CentroService(ICentroRepository centroRepository)
        {
            _centroRepository = centroRepository;
        }

        #endregion
        #region ICentroService

        public GetCentrosResponse GetCentros()
        {
            var response = new GetCentrosResponse();

            try
            {
                var centroList = _centroRepository
                                  .GetByCriteria(x => x.IsActivo)
                                  .OrderBy(p => p.Nombre)
                                  .ToList();

                var listaDeCentroViewModel = centroList.ConvertToDatosCentroViewModel();

                var selectList = new System.Web.Mvc.SelectList(listaDeCentroViewModel, "CentroId", "Nombre");

                response.ListaCentrosIdNombre = selectList;

                response.IsValid = true;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public GetTokenIdByCentroIdResponse GetTokenIdByCentroId(int centroId)
        {
            GetTokenIdByCentroIdResponse response = new GetTokenIdByCentroIdResponse();

            try
            {
                Graph.Entities.Centro centro = new Graph.Entities.Centro();
                centro.CentroId = 98;
                centro.Nombre = "Sevilla";
                centro.CuentaTokenId = 1;

                response.tokenId = centro.CuentaTokenId == null ? 0 : (int)centro.CuentaTokenId;
                response.IsValid = true;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;

            }

            return response;
        }



        #endregion
    }
}