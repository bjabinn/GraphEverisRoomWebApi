using ApiRooms.Centros.ViewModels;
using ApiRooms.Graph.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Centros.Mappers
{
    public static class CentroMapper
    {
        #region Mappers

        public static IEnumerable<CentroViewModel> ConvertToDatosCentroViewModel(this IEnumerable<Centro> centroList)
        {
            var centroRowViewModelList = new List<CentroViewModel>();

            if (centroList == null || !centroList.Any()) return centroRowViewModelList;

            centroRowViewModelList = centroList.Select(x => x.ConvertToCentroRowViewModel()).ToList();

            return centroRowViewModelList;
        }

        #region Private Methods

        private static CentroViewModel ConvertToCentroRowViewModel(this Centro centro)
        {
            return new CentroViewModel()
            {
                CentroId = centro.CentroId,
                Nombre = centro.Nombre,
            };
        }

        #endregion

        #endregion
    }
}