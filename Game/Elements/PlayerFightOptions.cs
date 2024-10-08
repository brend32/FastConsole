using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;
using Game.Enemies;

namespace Game.Elements;

public class PlayerFightOptionRenderer : Element
{
	public bool Available { get; set; }
	public bool Selected { get; set; }
	public string Name
	{
		get => _text.Value;
		set => _text.Value = value;
	}

	private Action _action;
	private Text _text;
	
	public PlayerFightOptionRenderer(string name, Action action)
	{
		_text = new Text()
		{
			Alignment = Alignment.TopLeft,
			Value = name
		};

		_action = action;
	}

	public override void Update()
	{
		_text.Foreground = Available ? null : Color.SlateGray;
		_text.Background = Selected ? Color.SeaGreen : null;
		
		_text.Position = Position;
		_text.Size = Size = new Size(Size.Width, _text.FitSize.Height);
	}

	protected override void OnRender()
	{
		_text.Render();
	}

	public void Interact()
	{
		_action();
	}
}

public class PlayerFightOptions : Element
{
	public bool Active { get; set; }
	
	private FightingArea _area;
	private List<EnemyBase> _aliveEnemies;
	private int _selectedEnemyIndex;

	private int _selectedOptionIndex;
	private Decision _decision;

	private FlexBox _flexBox;

	private PlayerFightOptionRenderer _skip;
	private PlayerFightOptionRenderer _attack;
	private PlayerFightOptionRenderer _heal;
	private PlayerFightOptionRenderer _activateShield;
	private List<PlayerFightOptionRenderer> _options;
	private List<PlayerFightOptionRenderer> _availableOptions;

	public PlayerFightOptions()
	{
		_skip = new PlayerFightOptionRenderer("Skip", () =>
		{
			_decision = new Decision("Skip", () => { });
		});
		_attack = new PlayerFightOptionRenderer("Attack", () =>
		{
			_selectedEnemyIndex = 0;
		});
		_heal = new PlayerFightOptionRenderer("Heal", () =>
		{
			_decision = new Decision("Heal", () =>
			{
				_area.Player.Heal(20);
			});
		});
		_activateShield = new PlayerFightOptionRenderer("Activate shield", () =>
		{
			_decision = new Decision("Activate shield", () =>
			{
				_area.Player.ActivateShield();
			});
		});

		_options = new List<PlayerFightOptionRenderer>()
		{
			_skip,
			_attack,
			_heal,
			_activateShield
		};

		_flexBox = new FlexBox()
		{
			GrowDirection = GrowDirection.Vertical,
			AlwaysRecalculate = true,
		};
		
		_flexBox.Children.Add(new Text()
		{
			Value = "Choose option:",
			Size = new Size(1, 1)
		});
		_flexBox.Children.Add(_skip);
		_flexBox.Children.Add(_attack);
		_flexBox.Children.Add(_heal);
		_flexBox.Children.Add(_activateShield);
	}

	public override void Update()
	{
		if (Active == false)
			return;
		
		for (int i = 0; i < _options.Count; i++)
		{
			_options[i].Selected = _options[i] == _availableOptions[_selectedOptionIndex];
		}

		if (_aliveEnemies != null)
		{
			for (int i = 0; i < _aliveEnemies.Count; i++)
			{
				_aliveEnemies[i].Selected = i == _selectedEnemyIndex;
			}
		}


		_flexBox.Position = Position;
		_flexBox.Size = new Size(Size.Width, _flexBox.Children.Count);
		
		foreach (Element child in _flexBox.Children)
		{
			child.Size = new Size(_flexBox.Size.Width, child.Size.Height);
		}
		
		_flexBox.Update();
	}

	public void Show(FightingArea arena)
	{
		_area = arena;
		_decision = null;
		Active = true;

		UpdateOptions();
		_aliveEnemies = arena.Enemies.Where(e => e.IsAlive).ToList();

		_selectedEnemyIndex = -1;
		_selectedOptionIndex = 0;
	}

	private void UpdateOptions()
	{
		_skip.Available = true;
		_attack.Available = true;
		_heal.Available = _area.Player.Health != _area.Player.MaxHealth;
		_activateShield.Available = _area.Player.HasShield == false;

		_availableOptions = _options.Where(o => o.Available).ToList();
	}

	public Decision GetDecision()
	{
		return _decision;
	}

	public void HandleInput(ConsoleKey key)
	{
		if (_selectedEnemyIndex >= 0)
		{
			HandleEnemySelectInput(key);
			return;
		}
		
		switch (key)
		{
			case ConsoleKey.UpArrow:
				_selectedOptionIndex = (_selectedOptionIndex - 1 + _availableOptions.Count) % _availableOptions.Count;
				break;
			
			case ConsoleKey.DownArrow:
				_selectedOptionIndex = (_selectedOptionIndex + 1) % _availableOptions.Count;
				break;
			
			case ConsoleKey.Enter:
				_availableOptions[_selectedOptionIndex].Interact();
				break;
		}
	}

	private void HandleEnemySelectInput(ConsoleKey key)
	{
		switch (key)
		{
			case ConsoleKey.LeftArrow:
				_selectedEnemyIndex = (_selectedEnemyIndex - 1 + _aliveEnemies.Count) % _aliveEnemies.Count;
				break;
			
			case ConsoleKey.RightArrow:
				_selectedEnemyIndex = (_selectedEnemyIndex + 1) % _aliveEnemies.Count;
				break;
			
			case ConsoleKey.Escape:
			case ConsoleKey.E:
				_selectedEnemyIndex = -1;
				break;
			
			case ConsoleKey.Enter:
				EnemyBase enemy = _aliveEnemies[_selectedEnemyIndex];
				enemy.Selected = false;
				_selectedEnemyIndex = -1;
				_aliveEnemies = null;
				_decision = new Decision("Attack", () =>
				{
					enemy.ReceiveDamage(_area.Player.Damage);
				});
				break;
		}
	}

	protected override void OnRender()
	{
		if (Active == false)
			return;
		
		_flexBox.Render();
	}
}