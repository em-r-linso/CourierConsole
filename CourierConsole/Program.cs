using CourierConsole;

internal static class Program
{
	static SaveData     Data          { get; set; }
	static List<Player> ActivePlayers { get; set; }
	static List<Icon>   ActiveIcons   { get; set; }

	static void Main(string[] args)
	{
		//BigTitle();

		// load save data
		Data = new();

		// show map
		Data.Map.Display();

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

			// allow"done" if players have been selected
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
					options.Add(($"Choose {icon.ToString()}", () =>
								    {
									    ActiveIcons.Add(icon);
									    done = true;
								    }));
				}

				// register new icons
				options.Add(("Register new icon", () => { Data.RegisterNewIcon(player); }));

				var iconMenu = new Menu(options);
				iconMenu.Display($"Choose an icon for {player.Name}:");
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

	static void BigTitle()
	{
		Console.Clear();
		Console.WriteLine("        CCCCCCCCCCCCC                                                       iiii");
		Console.WriteLine("     CCC::::::::::::C                                                      i::::i");
		Console.WriteLine("   CC:::::::::::::::C                                                       iiii");
		Console.WriteLine("  C:::::CCCCCCCC::::C");
		Console.WriteLine(" C:::::C       CCCCCC   ooooooooooo   uuuuuu    uuuuuu rrrrr   rrrrrrrrr  iiiiiii     eeeeeeeeeeee    rrrrr   rrrrrrrrr");
		Console.WriteLine("C:::::C               oo:::::::::::oo u::::u    u::::u r::::rrr:::::::::r i:::::i   ee::::::::::::ee  r::::rrr:::::::::r");
		Console.WriteLine("C:::::C              o:::::::::::::::ou::::u    u::::u r:::::::::::::::::r i::::i  e::::::eeeee:::::eer:::::::::::::::::r");
		Console.WriteLine("C:::::C              o:::::ooooo:::::ou::::u    u::::u rr::::::rrrrr::::::ri::::i e::::::e     e:::::err::::::rrrrr::::::r");
		Console.WriteLine("C:::::C              o::::o     o::::ou::::u    u::::u  r:::::r     r:::::ri::::i e:::::::eeeee::::::e r:::::r     r:::::r");
		Console.WriteLine("C:::::C              o::::o     o::::ou::::u    u::::u  r:::::r     rrrrrrri::::i e:::::::::::::::::e  r:::::r     rrrrrrr");
		Console.WriteLine("C:::::C              o::::o     o::::ou::::u    u::::u  r:::::r            i::::i e::::::eeeeeeeeeee   r:::::r");
		Console.WriteLine(" C:::::C       CCCCCCo::::o     o::::ou:::::uuuu:::::u  r:::::r            i::::i e:::::::e            r:::::r");
		Console.WriteLine("  C:::::CCCCCCCC::::Co:::::ooooo:::::ou:::::::::::::::uur:::::r           i::::::ie::::::::e           r:::::r");
		Console.WriteLine("   CC:::::::::::::::Co:::::::::::::::o u:::::::::::::::ur:::::r           i::::::i e::::::::eeeeeeee   r:::::r");
		Console.WriteLine("     CCC::::::::::::C oo:::::::::::oo   uu::::::::uu:::ur:::::r           i::::::i  ee:::::::::::::e   r:::::r");
		Console.WriteLine("        CCCCCCCCCCCCC   ooooooooooo       uuuuuuuu  uuuurrrrrrr           iiiiiiii    eeeeeeeeeeeeee   rrrrrrr");
		Console.ReadKey();
	}
}