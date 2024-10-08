using System.Diagnostics;
using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace Game;

public class FightingScene : Scene
{
	private Player _player;
	private FightingArea _area;
	
	public FightingScene(Player player)
	{
		_player = player;
		_area = new FightingArea(_player)
		{
			Size = new Size(80, 18),
		};
		_area.OnFightEnd += FightEnded;
		
		Elements.Add(_area);
		
		_area.StartFight();
	}

	private void FightEnded(bool isPlayerWin)
	{
		if (isPlayerWin)
		{
			_player.Heal(_player.MaxHealth);
		}
		
		//TODO: Open GameOver scene
		CloseScene();
	}
	
	public override void Update()
	{
		while (Console.KeyAvailable)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			_area.HandleInput(key.Key);	
		}
	}
}