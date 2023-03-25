namespace RLReplayParser.Properties;

public static class StrProperty
{
	public static string Read(BinaryReader binaryReader)
	{
		int byteLength = binaryReader.ReadInt32(); // Byte Length
		binaryReader.ReadInt32(); // Index or extension of byte length
		int textSize = binaryReader.ReadInt32();

		var bytes = binaryReader.ReadBytes(byteLength - 4);
		
		// Byte length is used since some names contain bad characters and this is a better way to capture the correct amount
		string text = new(bytes.ToList().Select(b => (char)b).ToArray(), 0, byteLength - 5); // Remove end null character
		
		return text;
	}
}