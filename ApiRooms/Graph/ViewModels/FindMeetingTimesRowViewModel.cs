using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.ViewModels
{
    public class FindMeetingTimesRowViewModel
    {
        public string attendees { get; set; }
        public List<Sala> Salas { get; set; }
        public DateTime Fecha { get; set; }
        public bool Selected { get; set; }
        public string MailSelected { get; set; }
    }
}