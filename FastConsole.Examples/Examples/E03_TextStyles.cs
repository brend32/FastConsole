using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace FastConsole.Examples.Examples;

public class E03_TextStyles
{
	public static void Run()
	{
		Windows.ForceUpgradeToAnsi(); // Вмикаємо розширений режим в консолі

		List<Element> elements = new List<Element>(); // Елементи для відмальовки
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 0),
			Value = "Normal"
		});
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 1),
			Background = Color.Coral,
			Value = "Background"
		});
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 2),
			Foreground = Color.Coral,
			Value = "Foreground"
		});
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 3),
			Value = "Bold",
			Style = TextStyle.Bold
		});
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 4),
			Value = "Italic",
			Style = TextStyle.Italic
		});
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 5),
			Value = "Underline",
			Style = TextStyle.Underline
		});
		elements.Add(new Text()
		{
			Size = new Size(25, 1),
			Position = new Point(0, 6),
			Value = "Bold and Italic",
			Style = TextStyle.Bold | TextStyle.Italic // Оператор | поєднує і Bold і Italic
		});
		
		while (true)
		{
			if (Time.TryUpdate()) // Оновлюємо час та тримаємо FPS у нормі
			{
				Element.UpdateAndRender(elements); // Оновлення і відмальовка елементів
			}

			while (Console.KeyAvailable) // Оброблення клавіш
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.End:
						return;
				}
			}
		}
	}
}