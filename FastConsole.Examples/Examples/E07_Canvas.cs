using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace FastConsole.Examples.Examples;

public class E07_Canvas
{
	public static void Run()
	{
		Windows.ForceUpgradeToAnsi(); // Вмикаємо розширений режим в консолі

		List<Element> elements = new List<Element>(); // Елементи для відмальовки
		Canvas canvas = new Canvas(new Size(20, 10)); // Рормір області для малювання
		canvas.Fill(Color.Bisque); // Заповнити усю область
		canvas.CellWidth = 2; // Ширина клітинки. Стандартно - 2
		
		canvas.FillRect(2, 2, 16, 6, Color.Aqua); // Заповнити прямокутник
		canvas.FillCell(0, 0, Color.Brown); // Заповнюємо клітинку
		canvas.FillCell(0, 9, Color.Blue);
		canvas.FillCell(19, 9, Color.Green);
		canvas.FillCell(19, 0, Color.Yellow);
		
		elements.Add(canvas);
		
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
					
					case ConsoleKey.E:
						canvas.CellWidth++;
						break;
					
					case ConsoleKey.S:
						canvas.CellWidth--;
						break;
				}
			}
		}
	}
}