using RLReplayParser.Models;

namespace RLReplayParser.Global;

/// <summary>
/// Extension methods to help get clearer data out of the <see cref="Replay"/> model
/// </summary>
public static class ReplayExtensions
{
	/// <summary>
	/// Returns the players that are on the blue team from a collection of all players
	///
	/// Returns null if the received collection is null
	/// </summary>
	/// <param name="allPlayers">Collection of all the players</param>
	/// <returns>Collection of players that are on the blue team</returns>
	public static IEnumerable<PlayerStat> GetBluePlayers(this IEnumerable<PlayerStat> allPlayers) =>
		allPlayers?.Where(player => player?.Team == Constants.BlueTeamNumber);

	/// <summary>
	/// Returns the players that are on the orange team from a collection of all players
	///
	/// Returns null if the received collection is null
	/// </summary>
	/// <param name="allPlayers">Collection of all the players</param>
	/// <returns>Collection of players that are on the orange team</returns>
	public static IEnumerable<PlayerStat> GetOrangePlayers(this IEnumerable<PlayerStat> allPlayers) =>
		allPlayers?.Where(player => player?.Team == Constants.OrangeTeamNumber);
}