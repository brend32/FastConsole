using System.Diagnostics;
using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace Game;

public class GameScene : Scene
{
	private Canvas _map;
	private Player _player;
	
	public GameScene()
	{
		_map = new Canvas(new Size(60, 20))
		{
			CellWidth = 2
		};
		_map.Fill(Color.Green);

		_player = new Player(100, 10);	
		
		Elements.Add(_map);
		Elements.Add(_player);
	}
	
	public override void Update()
	{
			
	}
}