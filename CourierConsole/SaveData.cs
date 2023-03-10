using Newtonsoft.Json;

namespace CourierConsole;

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
	}

	public List<Player>? Players { get; set; }

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
		Console.WriteLine("Input a new icon's name:");
		var name = Console.ReadLine();
		player.Icons.Add(new(name));
		Save();
		Console.WriteLine($"{name} has been registered for {player.Name}.");
		Console.ReadKey();
	}
}