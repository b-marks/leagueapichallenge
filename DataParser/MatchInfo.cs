using System.Collections.Generic;

namespace DataParser
{
    public class MatchInfo
    {
        public Match Match { get; set; }
        public List<Team> Teams { get; set; }
        public List<Ban> Bans { get; set; }
        public List<Participant> Participants { get; set; }
        public List<Event> Events { get; set; }
        public List<ParticipantFrame> ParticipantFrames { get; set; }
    }
}