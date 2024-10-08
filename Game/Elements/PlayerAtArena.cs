using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace Game.Elements;

public class PlayerAtArena : Element
{
	public Player Player { get; set; }

	private Text _name;
	private HealthBar _healthBar;
	private Text _damageText;
	private ShieldBar _shieldBar;
	private FlexBox _flexBox;
	private Box _box;

	private PlayerFightOptions _fightOptions;

	public PlayerAtArena(Player player)
	{
		Player = player;
		
		_name = new Text()
		{
			Alignment = Alignment.Center,
			Value = "Player"
		};
		_healthBar = new HealthBar(player);
		_damageText = new Text();
		_shieldBar = new ShieldBar(player);

		_fightOptions = new PlayerFightOptions()
		{
			Size = new Size(16, 7)
		};

		_flexBox = new FlexBox()
		{
			GrowDirection = GrowDirection.Vertical,
			AlwaysRecalculate = true
		};
		
		_flexBox.Children.Add(_name);
		_flexBox.Children.Add(_healthBar);
		_flexBox.Children.Add(_damageText);
		_flexBox.Children.Add(_shieldBar);
		
		_box = Box.DefaultBox(new Point(), new Size(), _flexBox);
	}

	public override void Update()
	{
		_name.Size = new Size(Size.Width - 2, 1);
		_healthBar.Size = new Size(Size.Width - 2, 1);
		_damageText.Size = new Size(Size.Width - 2, 1);
		_shieldBar.Size = new Size(Size.Width - 2, 1);

		_damageText.Value = $"Damage: {Player.Damage}";

		if (Player.HasShield)
		{
			_box.Background = Color.Aqua;
		}
		else
		{
			_box.Background = null;
		}
		
		_box.Foreground = Player.Highlighted ? Color.Coral : null;

		_box.Size = new Size(Size.Width, _flexBox.Children.Count + 2);
		_box.Position = Position;

		_fightOptions.Position = new Point(Position.X + _box.Size.Width + 2, Position.Y);
		
		_fightOptions.Update();
		_box.Update();
	}

	public void HandleInput(ConsoleKey key)
	{
		if (_fightOptions.Active)
		{
			_fightOptions.HandleInput(key);
		}
	}

	public Decision MakeTurn(FightingArea arena)
	{
		if (_fightOptions.Active)
		{
			Decision decision = _fightOptions.GetDecision();
			if (decision != null)
			{
				_fightOptions.Active = false;
			}

			return decision;
		}
		else
		{
			_fightOptions.Show(arena);
			return null;
		}
	}

	protected override void OnRender()
	{
		_box.Render();
		_fightOptions.Render();
	}
}