using ApiRooms.Centros;
using ApiRooms.Centros.Repositories;
using ApiRooms.Centros.Services;
using ApiRooms.Graph.Responses;
using ApiRooms.Graph.Responses.FindMeetingTimeRequest;
using ApiRooms.Graph.Services;
using ApiRooms.Graph.ViewModels;
using ApiRooms.Usuarios.Repositories;
using ApiRooms.Usuarios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiRooms.Controllers
{
    [System.Web.Mvc.Route("api/[Controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomsService _roomsService;
        private readonly ICentroService _centroService;
        private readonly ICentroRepository _centroRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;


        public RoomController()
        {
            _roomsService = new RoomsService();
            _centroRepository = new CentroRepository();
            _centroService = new CentroService(_centroRepository);
            _usuarioRepository = new UsuarioRepository();
            _usuarioService = new UsuarioService(_usuarioRepository);
        }

        // GET: Room
        public ActionResult Index()
        {
            return View();
        }
        //[System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpPost]
        public async Task<JsonResult> GetRooms([FromBody]GetRoomViewModel getRoomViewModel,[FromBody]string accessToken)
        {
            GetRoomResponse response = new GetRoomResponse();
            try
            {
                 accessToken = GetNewAccessToken(getRoomViewModel.CentroId);
                // Get the room lists defined in a tenant.
                var roomLists = await _roomsService.findRoomLists(accessToken);
                GraphModel model = new GraphModel();

                model.Salas = new List<Sala>();

                foreach (var item in roomLists.Salas)
                {
                        model.Salas.Add(item);
                }
                var entrevistadorMail = _usuarioService.GetUsuarioById(getRoomViewModel.EntrevistadorId).UsuarioViewModel.Email;
                if (entrevistadorMail == "" || entrevistadorMail == null)
                {
                    response.IsValid = false;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                response = await GetSuggestedMeetings(accessToken, model.Salas[0].Email, entrevistadorMail, getRoomViewModel.Fecha, getRoomViewModel.CentroId, getRoomViewModel.OficinaId, getRoomViewModel.IgnorarDisponibilidad);

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                response.IsValid = false;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetNewAccessToken(int centroId)
        {

            var tokenId = _centroService.GetTokenIdByCentroId(centroId).tokenId;
            var token = _roomsService.GetTokenById(tokenId);

            return token.cuentaToken.Token;
        }

        public string GetRoomFiltrar(int Centro, int? Oficina = null)
        {
            switch (Oficina)
            {
                case 1:
                    return "OscarEspla";
                case 2:
                    return "PlayaSanJuan";
                case null:
                    switch (Centro)
                    {
                        case (int)CentrosEnum.Alicante:
                            return "OscarEspla";
                        case (int)CentrosEnum.Lisboa:
                            return "CENTERS";
                        case (int)CentrosEnum.Murcia:
                            return "Central";
                        case (int)CentrosEnum.Sevilla:
                            return "Cartuja";
                        case (int)CentrosEnum.Temuco:
                            return "Apoquindo";
                        case (int)CentrosEnum.Tetuan:
                            return "TetouanShore";
                        case (int)CentrosEnum.Trujillo:
                            return "Orbegoso";
                        case (int)CentrosEnum.Tucuman:
                            return "SanMartin";
                        case (int)CentrosEnum.Uberlandia:
                            return "UBT";
                        case (int)CentrosEnum.Salamanca:
                            return "VillaMayor";
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }



            return "";
        }

        public async Task<GetRoomResponse> GetSuggestedMeetings(string token, string nombreLista, string entrevistadorMail, DateTime fecha, int centroId, int? oficinaId, bool ignorarDisponibilidad)
        {
            GetRoomResponse response = new GetRoomResponse();
            var fechaInicio = DateTime.Parse(fecha.ToUniversalTime().ToString());
            var fechaFin = fechaInicio;

            try
            {
                string accessToken = token;


                FindMeetingTimesRequest findMeetingTimeRequest = new FindMeetingTimesRequest();
                var attendees = new List<ApiRooms.Graph.Responses.FindMeetingTimeRequest.Attendee>();

                List<string> possivelAttendees = new List<string>();

                if (!ignorarDisponibilidad)
                {
                    possivelAttendees.Add(entrevistadorMail);
                }

                foreach (var item in possivelAttendees)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        attendees.Add(new ApiRooms.Graph.Responses.FindMeetingTimeRequest.Attendee()
                        {
                            emailAddress = new ApiRooms.Graph.Responses.FindMeetingTimeRequest.EmailAddress() { address = item, name = item },
                            type = "required"
                        });
                    }
                }

                findMeetingTimeRequest.timeConstraint = new TimeConstraint()
                {
                    timeslots = new List<Timeslot>() {
                            new Timeslot() {
                            start = new ApiRooms.Graph.Responses.FindMeetingTimeRequest.Start() { dateTime = fechaInicio, timeZone = "UTC" },
                            end = new ApiRooms.Graph.Responses.FindMeetingTimeRequest.End() { dateTime = fechaFin, timeZone = "UTC" }}}
                };



                // Getting the rooms in the list
                FindRoomsListResponse listaSalas = new FindRoomsListResponse();
                GetStringExcludedRoomsResponse excludedRooms = new GetStringExcludedRoomsResponse();
                if (!string.IsNullOrEmpty(nombreLista))
                {
                    listaSalas = await _roomsService.FindRooms(accessToken, nombreLista);
                    excludedRooms = _roomsService.GetStringExcludedRooms(centroId, oficinaId);
                }
                if (excludedRooms.IsValid && !string.IsNullOrEmpty(excludedRooms.StringExcludedRooms))
                {
                    listaSalas = FilterListSalas(listaSalas, excludedRooms.StringExcludedRooms);
                }

                for (int i = 0; i < listaSalas.value.Count; i++)
                {
                    if (i < 18 && i < listaSalas.value.Count)
                    {

                        attendees.Add(new ApiRooms.Graph.Responses.FindMeetingTimeRequest.Attendee()
                        {
                            emailAddress = new ApiRooms.Graph.Responses.FindMeetingTimeRequest.EmailAddress() { address = listaSalas.value[i].address },
                            type = "resource"
                        });
                    }
                    else
                    {
                        break;
                    }
                }


                findMeetingTimeRequest.returnSuggestionReasons = true;
                findMeetingTimeRequest.isOrganizerOptional = true;
                findMeetingTimeRequest.maxCandidates = 100;

                findMeetingTimeRequest.attendees = attendees;


                // Get the the meeting times
                var findMeetingTimes = await _roomsService.PostFindMeetingTimes(accessToken, findMeetingTimeRequest);

                if (findMeetingTimes.emptySuggestionsReason.Equals("AttendeesUnavailable"))
                {
                    response.IsValid = false;
                    return response;
                }

                List<FindMeetingTimesRowViewModel> suggestions = FilterSuggestions(findMeetingTimes, listaSalas);

                if (suggestions.Count == 0)
                {
                    response.IsValid = false;
                    return response;
                }

                response.IsValid = true;
                response.salas = new List<SelectListItem>();

                foreach (var sugg in suggestions)
                {
                    foreach (var sala in sugg.Salas)
                    {
                        if (sala.Name != null && sala.Email != null)
                        {
                            SelectListItem listaSala = new SelectListItem();
                            listaSala.Value = sala.Email;
                            listaSala.Text = sala.Name;

                            response.salas.Add(listaSala);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.IsValid = false;
                response.ErrorMessage = e.Message;

            }
            return response;
        }

        private FindRoomsListResponse FilterListSalas(FindRoomsListResponse listasalas, string excludedRooms)
        {
            FindRoomsListResponse salasFiltradas = new FindRoomsListResponse();
            salasFiltradas.value = new List<Value>();

            foreach (var sala in listasalas.value)
            {
                if (!excludedRooms.Contains(sala.name))
                {
                    salasFiltradas.value.Add(sala);
                }
            }

            return salasFiltradas;
        }

        private List<FindMeetingTimesRowViewModel> FilterSuggestions(FindMeetingTimeResponse findMeetingTimes, FindRoomsListResponse listaTodasSalas)
        {
            var retVal = new List<FindMeetingTimesRowViewModel>();

            foreach (var suggestion in findMeetingTimes.meetingTimeSuggestions)
            {
                var rooms = new List<Sala>();
                var attendees = new List<string>();
                int personas = 0;

                foreach (var attendee in suggestion.attendeeAvailability)
                {
                    if (attendee.attendee.emailAddress.address.Substring(0, 4) == "ROOM")
                    {
                        if (attendee.availability == "free")
                        {
                            var sala = listaTodasSalas.value.Find(m => m.address.Equals(attendee.attendee.emailAddress.address));

                            rooms.Add(new Sala() { Email = attendee.attendee.emailAddress.address, Name = sala.name });
                        }
                    }
                    else
                    {
                        personas++;
                        if (attendee.availability == "free")
                        {
                            attendees.Add(attendee.attendee.emailAddress.address);
                        }
                        else
                        {
                            attendees.Clear();
                            break;
                        }
                    }
                    //COMENTAR EL SIGUIENTE IF PARA QUE DEVUELVA MAS SUGERENCIAS DE SALAS
                    //if (rooms.Count > 0 && attendees.Count == personas)
                    //{
                    //    break;
                    //}
                }

                if (rooms.Count > 0 && attendees.Count == personas)
                {
                    StringBuilder strb = new StringBuilder();
                    foreach (var item in attendees)
                    {
                        strb.Append(item);
                        strb.Append('#');
                    }

                    var sugg = new FindMeetingTimesRowViewModel()
                    {
                        Fecha = suggestion.meetingTimeSlot.start.dateTime,
                        Salas = rooms,
                        attendees = strb.ToString()
                    };
                    retVal.Add(sugg);
                }
            }

            return retVal;
        }

    }
}