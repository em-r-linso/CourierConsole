using Newtonsoft.Json;

namespace CourierConsole;

/*
 * Note on travel speeds:
 *	4km/hr is an average walking speed
 *	30km is the maximum distance travelled per day (this takes 7.5 hours)
 *	If the map is 10 days wide, it is 300km wide
 *
 * Using a 300km wide circle:
 *	The area of a circle with diameter 300km is 70685km2
 *	Similar in size to Ireland
 *	All points generated will be within 150km of the center
 */

[JsonObject(MemberSerialization.OptIn)]
public class Map
{
	public Map(int radius)
	{
		Radius = radius;
	}

	[JsonProperty] int           Radius { get; set; }
	[JsonProperty] List<MapIem>? Items  { get; set; }

	public void PlaceOnMap(MapIem item)
	{
		Items ??= new();

		// add the item if it isn't already on the map
		if (!Items.Contains(item))
		{
			Items.Add(item);
		}
	}

	public void Display(int scale = 10) // scale is how many kn each point represents
	{
		Console.Clear();

		// draw every point on the map
		for (var y = -Radius; y <= Radius; y += scale)
		{
			for (var x = -Radius; x <= Radius; x += scale)
			{
				// check if (x,y) is less than 150 units away from the center
				if (Math.Sqrt(x * x + y * y) <= Radius)
				{


					// find items at (x, y)
					// (or rather, between (x, y) and (x + scale, y + scale))
					// and get the top 3 in terms of draw priority
					var items = Items?
							   .Where(i => i.X >= x && i.X < x + scale && i.Y >= y && i.Y < y + scale)
							   .OrderByDescending(i => i.DrawPriority)
							   .Take(3);

					// draw up to 3 items
					switch (items?.Count())
					{
						case 0:
							Console.Write(" . ");
							break;
						case 1:
							Console.Write($" {items.First().Character} ");
							break;
						case 2:
							Console.Write($" {items.First().Character}{items.Last().Character}");
							break;
						case 3:
							Console.Write($"{items.First().Character}{items.Skip(1).First().Character}{items.Last().Character}");
							break;
					}
				}

				// if (x,y) is outside the map, draw a blank space
				else
				{
					Console.Write("   ");
				}
			}

			Console.WriteLine();
		}

		Console.ReadKey();
	}
}