using CourierConsole;

internal static class Program
{
	static SaveData     Data          { get; set; }
	static List<Player> ActivePlayers { get; set; }
	static List<Icon>   ActiveIcons   { get; set; }

	static void Main(string[] args)
	{
		// load save data
		Data = new();

		// select players
		ActivePlayers = new();
		var done = false;

		while (!done)
		{
			var options = new List<(string Text, Action Behavior)>();

			// add or remove players
			foreach (var player in Data.Players)
			{
				// check if player in ActivePlayers
				if (ActivePlayers.Contains(player))
				{
					options.Add(($"Remove {player.Name}", () => { ActivePlayers.Remove(player); }));
				}
				else
				{
					options.Add(($"Add {player.Name}", () => { ActivePlayers.Add(player); }));
				}
			}

			// register new players
			options.Add(("Register new player", () => { Data.RegisterNewPlayer(); }));

			// allow "done" if players have been selected
			if (ActivePlayers.Count > 0)
			{
				options.Add(("Done", () => { done = true; }));
			}

			var playerMenu = new Menu(options);
			playerMenu.Display("Add players to today's game:");
		}

		// select an icon for each player
		ActiveIcons = new();
		foreach (var player in ActivePlayers)
		{
			done = false;

			while (!done)
			{
				var options = new List<(string Text, Action Behavior)>();

				// choose from this player's existing icons
				foreach (var icon in player.Icons)
				{
					options.Add(($"Choose {icon.Name}", () =>
								    {
									    ActiveIcons.Add(icon);
									    done = true;
								    }));
				}

				// register new icons
				options.Add(("Register new icon", () => { Data.RegisterNewIcon(player); }));

				var iconMenu = new Menu(options);
				iconMenu.Display("Choose today's Icon for " + player.Name + ":");
			}
		}

		// exit if we reach the end of the program
		Exit();
	}

	// exit the program
	// error 0 if nothing is specified
	static void Exit(int exitCode = 0)
	{
		// save in a separate file if we're exiting on an error
		// otherwise, save in the default file
		if (exitCode == 0)
		{
			Console.WriteLine("Saving and closing...");
			Data.Save();
		}
		else
		{
			var fileName = $"save-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.json";
			Console.WriteLine($"Saving to {fileName} and closing...");
			Data.Save(fileName);
		}

		// bye, y'all
		Environment.Exit(exitCode);
	}
}