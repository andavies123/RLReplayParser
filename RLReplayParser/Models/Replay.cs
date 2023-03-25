namespace RLReplayParser.Models;

public class Replay
{
    public ReplayHeader Header { get; set; } = new();
    public ReplayContent Content { get; set; } = new();
}