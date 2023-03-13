using Newtonsoft.Json;

namespace CourierConsole;

[JsonObject(MemberSerialization.OptIn)]
public class PlayerGroup : MapIem
{
	public PlayerGroup()
	{
		Character    = '@';
		DrawPriority = 99999;
	}
}