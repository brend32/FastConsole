using System.Diagnostics;
using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace Game;

public class GameScene : Scene
{
	private Map _map;
	private Player _player;
	
	public GameScene()
	{
		_map = new Map();

		_player = new Player(100, 10, _map);	
		
		Elements.Add(_map);
		Elements.Add(_player);
	}
	
	public override void Update()
	{
		while (Console.KeyAvailable)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			switch (key.Key)
			{
				case ConsoleKey.UpArrow:
				case ConsoleKey.W:
					_player.Move(new Point(0, -1));
					break;
				
				case ConsoleKey.DownArrow:
				case ConsoleKey.S:
					_player.Move(new Point(0, 1));
					break;
				
				case ConsoleKey.LeftArrow:
				case ConsoleKey.A:
					_player.Move(new Point(-1, 0));
					break;
				
				case ConsoleKey.RightArrow:
				case ConsoleKey.D:
					_player.Move(new Point(1, 0));
					break;
			}
		}	
	}
}