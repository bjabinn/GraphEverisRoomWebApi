using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Responses
{
    public class FindMeetingTimeResponse
    {
        public string odataContext { get; set; }
        public string emptySuggestionsReason { get; set; }
        public List<MeetingTimeSuggestion> meetingTimeSuggestions { get; set; }
    }

    public class Start
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

    public class End
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

    public class MeetingTimeSlot
    {
        public Start start { get; set; }
        public End end { get; set; }
    }

    public class AttendeeAvailability
    {
        public string availability { get; set; }
        public Attendee attendee { get; set; }
    }

    public class Attendee
    {
        public string type { get; set; }
        public EmailAddress emailAddress { get; set; }
    }

    public class EmailAddress
    {
        public string address { get; set; }
    }

    public class Location
    {
        public string displayName { get; set; }
        public string locationEmailAddress { get; set; }
    }

    public class MeetingTimeSuggestion
    {
        public float confidence { get; set; }
        public string suggestionReason { get; set; }
        public string organizerAvailability { get; set; }
        public MeetingTimeSlot meetingTimeSlot { get; set; }
        public List<AttendeeAvailability> attendeeAvailability { get; set; }
        public List<Location> locations { get; set; }
    }
}