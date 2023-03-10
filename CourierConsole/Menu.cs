namespace CourierConsole;

public class Menu
{
	public Menu(List<(string Text, Action Behavior)> options)
	{
		Options = options;
	}

	List<(string Text, Action Behavior)> Options { get; }

	public void Display(string title = "Select an option:")
	{
		Console.Clear();

		// get the max width of an option number
		// 1-9 -> 1, 10-99 -> 2, 100-999 -> 3, etc.
		var optionWidth = Options.Count.ToString().Length;

		while (true)
		{
			// display the menu title
			Console.WriteLine(title);

			// display the options
			for (var i = 0; i < Options.Count; i++)
			{
				Console.WriteLine($"\t[{(i + 1).ToString().PadLeft(optionWidth)}] {Options[i].Text}");
			}

			// get the user's input
			// keystroke if there are less than 10 options
			// line if there are 10 or more options
			var input = optionWidth == 1 ? Console.ReadKey().KeyChar.ToString() : Console.ReadLine();

			// if the input is a valid number, run the corresponding option
			if (int.TryParse(input, out var index))
			{
				if (index > 0 && index <= Options.Count)
				{
					Options[index - 1].Behavior();

					return;
				}
			}

			// if the input is invalid, display an error message and retry
			Console.WriteLine("Invalid input.");
		}
	}
}