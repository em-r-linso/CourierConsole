using Newtonsoft.Json;

namespace CourierConsole;

[JsonObject(MemberSerialization.OptIn)]
public class SaveData
{
	public SaveData()
	{
		if (File.Exists("save.json"))
		{
			// try to populate the data from the save file
			try
			{
				var json = File.ReadAllText("save.json");
				JsonConvert.PopulateObject(json, this);
			}
			catch (Exception e)
			{
				Console.WriteLine("Save file is missing or corrupted. Creating new save file.");
			}
		}

		// if the file doesn't exist or the data is invalid, instantiate new data
		Players ??= new();
		Map     ??= new(150);
	}

	[JsonProperty] public List<Player>? Players     { get; private set; }
	[JsonProperty] public Map?          Map         { get; private set; }

	// save the game data to a json file
	// a path can be specified to save to a different file if necessary
	public void Save(string path = "save.json")
	{
		var json = JsonConvert.SerializeObject(this, Formatting.Indented);
		File.WriteAllText(path, json);
	}

	public void RegisterNewPlayer()
	{
		Console.Clear();
		Console.WriteLine("Input a new player's name:");
		var name = Console.ReadLine();
		Players.Add(new(name));
		Save();
		Console.WriteLine($"{name} has been registered.");
		Console.ReadKey();
	}

	public void RegisterNewIcon(Player player)
	{
		Console.Clear();

		Console.WriteLine("Every Icon begins as a non-magical, low-tech inanimate object.");
		Console.WriteLine("For the best results, it should be something that can be easily carried.");
		Console.WriteLine("Your Icon must be described by a single noun.");
		Console.WriteLine("For example, \"sword,\" \"book,\" or \"fishing rod.\"");
		Console.Write("This icon is a ");
		var item = Console.ReadLine();

		Console.Clear();
		Console.WriteLine($"This Icon is a {item}.");

		Console.WriteLine("An Icon gains its power when it becomes widely associated with a specific ideal.");
		Console.WriteLine("For the best results, choose an ideal that you want to promote and embody in the game world.");
		Console.WriteLine("Your ideal must be three words at most.");
		Console.WriteLine("For example, \"protecting innocent lives\" or \"scientific advancement.\"");
		Console.Write("This icon represents ");
		var ideal = Console.ReadLine();

		var newIcon = new Icon(item, ideal);
		player.Icons.Add(newIcon);

		Save();

		Console.Clear();
		Console.WriteLine($"{newIcon.ToString()} has been registered for {player.Name}.");
		Console.ReadKey();
	}
}