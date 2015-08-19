namespace DataParser
{
    public class Event
    {
        public long MatchId { get; set; }
        public int ParticipantId { get; set; }
        public string EventType { get; set; }
        public int? ItemAfter { get; set; }
        public int? ItemBefore { get; set; }
        public int? ItemId { get; set; }
        public string LevelUpType { get; set; }
        public int? SkillSlot { get; set; }
        public long Timestamp { get; set; }

        public Event(RiotApi.Net.RestClient.Dto.Match.Generic.Event e)
        {
            ParticipantId = e.ParticipantId;
            EventType = e.EventType.ToString();
            int? nullInt = null;
            ItemAfter = e.ItemAfter == 0 ? nullInt : e.ItemAfter;
            ItemBefore = e.ItemBefore == 0 ? nullInt : e.ItemBefore;
            ItemId = e.ItemId == 0 ? nullInt : e.ItemId;
            LevelUpType = e.LevelUpType.ToString();
            SkillSlot = e.SkillSlot == 0 ? nullInt : e.SkillSlot;
            Timestamp = e.Timestamp;
        }

        public override string ToString()
        {
            return "\"" + MatchId + "\",\"" + ParticipantId + "\",\"" + EventType + "\",\"" + ItemAfter ?? "." + "\",\"" + ItemBefore ?? "." + "\",\"" + ItemId ?? "." + "\",\"" + LevelUpType + "\",\"" + SkillSlot ?? "." + "\",\"" + Timestamp + "\"";
        }
    }
}