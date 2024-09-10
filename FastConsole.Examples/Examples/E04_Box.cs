using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace FastConsole.Examples.Examples;

public class E04_Box
{
	public static void Run()
	{
		Windows.ForceUpgradeToAnsi(); // Вмикаємо розширений режим в консолі

		List<Element> elements = new List<Element>(); // Елементи для відмальовки
		Box box1 = Box.DefaultBox(new Point(0, 0), new Size(30, 10), new Text()
		{
			Background = Color.Brown,
			Foreground = Color.Cyan,
			Value = "Some long text\nHow are you?\nI'm fine :)"
		});
		box1.Background = Color.Coral;
		box1.Foreground = Color.Black;
		elements.Add(box1);
		Box box2 = Box.DefaultBox(new Point(37, 0), new Size(35, 10), new Text()
		{
			Background = Color.Brown,
			Foreground = Color.Cyan,
			BackgroundOnlyWhereTextIs = true,
			Value = "Some long text\nHow are you?\nI'm fine :)\nBackground only where text is"
		});
		box2.Background = Color.Coral;
		box2.Foreground = Color.Black;
		elements.Add(box2);

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