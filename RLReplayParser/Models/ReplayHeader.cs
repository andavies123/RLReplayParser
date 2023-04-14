namespace RLReplayParser.Models;

public class ReplayHeader
{
	public int TeamSize { get; set; }
	public int UnfairTeamSize { get; set; }
	public int PrimaryPlayerTeam { get; set; }
	public int Team0Score { get; set; }
	public int Team1Score { get; set; }
	public int ReplayVersion { get; set; }
	public int ReplayLastSaveVersion { get; set; }
	public int GameVersion { get; set; }
	public int BuildID { get; set; }
	public int Changelist { get; set; }
	public int ReserveMegabytes { get; set; }
	public int MaxChannels { get; set; }
	public int MaxReplaySizeMB { get; set; }
	public int NumFrames { get; set; }
    
	public float RecordFPS { get; set; }
	public float KeyframeDelay { get; set; }
    
	public string ReplayName { get; set; }
	public string BuildVersion { get; set; }
	public string Id { get; set; }
	public string MapName { get; set; }
	public string Date { get; set; }
	public string MatchType { get; set; }
	public string PlayerName { get; set; }
    
	public List<Goal> Goals { get; set; }
	public List<HighLight> HighLights { get; set; }
	public List<PlayerStat> PlayerStats { get; set; }
}