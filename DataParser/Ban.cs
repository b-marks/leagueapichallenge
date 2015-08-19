using RiotApi.Net.RestClient.Dto.Match.Generic;

namespace DataParser
{
    public class Ban
    {
        public long MatchId { get; set; }
        public int ChampionId { get; set; }

        public Ban(BannedChampion champ)
        {
            ChampionId = champ.ChampionId;
        }

        public override string ToString()
        {
            return "\"" + MatchId + "\",\"" + ChampionId + "\"";
        }
    }
}