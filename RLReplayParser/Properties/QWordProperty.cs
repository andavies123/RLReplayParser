namespace RLReplayParser.Properties;

public static class QWordProperty
{
	public static long Read(BinaryReader binaryReader)
	{
		binaryReader.ReadInt64(); // Byte Length
		return binaryReader.ReadInt64(); // Value
	}
}