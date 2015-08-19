using System;
using System.Configuration;
using Newtonsoft.Json;
using RiotApi.Net.RestClient;
using RiotApi.Net.RestClient.Configuration;
using System.IO;
using System.Threading;
using RiotApi.Net.RestClient.Dto.Match;
using System.Linq;
using System.Collections.Generic;
using RiotApi.Net.RestClient.Helpers;

namespace DataParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string filename = "./output.txt";
            string inputDirectory = "C:\\Users\\Ben\\Documents\\Visual Studio 2015\\Projects\\DataParser\\DataParser\\MatchIds\\";
            string outputDirectory = "./CSVs/";
            SaveData(inputDirectory, outputDirectory);
        }

        private static void ParseData(string filename)
        {
            string line;

            StreamReader file = new StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 2)
                {
                    MatchDetail match = ParseMatch(line);
                }
            }
            file.Close();
        }

        private static void SaveData(string inputDirectory, string outputDirectory)
        {
            bool timeline = true;
            string key = ConfigurationManager.AppSettings["APIKey"];
            RiotClient riotClient = RiotApiLoader.CreateHttpClient(key);
            IEnumerable<string> filenames = Directory.EnumerateFiles(inputDirectory, "*", SearchOption.AllDirectories);

            File.WriteAllText(outputDirectory + "matches.csv", "Id,Duration,Region,Patch,Queue" + Environment.NewLine);
            File.WriteAllText(outputDirectory + "teams.csv", "MatchId,Id,Winner" + Environment.NewLine);
            File.WriteAllText(outputDirectory + "bans.csv", "MatchId,ChampionId" + Environment.NewLine);
            File.WriteAllText(outputDirectory + "participants.csv", "MatchId,TeamId,ParticipantId,ChampionId,HighestAchievedSeasonTier,Lane,Role,Assists,Deaths,FirstBloodKill,GoldEarned,Item0,Item1,Item2,Item3,Item4,Item5,Item6,Kills,MagicDamageDealt,MagicDamageDealtToChampions,MagicDamageTaken,MinionsKilled,NeutralMinionsKilled,PhysicalDamageDealt,PhysicalDamageDealtToChampions,PhysicalDamageTaken,SightWardsBoughtInGame,TotalDamageDealt,TotalDamageDealtToChampions,TotalDamageTaken,TotalHeal,TotalTimeCrowdControlDealt,TotalUnitsHealed,TrueDamageDealt,TrueDamageDealtToChampions,TrueDamageTaken,VisionWardsBoughtInGame,WardsKilled,WardsPlaced,CreepsPerMinDeltas010,GoldPerMinDeltas010,CreepsPerMinDeltas1020,GoldPerMinDeltas1020,CreepsPerMinDeltas2030,GoldPerMinDeltas2030,CreepsPerMinDeltas30,GoldPerMinDeltas30,SummonerId,SummonerName" + Environment.NewLine);
            File.WriteAllText(outputDirectory + "events.csv", "MatchId,ParticipantId,EventType,ItemAfter,ItemBefore,ItemId,LevelUpType,SkillSlot,Timestamp" + Environment.NewLine);
            File.WriteAllText(outputDirectory + "participantFrames.csv", "MatchId,ParticipantId,CurrentGold,Level,MinionsKilled,TotalGold,Xp,Timestamp" + Environment.NewLine);

            File.WriteAllText(outputDirectory + "error.csv", "MatchId,Message,StackTrace,Timestamp" + Environment.NewLine);

            foreach (string inputFileName in filenames)
            {
                string matchIdsString = File.ReadAllText(inputFileName);
                string[] parts = inputFileName.Split('\\');
                string region = parts[parts.Length - 1].Replace(".json", "");
                foreach (string s in matchIdsString.Split(',', '[', ']', '\r', '\n', ' ', '\t'))
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    long matchId = long.Parse(s);
                    try
                    {
                        MatchInfo match = GetMatchInfo(region, matchId, timeline, key, riotClient);
                        File.AppendAllText(outputDirectory + "matches.csv", match.Match.ToString() + Environment.NewLine);
                        File.AppendAllLines(outputDirectory + "teams.csv", match.Teams.Select(t => t.ToString()));
                        File.AppendAllLines(outputDirectory + "bans.csv", match.Bans.Select(b => b.ToString()));
                        File.AppendAllLines(outputDirectory + "participants.csv", match.Participants.Select(p => p.ToString()));
                        File.AppendAllLines(outputDirectory + "events.csv", match.Events.Select(e => e.ToString()));
                        File.AppendAllLines(outputDirectory + "participantFrames.csv", match.ParticipantFrames.Select(pf => pf.ToString()));
                        Thread.Sleep(1500);
                    }
                    catch (Exception e)
                    {
                        File.AppendAllText(outputDirectory + "error.csv", "\"" + matchId + "\",\"" + DateTime.Now + "\",\"" + e.Message + "\",\"" + e.StackTrace + "\"");
                        Console.WriteLine(matchId + "\n" + e.Message + "\n" + e.StackTrace);
                    }
                }
            }
        }

        private static MatchDetail ParseMatch(string matchData)
        {
            return JsonConvert.DeserializeObject<MatchDetail>(matchData);
        }

        private static string DownloadMatchString(string region, long matchId, bool timeline, string key, RiotClient riotClient)
        {

            return DownloadMatch(region, matchId, timeline, key, riotClient).ToString();
        }

        private static MatchInfo GetMatchInfo(string region, long matchId, bool timeline, string key, RiotClient riotClient)
        {
            MatchDetail matchDetail = DownloadMatch(region, matchId, timeline, key, riotClient);
            Match match = new Match(matchDetail);

            List<Team> teams = matchDetail.Teams.Select(team => new Team(team)
            {
                MatchId = match.Id
            }).ToList();

            List<Ban> bans = matchDetail.Teams.SelectMany(team => team.Bans == null ? new List<Ban>() : team.Bans.Select(champ => new Ban(champ)
            {
                MatchId = match.Id
            })).ToList();

            long? nullLong = null;
            string nullString = null;
            List<Participant> participants = matchDetail.Participants.Select(p => new Participant(p)
            {
                MatchId = match.Id,
                SummonerId = (matchDetail.ParticipantIdentities.First(pi => p.ParticipantId.Equals(pi.ParticipantId)).Player == null) ? nullLong : (matchDetail.ParticipantIdentities.First(pi => p.ParticipantId.Equals(pi.ParticipantId)).Player.SummonerId),
                SummonerName = (matchDetail.ParticipantIdentities.First(pi => p.ParticipantId.Equals(pi.ParticipantId)).Player == null) ? nullString : (matchDetail.ParticipantIdentities.First(pi => p.ParticipantId.Equals(pi.ParticipantId)).Player.SummonerName)
            }).ToList();

            List<Event> events = matchDetail.Timeline.Frames.SelectMany(frame => frame.Events == null ? new List<Event>() : frame.Events.Where(e => e.EventType.Equals(Enums.EventType.ITEM_PURCHASED) || e.EventType.Equals(Enums.EventType.SKILL_LEVEL_UP)).Select(e => new Event(e)
            {
                MatchId = match.Id
            })).ToList();

            List<ParticipantFrame> participantFrames = matchDetail.Timeline.Frames.SelectMany(frame => frame.ParticipantFrames.Select(pf => new ParticipantFrame(pf.Value)
            {
                MatchId = match.Id,
                Timestamp = frame.Timestamp
            })).ToList();

            MatchInfo matchInfo = new MatchInfo
            {
                Match = match,
                Teams = teams,
                Bans = bans,
                Participants = participants,
                Events = events,
                ParticipantFrames = participantFrames
            };
            return matchInfo;
        }

        private static MatchDetail DownloadMatch(string region, long matchId, bool timeline, string key, RiotClient riotClient)
        {
            return riotClient.Match.GetMatchById((RiotApiConfig.Regions)Enum.Parse(typeof(RiotApiConfig.Regions), region.ToUpper()), matchId, timeline);
        }
    }
}
