namespace RLReplayParser.Models;

public class PlayerStat
{
	public string Name { get; set; }
	public (string key, string value) Platform { get; set; }
	public long OnlineID { get; set; }
	public int Team { get; set; }
	public int Score { get; set; }
	public int Goals { get; set; }
	public int Assists { get; set; }
	public int Saves { get; set; }
	public int Shots { get; set; }
	public bool BBot { get; set; } // IsBot
}