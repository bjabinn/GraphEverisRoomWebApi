using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRooms.Graph.ViewModels;
using ApiRooms.Graph.Responses;
using ApiRooms.Graph.Responses.FindMeetingTimeRequest;

namespace ApiRooms.Graph.Services
{
    interface IRoomsService
    {
        Task<GraphModel> findRoomLists(string accessToken);
        Task<FindRoomsListResponse> FindRooms(string accessToken, string roomList);
        Task<FindMeetingTimeResponse> PostFindMeetingTimes(string accessToken, FindMeetingTimesRequest findMeetingTimesRequest);
        SaveTokenResponse SaveToken(string emailCuenta, string token);
        GetTokenByIdResponse GetTokenById(int tokenId);
        GetStringExcludedRoomsResponse GetStringExcludedRooms(int centroID, int? oficinaId);
        GetExcludedRoomsResponse GetExcludedRooms(int CentroId, int? OficinaId, FindRoomsListResponse listaSalas);
        SaveExcludedRoomsResponse SaveExcludedRooms(ExcludedRoomsViewModel excludedRoomsViewModel);
    }
}
