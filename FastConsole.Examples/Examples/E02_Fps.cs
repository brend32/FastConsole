using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace FastConsole.Examples.Examples;

public class E02_Fps
{
	public static void Run()
	{
		Windows.ForceUpgradeToAnsi(); // Вмикаємо розширений режим в консолі

		List<Element> elements = new List<Element>(); // Елементи для відмальовки
		elements.Add(new FpsText() // Fps датчик
		{
			Size = new Size(25, 1),
			Foreground = Color.Bisque,
			Template = "FPS: {0} ({1}ms)" // Стандартний шаблон (можна не вписувати). {0} - кількість кадрів, {1} - час кадру
		});
		Text targetFpsText = new Text() // Текст для відображення бажаного fps
		{
			Size = new Size(45, 1),
			Foreground = Color.Chocolate,
			Position = new Point(0, 1)
		};
		elements.Add(targetFpsText);

		int i = 0;
		while (true)
		{
			if (Time.TryUpdate()) // Оновлюємо час та тримаємо FPS у нормі
			{
				targetFpsText.Value = $"Target FPS: {Time.TargetFPS},  it is: {i} frame";
				i++;
				Element.UpdateAndRender(elements); // Оновлення і відмальовка елементів
			}

			while (Console.KeyAvailable) // Оброблення клавіш
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.End:
						return;
					
					case ConsoleKey.F:
						if (Time.TargetFPS == 6000)
						{
							Time.TargetFPS = 60;
						} 
						else if (Time.TargetFPS == 60)
						{
							Time.TargetFPS = 5;
						}
						else if (Time.TargetFPS == 5)
						{
							Time.TargetFPS = 15;
						}
						else if (Time.TargetFPS == 15)
						{
							Time.TargetFPS = 30;
						}
						else
						{
							Time.TargetFPS = 6000;
						}
						break;
						
				}
			}
		}
	}
}