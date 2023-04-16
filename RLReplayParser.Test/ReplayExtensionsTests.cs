using FluentAssertions;
using RLReplayParser.Global;
using RLReplayParser.Models;

namespace RLReplayParser.Test;

[TestClass]
public class ReplayExtensionsTests
{
	#region GetBluePlayers Tests
	
	[DataTestMethod]
	[DataRow(3, 3)]
	[DataRow(5, 10)]
	[DataRow(10, 5)]
	public void GetBluePlayers_ReturnsAllBluePlayers_FromMixedCollection(int bluePlayersToAdd, int orangePlayersToAdd)
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(bluePlayersToAdd, orangePlayersToAdd);

		// Act
		IEnumerable<PlayerStat> bluePlayers = playerStats.GetBluePlayers();

		// Assert
		bluePlayers.Should().HaveCount(bluePlayersToAdd);
	}

	[TestMethod]
	public void GetBluePlayers_ReturnsAllPlayers_FromACollectionContainingOnlyBluePlayers()
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(10, 0);

		// Act
		IEnumerable<PlayerStat> bluePlayers = playerStats.GetBluePlayers();

		// Assert
		bluePlayers.Should().HaveCount(playerStats.Count);
	}

	[TestMethod]
	public void GetBluePlayers_ReturnsEmptyCollection_FromACollectionContainingNoBluePlayers()
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(0, 10);

		// Act
		IEnumerable<PlayerStat> bluePlayers = playerStats.GetBluePlayers();

		// Assert
		bluePlayers.Should().BeEmpty();
	}

	[TestMethod]
	public void GetBluePlayers_ReturnsNull_FromANullCollection()
	{
		// Arrange
		// Act
		IEnumerable<PlayerStat> bluePlayers = ((List<PlayerStat>)null).GetBluePlayers();

		// Assert
		bluePlayers.Should().BeNull();
	}

	[TestMethod]
	public void GetBluePlayers_ReturnsAllBluePlayers_FromMixedCollectionContainingNullPlayers()
	{
		// Arrange
		const int bluePlayersToAdd = 10;
		List<PlayerStat> playerStats = GeneratePlayerStatList(bluePlayersToAdd, 10);
		playerStats.Insert(0, null); // Add null PlayerStat to the beginning of the list
		playerStats.Add(null); // Add null PlayerStat to the end of the list

		// Act
		IEnumerable<PlayerStat> bluePlayers = playerStats.GetBluePlayers();

		// Assert
		bluePlayers.Should().HaveCount(bluePlayersToAdd);
	}

	[TestMethod]
	public void GetBluePlayers_ReturnsOnlyBluePlayers_FromMixedCollection()
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(100, 100);
		
		// Act
		IEnumerable<PlayerStat> bluePlayers = playerStats.GetBluePlayers();

		// Assert
		bluePlayers.Should().OnlyContain(player => player.Team == Constants.BlueTeamNumber);
	}
	
	#endregion
	
	#region GetOrangePlayers Tests
	
	[DataTestMethod]
	[DataRow(3, 3)]
	[DataRow(5, 10)]
	[DataRow(10, 5)]
	public void GetOrangePlayers_ReturnsAllOrangePlayers_FromMixedCollection(int bluePlayersToAdd, int orangePlayersToAdd)
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(bluePlayersToAdd, orangePlayersToAdd);

		// Act
		IEnumerable<PlayerStat> orangePlayers = playerStats.GetOrangePlayers();

		// Assert
		orangePlayers.Should().HaveCount(orangePlayersToAdd);
	}

	[TestMethod]
	public void GetOrangePlayers_ReturnsAllPlayers_FromACollectionContainingOnlyOrangePlayers()
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(0, 10);

		// Act
		IEnumerable<PlayerStat> orangePlayers = playerStats.GetOrangePlayers();

		// Assert
		orangePlayers.Should().HaveCount(playerStats.Count);
	}

	[TestMethod]
	public void GetOrangePlayers_ReturnsEmptyCollection_FromACollectionContainingNoOrangePlayers()
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(10, 0);

		// Act
		IEnumerable<PlayerStat> orangePlayers = playerStats.GetOrangePlayers();

		// Assert
		orangePlayers.Should().BeEmpty();
	}

	[TestMethod]
	public void GetOrangePlayers_ReturnsNull_FromANullCollection()
	{
		// Arrange
		// Act
		IEnumerable<PlayerStat> orangePlayers = ((List<PlayerStat>)null).GetOrangePlayers();

		// Assert
		orangePlayers.Should().BeNull();
	}

	[TestMethod]
	public void GetOrangePlayers_ReturnsAllOrangePlayers_FromMixedCollectionContainingNullPlayers()
	{
		// Arrange
		const int orangePlayersToAdd = 10;
		List<PlayerStat> playerStats = GeneratePlayerStatList(10, orangePlayersToAdd);
		playerStats.Insert(0, null); // Add null PlayerStat to the beginning of the list
		playerStats.Add(null); // Add null PlayerStat to the end of the list

		// Act
		IEnumerable<PlayerStat> orangePlayers = playerStats.GetOrangePlayers();

		// Assert
		orangePlayers.Should().HaveCount(orangePlayersToAdd);
	}

	[TestMethod]
	public void GetOrangePlayers_ReturnsOnlyOrangePlayers_FromMixedCollection()
	{
		// Arrange
		List<PlayerStat> playerStats = GeneratePlayerStatList(100, 100);
		
		// Act
		IEnumerable<PlayerStat> orangePlayers = playerStats.GetOrangePlayers();

		// Assert
		orangePlayers.Should().OnlyContain(player => player.Team == Constants.OrangeTeamNumber);
	}
	
	#endregion

	#region Helper Methods

	private static readonly Random Random = new();

	private static PlayerStat GeneratePlayerStat(int? team = null)
	{
		return new PlayerStat
		{
			Team = team ?? Random.Next()
		};
	}

	private static List<PlayerStat> GeneratePlayerStatList(int bluePlayersToAdd, int orangePlayersToAdd)
	{
		List<PlayerStat> playerStats = new();
		
		for (int blueIndex = 0; blueIndex < bluePlayersToAdd; blueIndex++)
			playerStats.Add(GeneratePlayerStat(team: Constants.BlueTeamNumber));
		
		for (int orangeIndex = 0; orangeIndex < orangePlayersToAdd; orangeIndex++)
			playerStats.Add(GeneratePlayerStat(team: Constants.OrangeTeamNumber));

		return playerStats;
	}

	#endregion
}