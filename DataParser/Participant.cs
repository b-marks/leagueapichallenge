namespace DataParser
{
    public class Participant
    {
        public long MatchId { get; set; }
        public int TeamId { get; set; }
        public int ParticipantId { get; set; }
        public int ChampionId { get; set; }
        public string HighestAchievedSeasonTier { get; set; }
        public string Lane { get; set; }
        public string Role { get; set; }
        public long Assists { get; set; }
        public long Deaths { get; set; }
        public bool FirstBloodKill { get; set; }
        public long GoldEarned { get; set; }
        public long Item0 { get; set; }
        public long Item1 { get; set; }
        public long Item2 { get; set; }
        public long Item3 { get; set; }
        public long Item4 { get; set; }
        public long Item5 { get; set; }
        public long Item6 { get; set; }
        public long Kills { get; set; }
        public long MagicDamageDealt { get; set; }
        public long MagicDamageDealtToChampions { get; set; }
        public long MagicDamageTaken { get; set; }
        public long MinionsKilled { get; set; }
        public long NeutralMinionsKilled { get; set; }
        public long PhysicalDamageDealt { get; set; }
        public long PhysicalDamageDealtToChampions { get; set; }
        public long PhysicalDamageTaken { get; set; }
        public long SightWardsBoughtInGame { get; set; }
        public long TotalDamageDealt { get; set; }
        public long TotalDamageDealtToChampions { get; set; }
        public long TotalDamageTaken { get; set; }
        public long TotalHeal { get; set; }
        public long TotalTimeCrowdControlDealt { get; set; }
        public long TotalUnitsHealed { get; set; }
        public long TrueDamageDealt { get; set; }
        public long TrueDamageDealtToChampions { get; set; }
        public long TrueDamageTaken { get; set; }
        public long VisionWardsBoughtInGame { get; set; }
        public long WardsKilled { get; set; }
        public long WardsPlaced { get; set; }
        public double CreepsPerMinDeltas1020 { get; set; }
        public double CreepsPerMinDeltas2030 { get; set; }
        public double CreepsPerMinDeltas30 { get; set; }
        public double CreepsPerMinDeltas010 { get; set; }
        public double GoldPerMinDeltas1020 { get; set; }
        public double GoldPerMinDeltas2030 { get; set; }
        public double GoldPerMinDeltas30 { get; set; }
        public double GoldPerMinDeltas010 { get; set; }
        public long? SummonerId { get; set; }
        public string SummonerName { get; set; }

        public Participant(RiotApi.Net.RestClient.Dto.Match.Generic.Participant p)
        {
            TeamId = p.TeamId;
            ParticipantId = p.ParticipantId;
            ChampionId = p.ChampionId;
            HighestAchievedSeasonTier = p.HighestAchievedSeasonTier.ToString();
            Lane = p.Timeline.Lane.ToString();
            Role = p.Timeline.Role.ToString();
            Assists = p.Stats.Assists;
            Deaths = p.Stats.Deaths;
            FirstBloodKill = p.Stats.FirstBloodKill;
            GoldEarned = p.Stats.GoldEarned;
            Item0 = p.Stats.Item0;
            Item1 = p.Stats.Item1;
            Item2 = p.Stats.Item2;
            Item3 = p.Stats.Item3;
            Item4 = p.Stats.Item4;
            Item5 = p.Stats.Item5;
            Item6 = p.Stats.Item6;
            Kills = p.Stats.Kills;
            MagicDamageDealt = p.Stats.magicDamageDealt;
            MagicDamageDealtToChampions = p.Stats.MagicDamageDealtToChampions;
            MagicDamageTaken = p.Stats.MagicDamageTaken;
            MinionsKilled = p.Stats.MinionsKilled;
            NeutralMinionsKilled = p.Stats.NeutralMinionsKilled;
            PhysicalDamageDealt = p.Stats.PhysicalDamageDealt;
            PhysicalDamageDealtToChampions = p.Stats.PhysicalDamageDealtToChampions;
            PhysicalDamageTaken = p.Stats.PhysicalDamageTaken;
            SightWardsBoughtInGame = p.Stats.SightWardsBoughtInGame;
            TotalDamageDealt = p.Stats.TotalDamageDealt;
            TotalDamageDealtToChampions = p.Stats.TotalDamageDealtToChampions;
            TotalDamageTaken = p.Stats.TotalDamageTaken;
            TotalHeal = p.Stats.TotalHeal;
            TotalTimeCrowdControlDealt = p.Stats.TotalTimeCrowdControlDealt;
            TotalUnitsHealed = p.Stats.TotalUnitsHealed;
            TrueDamageDealt = p.Stats.TotalDamageDealt - p.Stats.PhysicalDamageDealt - p.Stats.magicDamageDealt;//apparently this client doesn't have true damage dealt
            TrueDamageDealtToChampions = p.Stats.TrueDamageDealtToChampions;
            TrueDamageTaken = p.Stats.TrueDamageTaken;
            VisionWardsBoughtInGame = p.Stats.VisionWardsBoughtInGame;
            WardsKilled = p.Stats.WardsKilled;
            WardsPlaced = p.Stats.WardsPlaced;
            CreepsPerMinDeltas010 = p.Timeline.CreepsPerMinDeltas.ZeroToTen;
            GoldPerMinDeltas010 = p.Timeline.GoldPerMinDeltas.ZeroToTen;
            CreepsPerMinDeltas1020 = p.Timeline.CreepsPerMinDeltas.TenToTwenty;
            GoldPerMinDeltas1020 = p.Timeline.GoldPerMinDeltas.TenToTwenty;
            CreepsPerMinDeltas2030 = p.Timeline.CreepsPerMinDeltas.TwentyToThirty;
            GoldPerMinDeltas2030 = p.Timeline.GoldPerMinDeltas.TwentyToThirty;
            CreepsPerMinDeltas30 = p.Timeline.CreepsPerMinDeltas.ThirtyToEnd;
            GoldPerMinDeltas30 = p.Timeline.GoldPerMinDeltas.ThirtyToEnd;
        }

        public override string ToString()
        {
            return "\"" + MatchId + "\",\"" + TeamId + "\",\"" + ParticipantId + "\",\"" + ChampionId + "\",\"" + HighestAchievedSeasonTier + "\",\"" + Lane + "\",\"" + Role + "\",\"" + Assists + "\",\"" + Deaths + "\",\"" + FirstBloodKill + "\",\"" + GoldEarned + "\",\"" + Item0 + "\",\"" + Item1 + "\",\"" + Item2 + "\",\"" + Item3 + "\",\"" + Item4 + "\",\"" + Item5 + "\",\"" + Item6 + "\",\"" + Kills + "\",\"" + MagicDamageDealt + "\",\"" + MagicDamageDealtToChampions + "\",\"" + MagicDamageTaken + "\",\"" + MinionsKilled + "\",\"" + NeutralMinionsKilled + "\",\"" + PhysicalDamageDealt + "\",\"" + PhysicalDamageDealtToChampions + "\",\"" + PhysicalDamageTaken + "\",\"" + SightWardsBoughtInGame + "\",\"" + TotalDamageDealt + "\",\"" + TotalDamageDealtToChampions + "\",\"" + TotalDamageTaken + "\",\"" + TotalHeal + "\",\"" + TotalTimeCrowdControlDealt + "\",\"" + TotalUnitsHealed + "\",\"" + TrueDamageDealt + "\",\"" + TrueDamageDealtToChampions + "\",\"" + TrueDamageTaken + "\",\"" + VisionWardsBoughtInGame + "\",\"" + WardsKilled + "\",\"" + WardsPlaced + "\",\"" + CreepsPerMinDeltas010 + "\",\"" + GoldPerMinDeltas010 + "\",\"" + CreepsPerMinDeltas1020 + "\",\"" + GoldPerMinDeltas1020 + "\",\"" + CreepsPerMinDeltas2030 + "\",\"" + GoldPerMinDeltas2030 + "\",\"" + CreepsPerMinDeltas30 + "\",\"" + GoldPerMinDeltas30 + "\",\"" + SummonerId ??"." + "\",\"" + SummonerName ??"." + "\"";
        }
    }
}