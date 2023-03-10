namespace CourierConsole;

public class Icon
{
	public string Item { get; set; }
	public string Ideal { get; set; }

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