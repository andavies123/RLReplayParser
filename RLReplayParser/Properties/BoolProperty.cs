namespace RLReplayParser.Properties;

public static class BoolProperty
{
	public static bool Read(BinaryReader binaryReader)
	{
		binaryReader.ReadInt64(); // Byte Length
		bool value = binaryReader.ReadBoolean();

		return value;
	}
}