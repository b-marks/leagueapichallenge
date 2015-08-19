namespace DataParser
{
    public class ParticipantFrame
    {
        public long MatchId { get; set; }
        public long Timestamp { get; set; }
        public int ParticipantId { get; set; }
        public int CurrentGold { get; set; }
        public int Level { get; set; }
        public int MinionsKilled { get; set; }
        public int TotalGold { get; set; }
        public int Xp { get; set; }

        public ParticipantFrame(RiotApi.Net.RestClient.Dto.Match.Generic.ParticipantFrame pf)
        {
            ParticipantId = pf.ParticipantId;
            CurrentGold = pf.CurrentGold;
            Level = pf.Level;
            MinionsKilled = pf.MinionsKilled;
            TotalGold = pf.TotalGold;
            Xp = pf.Xp;
        }

        public override string ToString()
        {
            return "\"" + MatchId + "\",\"" + ParticipantId + "\",\"" + CurrentGold + "\",\"" + Level + "\",\"" + MinionsKilled + "\",\"" + TotalGold + "\",\"" + Xp + "\",\"" + Timestamp + "\"";
        }
    }
}