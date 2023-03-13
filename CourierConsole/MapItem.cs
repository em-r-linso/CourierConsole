using Newtonsoft.Json;

namespace CourierConsole;

[JsonObject(MemberSerialization.OptIn)]
public class MapIem
{
	public MapIem(int x = 0, int y = 0, int drawPriority = 0, char character = '?')
	{
		Position     = (x, y);
		DrawPriority = drawPriority;
		Character    = character;
	}

	[JsonProperty] public (int, int) Position { get; set; }

	// shorthand for Position.Item1
	public int X
	{
		get => Position.Item1;
		set => Position = (value, Position.Item2);
	}

	// shorthand for Position.Item2
	public int Y
	{
		get => Position.Item2;
		set => Position = (Position.Item1, value);
	}

	[JsonProperty] public int  DrawPriority { get; protected set; } // default 0; higher is drawn on top of lower
	[JsonProperty] public char Character    { get; protected set; } // the character to draw on the map
}