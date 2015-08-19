using RiotApi.Net.RestClient.Dto.Match.Generic;

namespace DataParser
{
    public class Team
    {
        public long MatchId { get; set; }
        public int Id { get; set; }
        public bool Winner { get; set; }

        public Team(TeamDto team)
        {
            Id = team.TeamId;
            Winner = team.Winner;
        }

        public override string ToString()
        {
            return "\"" + MatchId + "\",\"" + Id + "\",\"" + (Winner ? 1 : 0) + "\"";
        }
    }
}