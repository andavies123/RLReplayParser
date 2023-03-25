namespace RLReplayParser.Properties;

public static class IntProperty
{
	public static int Read(BinaryReader binaryReader)
	{
		binaryReader.ReadInt64(); // Byte Length
		return binaryReader.ReadInt32(); // Value
	}
}