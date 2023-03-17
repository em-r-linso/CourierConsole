using Newtonsoft.Json;

namespace CourierConsole;

[JsonObject(MemberSerialization.OptIn)]
public class PlayerGroup : MapItem
{
	public PlayerGroup()
	{
		Character    = '@';
		DrawPriority = 99999;
	}
}