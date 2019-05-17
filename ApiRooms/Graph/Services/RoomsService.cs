using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ApiRooms.Graph.Entities;
using ApiRooms.Graph.Mappers;
using ApiRooms.Graph.Repositories;
using ApiRooms.Graph.Responses;
using ApiRooms.Graph.Responses.FindMeetingTimeRequest;
using ApiRooms.Graph.ViewModels;
using Newtonsoft.Json;

namespace ApiRooms.Graph.Services
{
    public class RoomsService : IRoomsService
    {
        #region Fields
        private readonly ICuentaTokenRepository _cuentaTokenRepository;
        private readonly IBlackListSalaRepository _blackListSalasRepository;
        #endregion

        #region Constructors
        public RoomsService()
        {
            //_cuentaTokenRepository = new CuentaTokenRepository();
            //_blackListSalasRepository = new BlackListSalaRepository();

        }
        #endregion

        #region Public Methods
        public async Task<GraphModel> findRoomLists(string accessToken)
        {
            string endpoint = "https://graph.microsoft.com/beta/me/findRoomLists";
            using(var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                {
                    //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json "));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    
                    using(var response=await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            FindRoomsListResponse findRoomsListResponse = JsonConvert.DeserializeObject<FindRoomsListResponse>(stringResult);

                            var retVal = new GraphModel();
                            retVal.Salas = new List<Sala>();
                            foreach(var item in findRoomsListResponse.value)
                            {
                                retVal.Salas.Add(new Sala { Name = item.name, Email = item.address });
                            }
                            return retVal;
                        }
                        return null;
                    }
                }
            }
        }
        public SaveTokenResponse SaveToken(string emailCuenta, string token)
        {
            SaveTokenResponse response = new SaveTokenResponse();

            try
            {
                var cuentaToken = _cuentaTokenRepository.GetOne(x => x.Email.Contains(emailCuenta));
                if (cuentaToken != null)
                {
                    cuentaToken.Token = token;
                    cuentaToken.FechaCreacionToken = DateTime.Now;
                    cuentaToken.FechaExpiracionToken = DateTime.Now.AddHours(1);
                    _cuentaTokenRepository.Update(cuentaToken);
                    response.IsValid = true;
                }

            }
            catch (Exception e)
            {
                response.IsValid = false;
                response.ErrorMessage = e.Message;
            }
            return response;
        }

        public GetTokenByIdResponse GetTokenById(int tokenId)
        {
            GetTokenByIdResponse response = new GetTokenByIdResponse();

            try
            {
                var cuentaToken = new CuentaToken
                {
                    CuentaTokenId = 1,
                    Email = "daniel.garcia.perez@everis.com",
                    Token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEWDhHQ2k2SnM2U0s4MlRzRDJQYjdyQ3pmaTh5X2JfT2ZNdVdpOEpNOFhoZnZrLThRcjBjMUFaYl9ndWxncGFFSzA5c20tZHJSN3BFQ2t4WUpvaDFhX28tNDZDUDFMcUhaV0RmSmpUM28tWXlBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiaUJqTDFSY3F6aGl5NGZweEl4ZFpxb2hNMllrIiwia2lkIjoiaUJqTDFSY3F6aGl5NGZweEl4ZFpxb2hNMllrIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8zMDQ4ZGM4Ny00M2YwLTQxMDAtOWFjYi1hZTE5NzFjNzkzOTUvIiwiaWF0IjoxNTI4MTAxNTc0LCJuYmYiOjE1MjgxMDE1NzQsImV4cCI6MTUyODEwNTQ3NCwiYWNyIjoiMSIsImFpbyI6IlkyZGdZSmhoOXZTMW1WMzJ0dmFuQi9uS0w3LzVyY2t0SEdkY0hMRkQ5WXF5OWVMN08zc0EiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IlByb29mIG9mIENvbmNlcHQiLCJhcHBpZCI6IjA0NmE4YTVmLThhOTEtNGJlNi04MTIzLTgyMmQ4NDZlYWU0OCIsImFwcGlkYWNyIjoiMSIsImVfZXhwIjoyNjI4MDAsImZhbWlseV9uYW1lIjoiR2FyY2lhIFBlcmV6IiwiZ2l2ZW5fbmFtZSI6IkRhbmllbCIsImlwYWRkciI6IjIxNy4xMjUuMTQzLjE0MCIsIm5hbWUiOiJEYW5pZWwgR2FyY2lhIFBlcmV6Iiwib2lkIjoiZmEzZjEzOTMtMDJmNS00MmE3LThjODAtMTA0NTIzYTM3YjBkIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTI5MTU5OTcxMTYtNDEzMTYwMzAyOS0xNzg5MjA3NzkzLTM4NjUyMSIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzM0ZGRjlGRDA4QjQwIiwic2NwIjoiQ2FsZW5kYXJzLlJlYWQuU2hhcmVkIENhbGVuZGFycy5SZWFkV3JpdGUuU2hhcmVkIEZpbGVzLlJlYWRXcml0ZSBNYWlsLlJlYWQgTWFpbC5TZW5kIFVzZXIuUmVhZCBVc2VyLlJlYWRCYXNpYy5BbGwiLCJzdWIiOiJma3dKVlBlZTJyRmlEblhzc2t5TEpPM1g0T1FNMWNlRDJTamtTMkJCZHRZIiwidGlkIjoiMzA0OGRjODctNDNmMC00MTAwLTlhY2ItYWUxOTcxYzc5Mzk1IiwidW5pcXVlX25hbWUiOiJkZ2FyY3BlckBldmVyaXMuY29tIiwidXBuIjoiZGdhcmNwZXJAZXZlcmlzLmNvbSIsInV0aSI6Ikd3aWlWRmVTNlVLQmFRZUZxR3dnQUEiLCJ2ZXIiOiIxLjAifQ.kcPb41pSt4tg6ie7bHViXWUVFgZT6SN3g9b76bKJSLsjJYmMc1u7kaa7w9jHbB2rKW35GpbEQZDMubX-tYIt-yTOlMBrmuHl1tHXZpwr-EL-nCbu5-b2jkUYDP9ealENk6EDv_5f_j176W147UgDirONxNPygDmPIQgpphS6pi3I4_Lr3CgUTlneqm4M5EIldaXs8MCf9n319veHL9j9ZyWVZFw-tN5CbCMXvSP5SlCE_lo_0w7oMWzZmmLWZ8j1D6isT8SCNKXG06GBfTu-BzX6kiNf-26HJmwQdCwP1QITSI3iRBXM4PqJBMRN1U-QpeypoXTKca441PiMOWCj1A.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8zMDQ4ZGM4Ny00M2YwLTQxMDAtOWFjYi1hZTE5NzFjNzkzOTUvIiwiaWF0IjoxNTI4MTAxNTc0LCJuYmYiOjE1MjgxMDE1NzQsImV4cCI6MTUyODEwNTQ3NCwiYWNyIjoiMSIsImFpbyI6IlkyZGdZSmhoOXZTMW1WMzJ0dmFuQi9uS0w3LzVyY2t0SEdkY0hMRkQ5WXF5OWVMN08zc0EiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IlByb29mIG9mIENvbmNlcHQiLCJhcHBpZCI6IjA0NmE4YTVmLThhOTEtNGJlNi04MTIzLTgyMmQ4NDZlYWU0OCIsImFwcGlkYWNyIjoiMSIsImVfZXhwIjoyNjI4MDAsImZhbWlseV9uYW1lIjoiR2FyY2lhIFBlcmV6IiwiZ2l2ZW5fbmFtZSI6IkRhbmllbCIsImlwYWRkciI6IjIxNy4xMjUuMTQzLjE0MCIsIm5hbWUiOiJEYW5pZWwgR2FyY2lhIFBlcmV6Iiwib2lkIjoiZmEzZjEzOTMtMDJmNS00MmE3LThjODAtMTA0NTIzYTM3YjBkIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTI5MTU5OTcxMTYtNDEzMTYwMzAyOS0xNzg5MjA3NzkzLTM4NjUyMSIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzM0ZGRjlGRDA4QjQwIiwic2NwIjoiQ2FsZW5kYXJzLlJlYWQuU2hhcmVkIENhbGVuZGFycy5SZWFkV3JpdGUuU2hhcmVkIEZpbGVzLlJlYWRXcml0ZSBNYWlsLlJlYWQgTWFpbC5TZW5kIFVzZXIuUmVhZCBVc2VyLlJlYWRCYXNpYy5BbGwiLCJzdWIiOiJma3dKVlBlZTJyRmlEblhzc2t5TEpPM1g0T1FNMWNlRDJTamtTMkJCZHRZIiwidGlkIjoiMzA0OGRjODctNDNmMC00MTAwLTlhY2ItYWUxOTcxYzc5Mzk1IiwidW5pcXVlX25hbWUiOiJkZ2FyY3BlckBldmVyaXMuY29tIiwidXBuIjoiZGdhcmNwZXJAZXZlcmlzLmNvbSIsInV0aSI6Ikd3aWlWRmVTNlVLQmFRZUZxR3dnQUEiLCJ2ZXIiOiIxLjAifQ.kcPb41pSt4tg6ie7bHViXWUVFgZT6SN3g9b76bKJSLsjJYmMc1u7kaa7w9jHbB2rKW35GpbEQZDMubX-tYIt-yTOlMBrmuHl1tHXZpwr-EL-nCbu5-b2jkUYDP9ealENk6EDv_5f_j176W147UgDirONxNPygDmPIQgpphS6pi3I4_Lr3CgUTlneqm4M5EIldaXs8MCf9n319veHL9j9ZyWVZFw-tN5CbCMXvSP5SlCE_lo_0w7oMWzZmmLWZ8j1D6isT8SCNKXG06GBfTu-BzX6kiNf-26HJmwQdCwP1QITSI3iRBXM4PqJBMRN1U-QpeypoXTKca441PiMOWCj1A.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8zMDQ4ZGM4Ny00M2YwLTQxMDAtOWFjYi1hZTE5NzFjNzkzOTUvIiwiaWF0IjoxNTI4MTAxNTc0LCJuYmYiOjE1MjgxMDE1NzQsImV4cCI6MTUyODEwNTQ3NCwiYWNyIjoiMSIsImFpbyI6IlkyZGdZSmhoOXZTMW1WMzJ0dmFuQi9uS0w3LzVyY2t0SEdkY0hMRkQ5WXF5OWVMN08zc0EiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IlByb29mIG9mIENvbmNlcHQiLCJhcHBpZCI6IjA0NmE4YTVmLThhOTEtNGJlNi04MTIzLTgyMmQ4NDZlYWU0OCIsImFwcGlkYWNyIjoiMSIsImVfZXhwIjoyNjI4MDAsImZhbWlseV9uYW1lIjoiR2FyY2lhIFBlcmV6IiwiZ2l2ZW5fbmFtZSI6IkRhbmllbCIsImlwYWRkciI6IjIxNy4xMjUuMTQzLjE0MCIsIm5hbWUiOiJEYW5pZWwgR2FyY2lhIFBlcmV6Iiwib2lkIjoiZmEzZjEzOTMtMDJmNS00MmE3LThjODAtMTA0NTIzYTM3YjBkIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTI5MTU5OTcxMTYtNDEzMTYwMzAyOS0xNzg5MjA3NzkzLTM4NjUyMSIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzM0ZGRjlGRDA4QjQwIiwic2NwIjoiQ2FsZW5kYXJzLlJlYWQuU2hhcmVkIENhbGVuZGFycy5SZWFkV3JpdGUuU2hhcmVkIEZpbGVzLlJlYWRXcml0ZSBNYWlsLlJlYWQgTWFpbC5TZW5kIFVzZXIuUmVhZCBVc2VyLlJlYWRCYXNpYy5BbGwiLCJzdWIiOiJma3dKVlBlZTJyRmlEblhzc2t5TEpPM1g0T1FNMWNlRDJTamtTMkJCZHRZIiwidGlkIjoiMzA0OGRjODctNDNmMC00MTAwLTlhY2ItYWUxOTcxYzc5Mzk1IiwidW5pcXVlX25hbWUiOiJkZ2FyY3BlckBldmVyaXMuY29tIiwidXBuIjoiZGdhcmNwZXJAZXZlcmlzLmNvbSIsInV0aSI6Ikd3aWlWRmVTNlVLQmFRZUZxR3dnQUEiLCJ2ZXIiOiIxLjAifQ.kcPb41pSt4tg6ie7bHViXWUVFgZT6SN3g9b76bKJSLsjJYmMc1u7kaa7w9jHbB2rKW35GpbEQZDMubX-tYIt-yTOlMBrmuHl1tHXZpwr-EL-nCbu5-b2jkUYDP9ealENk6EDv_5f_j176W147UgDirONxNPygDmPIQgpphS6pi3I4_Lr3CgUTlneqm4M5EIldaXs8MCf9n319veHL9j9ZyWVZFw-tN5CbCMXvSP5SlCE_lo_0w7oMWzZmmLWZ8j1D6isT8SCNKXG06GBfTu-BzX6kiNf-26HJmwQdCwP1QITSI3iRBXM4PqJBMRN1U-QpeypoXTKca441PiMOWCj1A",
                    FechaCreacion = new DateTime(2018, 04, 06),
                    FechaCreacionToken= new DateTime(2019,04,08),
                    FechaExpiracionToken=new DateTime(2020,05,19),
                    IsActivo=true
                };

                //    _cuentaTokenRepository.GetOne(x => x.CuentaTokenId == tokenId);
                response.cuentaToken = cuentaToken.ConvertToCuentaTokenViewModel();
                response.IsValid = true;
            }
            catch (Exception e)
            {
                response.IsValid = false;
                response.ErrorMessage = e.Message;
            }
            return response;
        }

        public async Task<FindRoomsListResponse> FindRooms(string accessToken, string roomList)
        {
            string endpoint = "https://graph.microsoft.com/beta/me/findRooms";

            if (!string.IsNullOrEmpty(roomList))
            {
                endpoint = endpoint + "(RoomList='" + roomList + "')";
            }

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            FindRoomsListResponse findRoomsListResponse = JsonConvert.DeserializeObject<FindRoomsListResponse>(stringResult);

                            return findRoomsListResponse;
                        }
                        return null;
                    }
                }
            }
        }


        public GetExcludedRoomsResponse GetExcludedRooms(int CentroId, int? OficinaId, FindRoomsListResponse listaSalas)
        {
            throw new NotImplementedException();
        }

        public GetStringExcludedRoomsResponse GetStringExcludedRooms(int centroID, int? oficinaId)
        {
            throw new NotImplementedException();
        }

        public SaveExcludedRoomsResponse SaveExcludedRooms(ExcludedRoomsViewModel excludedRoomsViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<FindMeetingTimeResponse> PostFindMeetingTimes(string accessToken, FindMeetingTimesRequest findMeetingTimesRequest)
        {
            string endpoint = "https://graph.microsoft.com/v1.0/me/findMeetingTimes";

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, endpoint))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    findMeetingTimesRequest.meetingDuration = "PT1H";

                    request.Content = new StringContent(JsonConvert.SerializeObject(findMeetingTimesRequest), Encoding.UTF8, "application/json");

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string stringResult = await response.Content.ReadAsStringAsync();
                            FindMeetingTimeResponse findMeetingTimeResponse = JsonConvert.DeserializeObject<FindMeetingTimeResponse>(stringResult);
                            return findMeetingTimeResponse;
                        }
                        return null;
                    }
                }
            }
        }

     
        #endregion
    }
}