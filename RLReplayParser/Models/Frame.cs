namespace RLReplayParser.Models;

public class Frame
{
	/// <summary>
	/// Time since the beginning of the match in seconds
	/// </summary>
	public float Time { get; set; }
	
	/// <summary>
	/// Time since the last frame in seconds
	/// </summary>
	public float Delta { get; set; }


	public List<object> Replications { get; set; } = new();
}