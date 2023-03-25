namespace RLReplayParser.Properties;

public static class FloatProperty
{
	public static float Read(BinaryReader binaryReader)
	{
		binaryReader.ReadInt64(); // Byte Length
		return binaryReader.ReadSingle();
	}
}