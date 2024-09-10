using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace FastConsole.Examples.Examples;

public class MyPlayerRenderer : Element // Наслідуємось від Element
{
	public State PlayerState { get; set; } 
		
	private Text _text;

	private Dictionary<State, Color> _colors = new Dictionary<State, Color>()
	{
		{ State.Alive, Color.Chartreuse },
		{ State.Dead, Color.Crimson },
		{ State.Frozen, Color.Aqua }
	};

	public enum State
	{
		Alive,
		Dead,
		Frozen
	}
	
	public MyPlayerRenderer()
	{
		_text = new Text()
		{
			Value = "Player"
		};
	}

	public void Heal()
	{
		PlayerState = State.Alive;
	}

	public void Kill()
	{
		PlayerState = State.Dead;
	}

	public void Froze()
	{
		PlayerState = State.Frozen;
	}
	
	// Оновлення елементу
	public override void Update()
	{
		_text.Position = Position;
		_text.Size = _text.FitSize;
		_text.Foreground = _colors[PlayerState];
		
		_text.Update();
	}

	// Відмальовка елементу
	protected override void OnRender()
	{
		// Позиція курсора дорівнює позиції
		// Щоб змістити курсор є команда
		// MoveCursor(Point delta)
		
		// Можна поєднати готові елементи
		_text.Render();
		// Або малювати вручну через
		// Renderer.Write(string str, Color? foreground = null, Color? background = null, TextStyle style = TextStyle.None);
		// Дивіться як зроблені базові компоненти
	}
}

public class E06_CustomElement
{
	public static void Run()
	{
		Windows.ForceUpgradeToAnsi(); // Вмикаємо розширений режим в консолі

		List<Element> elements = new List<Element>(); // Елементи для відмальовки
		MyPlayerRenderer myPlayerRenderer = new MyPlayerRenderer();
		elements.Add(myPlayerRenderer);
		
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
					
					case ConsoleKey.K:
						myPlayerRenderer.Kill();
						break;
					
					case ConsoleKey.F:
						myPlayerRenderer.Froze();
						break;
					
					case ConsoleKey.H:
						myPlayerRenderer.Heal();
						break;
				}
			}
		}
	}
}