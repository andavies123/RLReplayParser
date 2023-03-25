namespace RLReplayParser.Models;

public class ReplayContent
{
	public List<string> Levels { get; set; } = new();
	public List<KeyFrame> KeyFrames { get; set; } = new();
	public List<Frame> Frames { get; set; } = new();
}