using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace FastConsole.Examples.Examples;

public class E01_HelloWorld
{
	public static void Run()
	{
		Windows.ForceUpgradeToAnsi(); // Вмикаємо розширений режим в консолі

		List<Element> elements = new List<Element>(); // Елементи для відмальовки
		elements.Add(new Text() // Текст, що буде відбальовуватись
		{
			Size = new Size(15, 1),
			Foreground = Color.Bisque,
			Value = "Hello World"
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