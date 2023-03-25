namespace RLReplayParser.Properties;

public static class NameProperty
{
	public static string Read(BinaryReader binaryReader)
	{
		binaryReader.ReadInt64(); // Byte Length;
		int textLength = binaryReader.ReadInt32();

		string text = new(binaryReader.ReadChars(textLength), 0, textLength - 1);

		return text;
	}
}