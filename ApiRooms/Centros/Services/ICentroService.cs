using ApiRooms.Centros.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRooms.Centros.Services
{
    public interface ICentroService
    {
        #region QueryRequest

        GetCentrosResponse GetCentros();
        GetTokenIdByCentroIdResponse GetTokenIdByCentroId(int centroId);

        #endregion
    }
}
