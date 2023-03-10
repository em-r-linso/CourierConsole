namespace CourierConsole;

public class Player
{
	public Player(string name)
	{
		Name     = name;
		JoinDate = DateTime.Now;
		Icons    = new();
	}

	public string     Name     { get; set; }
	public DateTime   JoinDate { get; set; }
	public List<Icon> Icons    { get; set; }
}