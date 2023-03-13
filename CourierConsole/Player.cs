using Newtonsoft.Json;

namespace CourierConsole;

[JsonObject(MemberSerialization.OptIn)]
public class Player
{
	public Player(string name)
	{
		Name     = name;
		JoinDate = DateTime.Now;
		Icons    = new();
	}

	[JsonProperty] public string     Name     { get; private set; }
	[JsonProperty]        DateTime   JoinDate { get; set; }
	[JsonProperty] public List<Icon> Icons    { get; private set; }
}