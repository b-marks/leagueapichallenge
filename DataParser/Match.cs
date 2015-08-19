using RiotApi.Net.RestClient.Dto.Match;

namespace DataParser
{
    public class Match
    {
        public long Id { get; set; }
        public long Duration { get; set; }
        public string Region { get; set; }
        public string Queue { get; set; }
        public string Patch { get; set; }

        public Match(MatchDetail matchDetail)
        {
            Id = matchDetail.MatchId;
            Duration = matchDetail.MatchDuration;
            Region = matchDetail.Region;
            Patch = matchDetail.MatchVersion;
            Queue = matchDetail.QueueType.ToString();
        }

        public override string ToString()
        {
            return "\"" + Id + "\",\"" + Duration + "\",\"" + Region + "\",\"" + Patch + "\",\"" + Queue + "\"";
        }
    }
}