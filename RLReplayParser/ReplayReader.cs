using System.Reflection;
using System.Text;
using RLReplayParser.Models;
using RLReplayParser.Properties;

namespace RLReplayParser;

public static class ReplayReader
{
	private const string ReplayExtension = ".replay";

	private const string None = "None";

	public static bool TryReadReplayFolder(string folderPath, out List<Replay> replays)
	{
		replays = new List<Replay>();

		DirectoryInfo directoryInfo = new(folderPath);
		if (!directoryInfo.Exists)
			return false;

		foreach (FileInfo fileInfo in directoryInfo.GetFiles())
		{
			if (fileInfo.Extension != ReplayExtension)
				continue;

			if (TryReadReplayFile(fileInfo.FullName, out Replay? replay) && replay != null)
				replays.Add(replay);
		}
		
		Console.WriteLine($"Parsed {replays.Count} replay files");
		return replays.Count > 0;
	}
	
	/// <summary>
	/// Attempts to read the contents of replay file given the path of the file
	/// </summary>
	/// <param name="filePath">The path of the replay file</param>
	/// <param name="replay">Replay object that contains the data for the replay</param>
	/// <returns>False if file does not exist or the file extension is wrong. True otherwise</returns>
	public static bool TryReadReplayFile(string filePath, out Replay? replay)
	{
		replay = null;
		
		if (!File.Exists(filePath) || !Path.GetExtension(filePath).Equals(ReplayExtension, StringComparison.OrdinalIgnoreCase))
			return false;

		Console.WriteLine($"Reading {Path.GetFileName(filePath)}");
		
		using Stream stream = File.Open(filePath, FileMode.Open);
		using BinaryReader binaryReader = new(stream, Encoding.UTF8, false);
		
		replay = ReadReplay(binaryReader);
		
		binaryReader.Close();
		binaryReader.Dispose();
		stream.Close();
		stream.Dispose();
		
		return true;
	}

	private static Replay ReadReplay(BinaryReader binaryReader)
	{
		Replay replay = new();

		// 4 bytes = Length from byte 9 to the end of replay header
		binaryReader.ReadInt32();
		// 4 bytes = Unsure (very large number if together)
		binaryReader.ReadInt32();
		// 4 bytes = 868 - seems to be pretty consistent
		binaryReader.ReadInt32();
		// 4 bytes = ? Possibly main version. If this is > 17 then an extra number is read
		int mainVersion = binaryReader.ReadInt32();
		// 4 bytes = ? Possibly minor version if main version is > 17
		if (mainVersion > 17)
			binaryReader.ReadInt32();
		
		// 4 bytes = length of some text
		int textLength = binaryReader.ReadInt32();
		// ^ bytes = the text (end of header)
		binaryReader.ReadBytes(textLength);
		
		// Read until reaching end of object
		while (ReadNextProperty(binaryReader, replay.Header)) { }
		ReadContent(binaryReader, replay.Content);

		return replay;
	}

	private static void ReadContent(BinaryReader binaryReader, ReplayContent content)
	{
		binaryReader.ReadInt32(); // Length of the rest of the file after the next 4 bytes
		binaryReader.ReadSingle(); // Unsure. Really large number. Padding?
		
		int numLevels = binaryReader.ReadInt32();
		for (int i = 0; i < numLevels; i++)
			content.Levels.Add(ReadSizeValuePair(binaryReader));

		int keyFrameCount = binaryReader.ReadInt32(); // Length?

		for (int i = 0; i < keyFrameCount; i++)
		{
			content.KeyFrames.Add(new KeyFrame
			{
				Time = binaryReader.ReadSingle(),
				Frame = binaryReader.ReadInt32(),
				Position = binaryReader.ReadInt32()
			});
		}
	}

	public static bool ReadNextProperty(BinaryReader binaryReader, object obj)
	{
		string propertyName = ReadSizeValuePair(binaryReader);
		propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

		if (propertyName == None)
			return false;
		
		string propertyType = ReadSizeValuePair(binaryReader);

		if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(propertyType))
			return false;

		PropertyInfo? propertyInfo = obj.GetType().GetProperty(propertyName);

		if (propertyInfo == null)
		{
			Console.WriteLine($"{propertyName} does not exist for type {obj.GetType()}");
			return false;
		}
		
		switch (propertyType)
		{
			case nameof(IntProperty): propertyInfo.SetValue(obj, IntProperty.Read(binaryReader)); break;
			case nameof(StrProperty): propertyInfo.SetValue(obj, StrProperty.Read(binaryReader)); break;
			case nameof(NameProperty): propertyInfo.SetValue(obj, NameProperty.Read(binaryReader)); break;
			case nameof(ByteProperty): propertyInfo.SetValue(obj, ByteProperty.Read(binaryReader)); break;
			case nameof(QWordProperty): propertyInfo.SetValue(obj, QWordProperty.Read(binaryReader)); break;
			case nameof(BoolProperty): propertyInfo.SetValue(obj, BoolProperty.Read(binaryReader)); break;
			case nameof(FloatProperty): propertyInfo.SetValue(obj, FloatProperty.Read(binaryReader)); break;
			case nameof(ArrayProperty): propertyInfo.SetValue(obj, ArrayProperty.Read(binaryReader, propertyName)); break;
		}

		return true;
	}

	public static string ReadSizeValuePair(BinaryReader binaryReader)
	{
		int size = binaryReader.ReadInt32();
		string name = new(binaryReader.ReadChars(size), 0, size - 1);

		return name;
	}
}