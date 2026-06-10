using System.Diagnostics;
using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace Game;

public class GameScene : Scene
{
	private Map _map;
	private Player _player;
    private Text _timeToFight;
	private double _timeForNextFight;
	private bool _fromFight;
	
	public GameScene()
	{
		_map = new Map();

		_player = new Player(100, 10, _map);
        _timeToFight = new Text()
        {
            Foreground = Color.Green,
            Position = new Point(2, 21),
            Size = new Size(30, 1)
        };
		
		Elements.Add(_map);
		Elements.Add(_player);
        Elements.Add(_timeToFight);
	    Elements.Add(new Text()
        {
            Foreground = Color.Brown,
            Position = new Point(2, 23),
            Size = new Size(30, 1),
            Value = "Press J to speed up timer"
        });
        
		UpdateFightTime();
	}

	private void UpdateFightTime()
	{
		_timeForNextFight = Time.NowSeconds + Random.Shared.Next(60, 85);
	}
	
	public override void Update()
	{
		if (_fromFight)
		{
			UpdateFightTime();
			_fromFight = false;
		}
		
		while (Console.KeyAvailable)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			_player.HandleInput(key.Key);

			switch (key.Key)
			{
				case ConsoleKey.J:
					_timeForNextFight = Time.NowSeconds + 4;
					break;
			}
		}

        _timeToFight.Value = $"Time to fight {_timeForNextFight - Time.NowSeconds:F0}s";

		if (_timeForNextFight < Time.NowSeconds)
		{
			OpenScene(new FightingScene(_player));
			_fromFight = true;
		}
	}
}