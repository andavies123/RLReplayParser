using System.Collections;
using RLReplayParser.Models;

namespace RLReplayParser.Properties;

public static class ArrayProperty
{
	public static IList Read(BinaryReader binaryReader, string arrayName)
	{
		binaryReader.ReadInt64(); // Bytes
		int arrayLength = binaryReader.ReadInt32();

		switch (arrayName)
		{
			case nameof(ReplayHeader.Goals): return ReadArray<Goal>(binaryReader, arrayLength);
			case nameof(ReplayHeader.HighLights): return ReadArray<HighLight>(binaryReader, arrayLength);
			case nameof(ReplayHeader.PlayerStats): return ReadArray<PlayerStat>(binaryReader, arrayLength);
			default: Console.WriteLine($"{arrayName} array does not exist"); break;
		}

		return null;
	}

	public static List<T> ReadArray<T>(BinaryReader binaryReader, int arrayLength) where T : new()
	{
		List<T> list = new();

		for (int i = 0; i < arrayLength; i++)
			list.Add(ReadObject<T>(binaryReader));

		return list;
	}

	public static T ReadObject<T>(BinaryReader binaryReader) where T : new()
	{
		T obj = new();
		
		while(ReplayReader.ReadNextProperty(binaryReader, obj)) { }

		return obj;
	}
}