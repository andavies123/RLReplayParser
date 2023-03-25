namespace RLReplayParser.Models;

public class KeyFrame
{
	/// <summary>
	/// When this key frame occurs in seconds
	/// </summary>
	public float Time { get; set; }
	
	/// <summary>
	/// The frame number for this keyframe
	/// </summary>
	public int Frame { get; set; }
	
	/// <summary>
	/// The bit position for this keyframe in the stream
	/// </summary>
	public int Position { get; set; }
}