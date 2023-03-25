namespace RLReplayParser.Properties;

public static class ByteProperty
{
	public static (string key, string value) Read(BinaryReader binaryReader)
	{
		binaryReader.ReadInt64(); // Byte Length
		string key = ReplayReader.ReadSizeValuePair(binaryReader);
		string value = ReplayReader.ReadSizeValuePair(binaryReader);

		return (key, value);
	}
}