using Newtonsoft.Json;

namespace CourierConsole;

[JsonObject(MemberSerialization.OptIn)]
public class Icon
{
	[JsonProperty] string Item { get; set; }
	[JsonProperty] string Ideal { get; set; }

	public override string ToString()
	{
		return $"the {Item} of {Ideal}";
	}

	public Icon(string item, string ideal)
	{
		Item = item;
		Ideal = ideal;
	}
}