﻿using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;
using Game;
using Game.Elements;

class MenuScene : Scene
{
	private MenuButton[] _buttons;
	private int _selectedIndex = 0;
	private int _boxSize = 32;

	private FightingArea _area;

	public MenuScene()
	{
		_buttons = new[]
		{
			new MenuButton("Start", () =>
			{
				OpenScene(new GameScene());
			}),
			new MenuButton("About", () =>
			{
				OpenScene(new AboutScene());
			}),
			new MenuButton("Settings", () =>
			{
				OpenScene(new SettingsScene());
			}),
			new MenuButton("Exit", () =>
			{
				SceneManager.Exit();
			}),
		};
		
		Elements.Add(Box.DefaultBox(new Point(0, 0), new Size(32, 4), new Text()
		{
			Alignment = Alignment.Center,
			Value = "Adventure game\nversion: 0.1"
		}));

		Player player = new Player(100, 25, null);
		FightingArea area = _area = new FightingArea(player)
		{
			Size = new Size(80, 20),
			Position = new Point(30, 0)
		};
		

		FlexBox box = new FlexBox()
		{
			Size = new Size(32, _buttons.Length),
			GrowDirection = GrowDirection.Vertical,
			AlwaysRecalculate = true,
			Position = new Point(0, 4)
		};
		box.Children.AddRange(_buttons);
		foreach (MenuButton button in _buttons)
		{
			button.Size = new Size(32, 1);
		}
		Elements.Add(box);	
		
		Elements.Add(area);
		
		area.StartFight();
	}

	public override void Update()
	{
		_selectedIndex = (_selectedIndex + _buttons.Length) % _buttons.Length;

		for (int i = 0; i < _buttons.Length; i++)
		{
			_buttons[i].IsSelected = i == _selectedIndex;
		}	

		while (Console.KeyAvailable)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			if (_area.IsFighting)
			{
				_area.HandleInput(key.Key);
				return;
			}
			
			switch (key.Key)
			{
				case ConsoleKey.UpArrow:
					_selectedIndex--;
					break;

				case ConsoleKey.DownArrow:
					_selectedIndex++;
					break;
				
				case ConsoleKey.Enter:
					_buttons[_selectedIndex].PerformAction();
					break;
			}
		}
	}
	
}